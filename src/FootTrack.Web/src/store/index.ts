import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";
import { RootState } from "./types";
import { profile } from "@/store/profile/";
import { alert } from "@/store/alert/";
import { form } from "@/store/form/";

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
  state: {
    version: "1.0.0"
  },
  modules: {
    profile,
    alert,
    form
  }
};

export default new Vuex.Store<RootState>(store);
