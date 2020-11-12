<template>
  <div>
    <div id="map" class="map"></div>
    <div class="container">
      <training-chart :data="chartData" :options="chartOptions" v-if="loaded"></training-chart>
    </div>
  </div>
</template>

<script lang="ts">
import { trainingService } from "@/services";
import { Component, Vue } from "vue-property-decorator";
import { TrainingRecord } from "@/models/TrainingRecord";
import { Loader, LoaderOptions } from "google-maps";
import { google } from "google-maps";
import TrainingChart from "./TrainingChart.vue";

@Component({
  components: {
    TrainingChart
  }
})
export default class TrainingDetails extends Vue {
  trainingData = new Array<TrainingRecord>();
  points = new Array<google.maps.LatLng>();
  map!: google.maps.Map;
  heatmap!: google.maps.visualization.HeatmapLayer;
  loaded = false;
  chartData = [] as any;
  chartOptions = [] as any;

  async mounted() {
    const records = await trainingService
      .getTraining(this.$route.params.id)
      .then(training => {
        return training.result.trainingRecords;
      });

    this.trainingData = records;

    const timeLabels = this.trainingData.map(x =>
      new Date(x.timestamp).toLocaleTimeString("pl-PL")
    );
    const speed = this.trainingData.map(x => x.speed + 20);

    this.chartData = {
      labels: timeLabels,
      datasets: [
        {
          label: "Speed",
          data: speed,
          backgroundColor: "#f87979",
          fill: false
        }
      ]
    };

    this.chartOptions = {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        yAxes: [
          {
            display: true,
            scaleLabel: {
              display: true,
              labelString: "Speed [m/s]"
            }
          }
        ],
        xAxes: [
          {
            display: true,
            scaleLabel: {
              display: true,
              labelString: "Timestamp [hh:mm:ss]"
            }
          }
        ]
      }
    };

    this.loaded = true;

    const loader = new Loader(process.env.VUE_APP_GOOGLE_API_KEY, {
      libraries: ["visualization"]
    });
    const google = await loader.load();

    const points = this.trainingData.map(
      x => new google.maps.LatLng(x.latitude, x.longitude)
    );

    this.points = points;
    const initialMapCenter = this.points[this.points.length - 1];
    this.map = new google.maps.Map(document.getElementById("map")!, {
      center: initialMapCenter,
      zoom: 18
    });

    this.heatmap = new google.maps.visualization.HeatmapLayer({
      data: this.points
    });

    this.heatmap.setMap(this.map);
  }
}
</script>
<style lang="scss" scoped>
.map {
  height: 500px;
  width: 500px;
}
</style>