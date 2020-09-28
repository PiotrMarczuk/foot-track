<template>
  <login-form></login-form>
  <register-form></register-form>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";
@Options({
  components: {
    LoginForm,
    RegisterForm
  }
})
export default class Login extends Vue {}
</script>