<template>
  <div class="center">
    <form @submit.prevent="register">
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
          <vs-input
            type="text"
            v-model="userFormGroup.props.firstName"
            placeholder="First Name"
          >
            <template #icon>
              <i class="material-icons">person</i>
            </template>
          </vs-input>

          <vs-input
            type="text"
            v-model="userFormGroup.props.lastName"
            placeholder="Last Name"
          >
            <template #icon>
              <i class="material-icons">people</i>
            </template>
          </vs-input>
        </div>

        <template #footer>
          <div class="footer-dialog">
            <vs-button
              block
              color="#ff2e63"
              :disabled="userFormGroup.invalid"
              @click="register"
            >
              Register
            </vs-button>
          </div>
        </template>
      </vs-dialog>
    </form>
  </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { RxFormBuilder, IFormGroup } from "@rxweb/reactive-forms";
import { UserRegister } from "@/models/UserRegister";

@Component
export default class LoginForm extends Vue {
  userFormGroup!: IFormGroup<UserRegister>;
  formBuilder: RxFormBuilder = new RxFormBuilder();

  constructor() {
    super();
    this.userFormGroup = this.formBuilder.formGroup(UserRegister) as IFormGroup<
      UserRegister
    >;
  }

  get visible() {
    return this.$store.state.form.registerFormVisible;
  }

  set visible(value: boolean) {
    const { commit } = this.$store;
    commit("form/setRegisterFormVisible", value);
  }

  register() {
    const { dispatch } = this.$store;
    dispatch("profile/register", this.userFormGroup.props);
  }
}
</script>

<style lang="scss" scoped>
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
