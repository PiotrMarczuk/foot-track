import { getters } from "./getters";
import { actions } from "./actions";
import { mutations } from "./mutations";
import { ProfileState } from "./types";

const user = JSON.parse(localStorage.getItem("user") || "{}");

export const state: ProfileState = user
  ? {
      user: user,
      status: { loggedIn: true, loggingIn: false, registering: false }
    }
  : { user: undefined, status: undefined };

const namespaced = true;

export const profile = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
