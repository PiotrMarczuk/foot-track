import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import TrainingHub from "@/plugins/training-hub";
import vuetify from "./plugins/vuetify";
/* eslint-disable */
// @ts-ignore
import VueGoogleHeatmap from "vue-google-heatmap";
/* eslint-enable */

Vue.config.productionTip = false;
Vue.use(VueGoogleHeatmap, {
  apiKey: ""
});


Vue.use(TrainingHub);
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
