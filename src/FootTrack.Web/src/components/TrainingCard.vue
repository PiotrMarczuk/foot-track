<template>
  <div class="mx-auto">
    <v-card color="primary" dark>
      <v-card-title>
        <v-icon large left>
          mdi-twitter
        </v-icon>
        <span class="title font-weight-light">Twitter</span>
      </v-card-title>

      <v-card-text class="headline font-weight-bold">
        Why aren't you excersising yet!? Oh my god. Just start your training as
        soon as possible.
      </v-card-text>

      <v-card-actions class="end">
        <v-list-item class="grow">
          <v-list-item-avatar color="grey darken-3">
            <v-img
              class="elevation-6"
              src="https://images.daznservices.com/di/library/GOAL/81/db/cristiano-ronaldo-2019_654nqmhaue6913qn8u094bh8p.jpg?t=-268858035&quality=100"
            ></v-img>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>Cristiano Ronaldo</v-list-item-title>
          </v-list-item-content>

          <v-row align="center" justify="end">
            <v-btn
              rounded
              color="secondary"
              @click="startTraining"
              >Start training</v-btn
            >
          </v-row>
        </v-list-item>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script lang="ts">
import { trainingService } from "@/services";
import { User } from '@/store/profile/types';
import { Component, Vue } from "vue-property-decorator";
import { Getter } from 'vuex-class';

@Component
export default class TrainingCard extends Vue {
  @Getter("profile/user") user!: User

  async startTraining() {
    const trainingId = await trainingService.startTraining(this.user.id);
    this.$router.push(`/training/${trainingId}`);
  }
}
</script>