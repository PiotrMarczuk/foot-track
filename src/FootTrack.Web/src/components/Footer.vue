<template>
  <v-footer app :padless="true" :fixed="true">
    <v-card flat tile width="100%" class="text-center" color="secondary">
      <v-card-text class="white--text">
        {{ new Date().getFullYear() }} — <strong>Foot Track</strong>
      </v-card-text>
    </v-card>
  </v-footer>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Navbar extends Vue {}
</script>