import { ActionTree } from "vuex";
import { ProfileState } from "./types";
import { RootState } from "../types";
import { UserLogin } from "@/models/UserLogin";
import { userService } from "@/services";
import { UserRegister } from "@/models/UserRegister";
import router from '@/router';

export const actions: ActionTree<ProfileState, RootState> = {
  login({ dispatch, commit }, userLogin: UserLogin) {
    commit("loginRequest", userLogin);

    userService.login(userLogin).then(
      user => {
        commit("loginSuccess", user.result);
        const token = user.result.token;
        localStorage.setItem('user-token', token);
        localStorage.setItem('user', JSON.stringify(user.result));
        commit("form/setLoginFormVisible", false, { root: true });
      },
      error => {
        localStorage.removeItem('user-token');
        commit("loginFailure", error);
        dispatch("alert/error", error.response.data.errors.message, { root: true });
        commit("form/setLoginFormVisible", false, { root: true });
      }
    );
  },
  register({ dispatch, commit }, userRegister: UserRegister) {
    commit("registerRequest", userRegister);
    userService.register(userRegister).then(
      user => {
        commit("registerSuccess", user);
        const token = user.result.token;
        localStorage.setItem('user-token', token);
        localStorage.setItem('user', JSON.stringify(user.result));
        commit("form/setRegisterFormVisible", false, { root: true });
      },
      error => {
        localStorage.removeItem('user-token');
        commit("registerFailure", error);
        dispatch("alert/error", error.response.data.errors.message, { root: true });
        commit("form/setRegisterFormVisible", false, { root: true });
      }
    );
  },
  logout({ commit }) {
    userService.logout();
    if (router.currentRoute.path != "/") {
      router.push('/');
    }
    commit("logout");
  }
};
