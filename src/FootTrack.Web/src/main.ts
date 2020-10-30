import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import Axios from 'axios';

Vue.config.productionTip = false;

Axios.interceptors.request.use(config => {
  config.headers.Authorization = `Bearer ${localStorage.getItem('user-token')}`;
  return config;
},
  error => Promise.reject(error));

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
