<template>
  <div id="app">
    <login-form></login-form>
    <register-form></register-form>
    <div>
      <navbar></navbar>
    </div>
    <div>
      <router-view />
      <div v-if="alert.message">
        <alert v-bind="alert.message"></alert>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Alert from "@/components/Alert.vue";
import Navbar from "@/components/Navbar.vue";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";

@Component({
  components: {
    Alert,
    Navbar,
    LoginForm,
    RegisterForm
  }
})
export default class App extends Vue {
  get alert() {
    return this.$store.state.alert;
  }
}
</script>
<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  background-color: $base-white;
}
</style>
