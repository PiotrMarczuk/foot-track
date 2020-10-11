<template>
  <div id="map" class="map">bla bla bla</div>
</template>

<script lang="ts">
import { Loader, LoaderOptions } from "google-maps";
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class GoogleHeatmap extends Vue {
  points: Array<google.maps.LatLng> = new Array<google.maps.LatLng>();
  map: any;
  heatmap: any;

  async mounted() {
    const loader = new Loader(process.env.VUE_APP_GOOGLE_API_KEY, {
      libraries: ["visualization"]
    });
    const google = await loader.load();
    this.map = new google.maps.Map(document.getElementById("map")!, {
      center: { lat: 37.782, lng: -122.447 },
      zoom: 20
    });

    this.heatmap = new google.maps.visualization.HeatmapLayer({
      data: this.points
    });

    setInterval(this.reloadHeatmap, 10000);
  }

  private reloadHeatmap() {
    this.heatmap.setMap(null);
    this.heatmap.setMap(this.map);
    console.log('reloaded');
  }
}
</script>
<style lang="scss" scoped>
.map {
  height: 500px;
  width: 500px;
}
</style>
