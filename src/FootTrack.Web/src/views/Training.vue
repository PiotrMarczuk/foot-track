<template>
  <div>
    <google-heatmap></google-heatmap>

    <v-btn @click="endTraining">End</v-btn>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import GoogleHeatmap from "@/components/GoogleHeatmap.vue";
import { trainingService } from "@/services";
import { Getter } from "vuex-class";
import { User } from "@/store/profile/types";

@Component({
  components: {
    GoogleHeatmap
  }
})
export default class Training extends Vue {
  @Getter("profile/user") user!: User;

  endTraining() {
    trainingService.endTraining(this.user.id);
  }
}
</script>
