<template>
  <div>
    <v-bottom-navigation color="primary" grow app :value="value">
      <v-btn to="/">
        <span>Home</span>
        <v-icon>mdi-home-circle-outline</v-icon></v-btn
      >

      <v-btn to="/about">
        <span>About</span>
        <v-icon>mdi-information-outline</v-icon></v-btn
      >

      <template v-if="isUserLoggedIn">
        <v-btn to="/training">
          <span>Training</span>
          <v-icon>mdi-soccer</v-icon>
        </v-btn>
      </template></v-bottom-navigation
    >
  </div>
</template>

<script lang="ts">
import { UserStatus } from "@/store/profile/types";
import { Component, Vue } from "vue-property-decorator";
import { Getter } from "vuex-class";

@Component
export default class BottomNavigation extends Vue {
  @Getter("profile/userStatus") userStatus!: UserStatus;
  value = 0;

  get isUserLoggedIn() {
    return this.userStatus?.loggedIn;
  }
}
</script>