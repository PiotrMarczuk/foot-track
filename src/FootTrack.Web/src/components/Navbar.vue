<template>
  <div class="center examplex">
    <login-form></login-form>
    <register-form></register-form>
    <vs-navbar
      class="nav"
      padding-scroll
      text-white
      center-collapsed
      v-model="active"
      v-bind:color="'#252a34'"
    >
      <template #left>
        FOOTTRACK
      </template>
      <vs-navbar-item :active="active == 'guide'" id="guide">
        Guide
      </vs-navbar-item>
      <vs-navbar-item :active="active == 'docs'" id="docs">
        Documents
      </vs-navbar-item>
      <vs-navbar-item :active="active == 'components'" id="components">
        Components
      </vs-navbar-item>
      <vs-navbar-item :active="active == 'license'" id="license">
        License
      </vs-navbar-item>
      <template #right>
        <template v-if="!currentUser">
          <vs-button @click="loginClick" color="#ff2e63">Sign up</vs-button>
        </template>
        <template v-else>
          <vs-button @click="logout" color="#ff2e63">Log out</vs-button>

          <vs-avatar>
            <i class="material-icons">person</i>
          </vs-avatar>
        </template>
      </template>
    </vs-navbar>
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