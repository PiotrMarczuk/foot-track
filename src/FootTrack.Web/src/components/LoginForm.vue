<template>
  <v-dialog v-model="visible" hide-overlay max-width="600px">
    <v-card>
      <v-card-title>
        <span class="headline"> Welcome to Foot Track</span>
      </v-card-title>
      <v-card-text class="login-form-wrapper">
        <validation-observer ref="validationObserverRef">
          <form>
            <validation-provider
              v-slot="{ errors }"
              name="Email"
              rules="required|email"
            >
              <v-text-field
                v-model="userLogin.email"
                :error-messages="errors"
                label="E-mail"
                required
              ></v-text-field>
            </validation-provider>
            <validation-provider
              v-slot="{ errors }"
              name="Password"
              rules="required|min:6"
            >
              <v-text-field
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                :type="showPassword ? 'text' : 'password'"
                :error-messages="errors"
                label="Password"
                hint="At least 6 characters"
                class="input-group--focused"
                @click:append="showPassword = !showPassword"
                v-model="userLogin.password"
              ></v-text-field>
            </validation-provider>
            <v-card-actions >
              <v-container>
                <v-row>
                  <v-btn
                    id="custom-disabled"
                    color="secondary"
                    @click="submit"
                    :loading="isSubmitDisabled"
                    :disabled="isSubmitDisabled"
                    block
                    rounded
                  >
                    Log in
                  </v-btn>
                </v-row>
                <v-row class="register-wrapper">
                  <v-card-text>
                    <span class="account-question">
                      Don't have account yet?
                    </span>
                    <v-btn color="secondary" text small rounded @click="enableRegister">
                    Register
                     <v-icon right>
                      mdi-account-plus
                    </v-icon>
                  </v-btn>
                  </v-card-text>
                </v-row>
              </v-container>
            </v-card-actions>
          </form>
        </validation-observer>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { extend, ValidationObserver, ValidationProvider } from "vee-validate";
import { required, email, min } from "vee-validate/dist/rules";
import { UserLogin } from "@/models/UserLogin";
import { getters } from "@/store/profile/getters";

extend("required", {
  ...required,
  message: "{_field_} can not be empty"
});

extend("min", {
  ...min,
  message: "{_field_} must be longer than {length} characters"
}),
  extend("email", {
    ...email,
    message: "Email must be valid"
  });

@Component({
  components: {
    ValidationProvider,
    ValidationObserver
  }
})
export default class LoginForm extends Vue {
  $refs!: {
    validationObserverRef: InstanceType<typeof ValidationObserver>;
  };

  showPassword = false;
  userLogin = new UserLogin();
  errors = null;

  get isSubmitDisabled(): boolean {
    const { getters } = this.$store;
    return getters["profile/userStatus"].loggingIn;
  }

  async submit() {
    if ((await this.$refs.validationObserverRef.validate()) == false) {
      return;
    }

    const { dispatch } = this.$store;
    dispatch("profile/login", this.userLogin);
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
    dispatch("profile/login", {});
  }

  public enableRegister() {
    const { commit } = this.$store;
    commit("form/setRegisterFormVisible", true);
  }
}
</script>

<style lang="scss" scoped>
#custom-disabled.theme--light.v-btn.v-btn--disabled:not(.v-btn--flat):not(.v-btn--text):not(.v-btn--outlined) {
  background-color: $secondary !important;
  color: white !important;
}

.register-wrapper {
  text-align: center;
}
</style>

