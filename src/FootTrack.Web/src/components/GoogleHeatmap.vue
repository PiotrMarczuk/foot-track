<template>
  <div id="map" class="map">bla bla bla</div>
</template>

<script lang="ts">
import { Loader, LoaderOptions } from "google-maps";
import { Component, Prop, Vue } from "vue-property-decorator";
import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

@Component
export default class GoogleHeatmap extends Vue {
  points: Array<google.maps.LatLng> = new Array<google.maps.LatLng>();
  map!: google.maps.Map;
  heatmap!: google.maps.visualization.HeatmapLayer;

  async mounted() {
    const loader = new Loader(process.env.VUE_APP_GOOGLE_API_KEY, {
      libraries: ["visualization"]
    });
    const google = await loader.load();
    const initialMapCenter = new google.maps.LatLng(52.2663005, 21.0320765);

    this.map = new google.maps.Map(document.getElementById("map")!, {
      center: initialMapCenter,
      zoom: 18
    });

    this.heatmap = new google.maps.visualization.HeatmapLayer({
      data: this.points
    });

    setInterval(this.reloadHeatmap, 10000);
    const connection = new HubConnectionBuilder()
      .withUrl(process.env.VUE_APP_HUBURL || "")
      .configureLogging(LogLevel.Information)
      .build();

    connection.on("TrainingMessage", data => {
      const newPoint = new google.maps.LatLng(data.latitude, data.longitude);
      this.points.push(newPoint);
      this.map.setCenter(newPoint);
    });

    connection.start();
  }

  private reloadHeatmap() {
    this.heatmap.setMap(null);
    this.heatmap.setMap(this.map);
    if (this.points.length > 0) {
      this.map.setCenter(this.points[this.points.length - 1]);
    }
  }
}
</script>
<style lang="scss" scoped>
.map {
  height: 500px;
  width: 500px;
}
</style>
