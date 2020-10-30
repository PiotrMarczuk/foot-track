<template>
  <div>
    <v-overlay :value="isLoginFormEnabled || isRegisterFormEnabled"></v-overlay>
    <login-form v-if="isLoginFormEnabled"></login-form>
    <register-form v-if="isRegisterFormEnabled"></register-form>
    <v-container fluid>
      <v-row>
        <v-col>
          <template v-if="isUserLoggedIn">
            <training-card class="top-card"></training-card>
          </template>
          <template v-else>
            <signup-card class="top-card"></signup-card>
          </template>
          <about-card></about-card>
        </v-col>
        <v-col>
          <quotes-carousel></quotes-carousel>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LoginForm from "@/components/LoginForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";
import SignupCard from "@/components/SignupCard.vue";
import TrainingCard from "@/components/TrainingCard.vue";
import AboutCard from "@/components/AboutCard.vue";
import QuotesCarousel from "@/components/QuotesCarousel.vue";
import { getters } from "@/store/profile/getters";
import { Getter } from "vuex-class";
import { UserStatus } from "@/store/profile/types";

@Component({
  components: {
    LoginForm,
    RegisterForm,
    SignupCard,
    QuotesCarousel,
    AboutCard,
    TrainingCard
  }
})
export default class Home extends Vue {
  @Getter("form/loginFormVisible") isLoginFormVisible!: boolean;
  @Getter("form/registerFormVisible") isRegisterFormVisible!: boolean;
  @Getter("profile/userStatus") userStatus!: UserStatus;

  get isLoginFormEnabled() {
    return this.isLoginFormVisible;
  }

  get isRegisterFormEnabled() {
    return this.isRegisterFormVisible;
  }

  get isUserLoggedIn() {
    return this.userStatus?.loggedIn;
  }
}
</script>

<style lang="scss" scoped>
.top-card {
  padding-bottom: 30px;
}
</style>
