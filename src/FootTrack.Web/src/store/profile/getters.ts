import { GetterTree } from "vuex";
import { ProfileState } from "./types";
import { RootState } from "../types";

export const getters: GetterTree<ProfileState, RootState> = {
  user(state) {
    const { user } = state;
    return user;
  },
  userStatus(state){
    const { status } = state;
    return status;
  },
};
