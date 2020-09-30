import { MutationTree } from 'vuex';
import { ProfileState, User } from './types';

export const mutations: MutationTree<ProfileState> = {
    loginRequest(state) {
        state.status = { loggedIn: false, loggingIn: true, registering: false };
        state.user = undefined;
    },
    loginSuccess(state, payload: User) {
        state.status = { loggedIn: true, loggingIn: false, registering: false };
        state.user = payload;
    },
    loginFailure(state) {
        state.status = { loggedIn: false, loggingIn: false, registering: false };
        state.user = undefined;
    },
    logout(state) {
        state.status = { loggedIn: false, loggingIn: false, registering: false };
        state.user = undefined;
    },
    registerRequest(state) {
        state.status = { loggedIn: false, loggingIn: false, registering: true };
        state.user = undefined;
    },
    registerSuccess(state, payload: User) {
        state.status = { loggedIn: true, loggingIn: false, registering: false };
        state.user = payload;
    },
    registerFailure(state) {
        state.status = { loggedIn: false, loggingIn: false, registering: false };
        state.user = undefined;
    }
};