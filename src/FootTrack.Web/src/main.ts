import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { ReactiveFormConfig, ClientLibrary } from '@rxweb/reactive-forms';
import "vuesax/dist/vuesax.css";
import "material-icons/iconfont/material-icons.scss";
/* eslint-disable */
// @ts-ignore
import Vuesax from "vuesax";
// @ts-ignore
import VueGoogleHeatmap from "vue-google-heatmap";
/* eslint-enable */

import TrainingHub from "@/services/training-hub";



ReactiveFormConfig.clientLib = ClientLibrary.Vue;
Vue.config.productionTip = false;
Vue.use(Vuesax);
Vue.use(VueGoogleHeatmap, {
  apiKey: ''
});

ReactiveFormConfig.set({
  validationMessage: {
    required: "This field is required",
    email: "Email is invalid",
    notEmpty: "This field should not be empty",
  }
});

Vue.use(TrainingHub);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
