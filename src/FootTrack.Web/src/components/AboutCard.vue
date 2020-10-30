<template>
  <v-card color="secondary" dark>
    <div class="d-flex flex-no-wrap justify-space-between">
      <div>
        <v-card-title class="headline" v-text="header"></v-card-title>
        <v-card-subtitle
          >You can get all the informations clicking
          <v-btn to="/about" text rounded>here </v-btn></v-card-subtitle
        >
      </div>
    </div>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Mutation } from "vuex-class";

@Component
export default class AboutCard extends Vue {
  get header() {
    return "Didn't you know about Foot Track?";
  }
}
</script>