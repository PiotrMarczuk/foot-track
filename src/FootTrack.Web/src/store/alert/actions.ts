import { ActionTree } from "vuex";
import { RootState } from "../types";
import { AlertState } from "./types";

export const actions: ActionTree<AlertState, RootState> = {
  success({ commit }, message: string): void {
    commit("success", message);
  },
  error({ commit }, message: string): void {
    console.log(message);
    commit("error", message);
  },
  clear({ commit }): void {
    commit("clear");
  }
};
