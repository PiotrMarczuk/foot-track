<template>
  <div class="home">
    <google-heatmap></google-heatmap>
  </div>
</template>

<script lang="ts">

import { Component, Vue } from "vue-property-decorator";
import GoogleHeatmap from "@/components/GoogleHeatmap.vue";
@Component({
  components:{
    GoogleHeatmap
  }
})
export default class Home extends Vue {}
</script>
