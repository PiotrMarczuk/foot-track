<template>
  <div>
    <v-overlay :value="isLoginFormEnabled || isRegisterFormEnabled"></v-overlay>
    <login-form v-if="isLoginFormEnabled"></login-form>
    <register-form v-if="isRegisterFormEnabled"></register-form>
    <router-link to="/training">
      <v-btn v-if="isUserLoggedIn" @click="startTraining">Start training</v-btn>
    </router-link>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";
import { getters } from "@/store/profile/getters";
import { trainingService } from "@/services";
import { Getter } from "vuex-class";
import { User, UserStatus } from '@/store/profile/types';

@Component({
  components: {
    LoginForm,
    RegisterForm
  }
})
export default class Home extends Vue {
  @Getter("form/loginFormVisible") isLoginFormVisible!: boolean;
  @Getter("form/registerFormVisible") isRegisterFormVisible!: boolean;
  @Getter("profile/user") user!: User;
  @Getter("profile/userStatus") userStatus!: UserStatus


  get isLoginFormEnabled() {
    return this.isLoginFormVisible;
  }

  get isRegisterFormEnabled() {
    return this.isRegisterFormVisible;
  }

  get isUserLoggedIn(){
    return this.userStatus?.loggedIn;
  }

  startTraining() {
    trainingService.startTraining(this.user.id);
  }
}
</script>
