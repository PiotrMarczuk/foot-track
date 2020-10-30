import { MutationTree } from "vuex";
import { AlertState } from "./types";

export const mutations: MutationTree<AlertState> = {
  success(state, message: string) {
    state.type = "alert-success";
    state.message = message;
  },
  error(state, message: string) {
    state.type = "alert-danger";
    state.message = message;
  },
  clear(state) {
    state.type = undefined;
    state.message = undefined;
  }
};
