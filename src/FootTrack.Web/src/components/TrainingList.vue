<template>
  <v-card class="mx-auto">
    <v-list>
      <v-list-item-group>
        <v-list-item two-line v-for="(training, i) in trainings" :key="i" @click="navigateToTraining(training.id)">
          <v-list-item-content>
            <v-list-item-title>{{ training.name }}</v-list-item-title>
            <v-list-item-subtitle>
              {{ training.dateAndTime }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-card>
</template>

<script lang="ts">
import { trainingService } from "@/services";
import { User } from "@/store/profile/types";
import { Training } from "@/models/Training";
import { Component, Vue } from "vue-property-decorator";
import { Getter } from "vuex-class";
import router from '@/router';

@Component
export default class TrainingList extends Vue {
  @Getter("profile/user") user!: User;
  trainings = [];
  i = 0;
  pageSize = 10;

  async mounted() {
    this.trainings = await trainingService
      .getTrainings(this.user.id, 1, this.pageSize)
      .then(training => {
        return training.result;
      });
  }

  navigateToTraining(trainingId: string){
      router.push(`/training-history/${trainingId}`);
  }
}
</script>
