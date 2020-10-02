<template>
<div>
    <v-app-bar app color="primary" dark>
      <v-spacer></v-spacer>
      <v-btn
        color="secondary"
        dark
        rounded
        @click="loginClick"
      >
        <span class="mr-2">Sign in</span>
        <v-icon>mdi-soccer</v-icon>
      </v-btn>
    </v-app-bar>
</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";

@Component({
  components: {
    LoginForm,
    RegisterForm
  }
})
export default class Navbar extends Vue {
  public active = "guide";

  get currentUser() {
    return this.$store.state.profile.user;
  }

  loginClick() {
    const { commit } = this.$store;
    commit("form/setLoginFormVisible", true);
  }

  logout() {
    const { dispatch } = this.$store;
    dispatch("profile/logout");
  }
}
</script>
