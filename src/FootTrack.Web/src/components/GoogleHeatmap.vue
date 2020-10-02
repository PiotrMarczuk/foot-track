<template>
  <vue-google-heatmap :points="points" :width="400" :height="350" />
</template>

<script lang="ts">
import { Point } from '@/models/Point';
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class GoogleHeatmap extends Vue {
  points = new Array<Point>();
  constructor() {
    super();
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
    this.points.push({ lat: 37.786117, lng: -122.440119 });
  }
}
</script>
