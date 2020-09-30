<template>
  <div class="center">
    <form>
      <vs-dialog blur v-model="visible">
        <template #header>
          <h4 class="not-margin">Welcome to <b>Foot Track</b></h4>
        </template>

        <div class="con-form">
          <vs-input v-model="userFormGroup.props.email" placeholder="Email">
            <template #icon>
              <i class="material-icons">email</i>
            </template>
          </vs-input>
          <vs-input
            type="password"
            v-model="userFormGroup.props.password"
            placeholder="Password"
          >
            <template #icon>
              <i class="material-icons">lock</i>
            </template>
          </vs-input>
        </div>

        <template #footer>
          <div class="footer-dialog">
            <vs-button color="#ff2e63" :disabled="!userFormGroup.valid" block @click="login">
              Sign In
            </vs-button>
            <div class="new">
              New Here?
              <vs-button color="#ff2e63"  @click="enableRegister"
                ><i class="material-icons">check_box</i>Create New
                Account</vs-button
              >
            </div>
          </div>
        </template>
      </vs-dialog>
    </form>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { RxFormBuilder, IFormGroup } from "@rxweb/reactive-forms";
import { UserLogin } from "@/models/UserLogin";

@Component
export default class LoginForm extends Vue {
  userFormGroup!: IFormGroup<UserLogin>;
  formBuilder: RxFormBuilder = new RxFormBuilder();
  submitted = false;
  checkbox1 = false;

  constructor() {
    super();
    this.userFormGroup = this.formBuilder.formGroup(UserLogin) as IFormGroup<
      UserLogin
    >;
  }

  get visible() {
    return this.$store.state.form.loginFormVisible;
  }

  set visible(value: boolean) {
    const { commit } = this.$store;
    commit("form/setLoginFormVisible", value);
  }

  public login() {
    const { dispatch } = this.$store;
    dispatch("profile/login", this.userFormGroup.props);
  }

  public enableRegister() {
    const { commit } = this.$store;
    commit("form/setRegisterFormVisible", true);
  }
}
</script>

  <style lang="scss">
.not-margin {
  margin: 0px;
  font-weight: normal;
  padding: 10px;
}

.con-form {
  width: 100%;
}

.con-form .flex {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.con-form .flex a {
  font-size: 0.8rem;
  opacity: 0.7;
}

.con-form .flex a:hover {
  opacity: 1;
}

.con-form .vs-checkbox-label {
  font-size: 0.8rem;
}

.con-form .vs-input-content {
  margin: 10px 0px;
  width: calc(100%);
}

.con-form .vs-input-content .vs-input {
  width: 100%;
}

.footer-dialog {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  width: calc(100%);
}

.footer-dialog .new {
  margin: 0px;
  margin-top: 20px;
  padding: 0px;
  font-size: 0.7rem;
}

.footer-dialog .new a {
  color: rgba(var(--vs-primary), 1) !important;
  margin-left: 6px;
}

.footer-dialog .new a:hover {
  text-decoration: underline;
}

.footer-dialog .vs-button {
  margin: 0px;
}
</style>