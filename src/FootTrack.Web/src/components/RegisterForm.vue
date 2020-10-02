<template>
  <v-row justify="center">
    <v-dialog v-model="visible" hide-overlay max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline"> Join Foot Track </span>
        </v-card-title>
        <v-card-text>
          <validation-observer ref="validationObserverRef">
            <form>
              <validation-provider
                v-slot="{ errors }"
                name="Email"
                rules="required|email"
              >
                <v-text-field
                  v-model="userRegister.email"
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
                  v-model="userRegister.password"
                ></v-text-field>
              </validation-provider>
              <validation-provider
                v-slot="{ errors }"
                name="Confirm password"
                rules="required|min:6|confirmed:Password"
              >
                <v-text-field
                  :append-icon="showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="showConfirmPassword ? 'text' : 'password'"
                  :error-messages="errors"
                  label="Confirm password"
                  hint="At least 6 characters"
                  class="input-group--focused"
                  @click:append="showConfirmPassword = !showConfirmPassword"
                  v-model="confirmPassword"
                ></v-text-field>
              </validation-provider>
              <v-text-field
                v-model="userRegister.firstName"
                label="First name"
              ></v-text-field>
              <v-text-field
                v-model="userRegister.lastName"
                label="Last name"
              ></v-text-field>

              <v-btn
                id="custom-disabled-register"
                color="secondary"
                @click="submit"
                :loading="isSubmitDisabled"
                :disabled="isSubmitDisabled"
                block
                rounded
              >
                REGISTER
              </v-btn>
            </form>
          </validation-observer>
        </v-card-text>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { extend, ValidationObserver, ValidationProvider } from "vee-validate";
import { required, email, min, confirmed } from "vee-validate/dist/rules";
import { getters } from "@/store/profile/getters";
import { UserRegister } from "@/models/UserRegister";

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
extend("confirmed", {
  ...confirmed,
  message: "{_field_} does not match."
});

@Component({
  components: {
    ValidationProvider,
    ValidationObserver
  }
})
export default class RegisterForm extends Vue {
  $refs!: {
    validationObserverRef: InstanceType<typeof ValidationObserver>;
  };

  showPassword = false;
  showConfirmPassword = false;
  confirmPassword = "";
  userRegister = new UserRegister();
  errors = null;

  get isSubmitDisabled(): boolean {
    const { getters } = this.$store;
    return getters["profile/userStatus"].registering;
  }

  async submit() {
    if ((await this.$refs.validationObserverRef.validate()) == false) {
      return;
    }

    const { dispatch } = this.$store;
    dispatch("profile/register", this.userRegister);
  }

  get visible() {
    return this.$store.state.form.registerFormVisible;
  }

  set visible(value: boolean) {
    const { commit } = this.$store;
    commit("form/setRegisterFormVisible", value);
  }
}
</script>

<style lang="scss" scoped>
#custom-disabled-register.theme--light.v-btn.v-btn--disabled:not(.v-btn--flat):not(.v-btn--text):not(.v-btn--outlined) {
  background-color: $secondary !important;
  color: white !important;
}
</style>
