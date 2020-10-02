<template>
  <div>
    <v-overlay :value="isLoginFormEnabled || isRegisterFormEnabled"></v-overlay>
    <login-form v-if="isLoginFormEnabled"></login-form>
    <register-form v-if="isRegisterFormEnabled"></register-form>
    <router-link to="/training"> <v-btn>Start training</v-btn> </router-link>
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
export default class Home extends Vue {
  get isLoginFormEnabled() {
    return this.$store.state.form.loginFormVisible;
  }

  get isRegisterFormEnabled() {
    return this.$store.state.form.registerFormVisible;
  }
}
</script>
