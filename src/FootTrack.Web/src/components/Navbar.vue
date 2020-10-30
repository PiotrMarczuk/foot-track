<template>
  <div>
    <v-app-bar app color="primary" dark>
      <v-btn text depressed to="/">FootTrack</v-btn>
      <v-spacer></v-spacer>
      <v-btn v-if="isUserLoggedIn" @click="logout">
        LogOut
      </v-btn>
    </v-app-bar>
  </div>
</template>

<script lang="ts">
import { Action, Getter, Mutation } from "vuex-class";
import { Component, Vue } from "vue-property-decorator";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";
import { UserStatus } from "@/store/profile/types";

@Component({
  components: {
    LoginForm,
    RegisterForm
  }
})
export default class Navbar extends Vue {
  @Getter("profile/userStatus") userStatus!: UserStatus;
  @Action("profile/logout") userLogout!: any;

  get isUserLoggedIn() {
    return this.userStatus?.loggedIn;
  }

  logout() {
    this.userLogout();
  }
}
</script>
