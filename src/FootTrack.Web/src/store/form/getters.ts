import { GetterTree } from "vuex";
import { FormState } from "./types";
import { RootState } from "../types";

export const getters: GetterTree<FormState, RootState> = {
  registerFormVisible(state) {
    const { registerFormVisible } = state;
    return registerFormVisible;
  },
  loginFormVisible(state){
    const { loginFormVisible } = state;
    return loginFormVisible;
  }
};
