import { MutationTree } from 'vuex';
import { FormState } from './types';

export const mutations: MutationTree<FormState> = {
    setLoginFormVisible(state, value) {
        state.loginFormVisible = value;
        state.registerFormVisible = false;
    },
   setRegisterFormVisible(state, value){
       state.registerFormVisible = value;
       state.loginFormVisible = false;
   }
};