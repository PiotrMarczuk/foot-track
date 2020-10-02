import Vue from "vue";
/* eslint-disable */
// @ts-ignore
import Vuetify from "vuetify/lib";
/* eslint-enable */

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    options: {
      customProperties: true
    },
    themes: {
      light: {
        primary: "#00adb5",
        secondary: "#393e46",
        accent: "#222831",
        error: "#FF5252",
        info: "#2196F3",
        success: "#4CAF50",
        warning: "#FFC107"
      }
    }
  }
});
