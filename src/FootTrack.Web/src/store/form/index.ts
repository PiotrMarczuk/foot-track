import { mutations } from "./mutations";
import { FormState } from "./types";
import { getters } from "./getters";

export const state: FormState = {
  loginFormVisible: false,
  registerFormVisible: false
};

const namespaced = true;

export const form = {
  namespaced,
  state,
  getters,
  mutations
};
