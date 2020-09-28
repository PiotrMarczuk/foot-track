<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="email"> Email </label>
        <input type="email" v-model="user.email" />
      </div>
      <div class="form-group">
        <label for="password"> Password </label>
        <input type="password" v-model="user.password" />
      </div>
      <div class="form-group">
        <button class="btn btn-primary">Login</button>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { UserLoginDto } from "@/dtos/UserLoginDto";
import { AuthService } from "@/services/auth.service";
import { UserRegisterDto } from '@/dtos/UserRegisterDto';

export default class RegisterForm extends Vue {
  public user: UserRegisterDto = new UserRegisterDto();

  public handleSubmit(): void {
    console.log(this.user);
    let authService = new AuthService();
    authService.register(this.user);
  }
}
</script>

<style lang="sass" scoped>

</style>
