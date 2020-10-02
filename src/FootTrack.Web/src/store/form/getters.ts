import { GetterTree } from "vuex";
import { FormState } from "./types";
import { RootState } from "../types";

export const getters: GetterTree<FormState, RootState> = {
  isRegisterFormVisible(state) {
    const { registerFormVisible } = state;
    return registerFormVisible;
  }
};
