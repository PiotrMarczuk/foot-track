import { ActionTree } from "vuex";
import { ProfileState } from "./types";
import { RootState } from "../types";
import { UserLogin } from "@/models/UserLogin";
import { userService } from "@/services";
import { UserRegister } from "@/models/UserRegister";

export const actions: ActionTree<ProfileState, RootState> = {
  login({ dispatch, commit }, userLogin: UserLogin) {
    commit("loginRequest", userLogin);
    userService.login(userLogin).then(
      user => {
        commit("loginSuccess", user);
        commit("form/setLoginFormVisible", false, { root: true });
      },
      error => {
        commit("loginFailure", error);
        dispatch("alert/error", error, { root: true });
        commit("form/setLoginFormVisible", false, { root: true });
      }
    );
  },
  register({ dispatch, commit }, userRegister: UserRegister) {
    commit("registerRequest", userRegister);
    userService.register(userRegister).then(
      user => {
        commit("registerSuccess", user);
        commit("form/setRegisterFormVisible", false, { root: true });
      },
      error => {
        commit("registerFailure", error);
        dispatch("alert/error", error, { root: true });
        commit("form/setRegisterFormVisible", false, { root: true });
      }
    );
  },
  logout({ commit }) {
    userService.logout();
    commit("logout");
  }
};
