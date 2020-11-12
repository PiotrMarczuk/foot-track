<template>
  <div>
    <span> Actual speed: {{ actualSpeed }} [m/s]</span>
    <div id="map" class="map"></div>
  </div>
</template>

<script lang="ts">
import { Loader, LoaderOptions } from "google-maps";
import { Component, Prop, Vue } from "vue-property-decorator";
import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
import { trainingService } from "@/services";
import { TrainingMeasurement } from "@/models/TrainingMeasurement";
import { SpeedWithTimestamp } from "@/models/SpeedWithTimestamp";

@Component
export default class GoogleHeatmap extends Vue {
  points: Array<google.maps.LatLng> = new Array<google.maps.LatLng>();
  additionalData: Array<SpeedWithTimestamp> = new Array<SpeedWithTimestamp>();

  map!: google.maps.Map;
  heatmap!: google.maps.visualization.HeatmapLayer;
  sentPointsCount = 0;
  private _actualSpeed = 0;

  async mounted() {
    const loader = new Loader(process.env.VUE_APP_GOOGLE_API_KEY, {
      libraries: ["visualization"],
    });
    const google = await loader.load();
    const initialMapCenter = new google.maps.LatLng(52.2663005, 21.0320765);

    this.map = new google.maps.Map(document.getElementById("map")!, {
      center: initialMapCenter,
      zoom: 18,
    });

    this.heatmap = new google.maps.visualization.HeatmapLayer({
      data: this.points,
    });

    setInterval(this.reloadHeatmap, 10000);
    setInterval(this.sendData, 10000);

    const connection = new HubConnectionBuilder()
      .withUrl(process.env.VUE_APP_HUBURL || "")
      .configureLogging(LogLevel.Information)
      .build();

    connection.on("TrainingMessage", (data) => {
      const newPoint = new google.maps.LatLng(data.latitude, data.longitude);
      const additionalData = new SpeedWithTimestamp(data.Speed, data.Timestamp);

      this.points.push(newPoint);
      this.additionalData.push(additionalData);
      this._actualSpeed = data.Speed;
      this.map.setCenter(newPoint);
    });

    connection.start();
  }

  get actualSpeed() {
    return this._actualSpeed;
  }

  private reloadHeatmap() {
    this.heatmap.setMap(null);
    this.heatmap.setMap(this.map);
    if (this.points.length > 0) {
      this.map.setCenter(this.points[this.points.length - 1]);
    }
  }

  private sendData() {
    const pointsToBeSent = this.points.slice(
      this.sentPointsCount,
      this.points.length
    );
    const additionalDataToBeSent = this.additionalData.slice(
      this.sentPointsCount,
      this.additionalData.length
    );

    const sentDataLength = this.additionalData.length;

    const dataToBeSent = new Array<TrainingMeasurement>(pointsToBeSent.length);

    for (let index = 0; index < pointsToBeSent.length; index++) {
      const point = pointsToBeSent[index];
      const additionalData = additionalDataToBeSent[index];

      dataToBeSent.push(
        new TrainingMeasurement(
          point,
          additionalData.speed,
          additionalData.timestamp
        )
      );
    }

    trainingService.sendTrainingData(this.$route.params.id, dataToBeSent);
    this.sentPointsCount = sentDataLength;
  }
}
</script>
<style lang="scss" scoped>
.map {
  height: 500px;
  width: 500px;
}
</style>
