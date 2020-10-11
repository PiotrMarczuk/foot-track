<template>
  <div>
    <v-app-bar app color="primary" dark>
      <v-spacer></v-spacer>
      <v-btn
        color="secondary"
        dark
        rounded
        @click="loginClick"
        v-if="!isUserLoggedIn"
      >
        <span class="mr-2">Sign in</span>
        <v-icon>mdi-soccer</v-icon>
      </v-btn>

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
  @Mutation("form/setLoginFormVisible") setLoginFormVisibility: any;

  get isUserLoggedIn() {
    return this.userStatus?.loggedIn;
  }

  loginClick() {
    this.setLoginFormVisibility(true);
  }

  logout() {
    this.userLogout();
  }
}
</script>
