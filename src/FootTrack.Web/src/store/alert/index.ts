import { actions } from './actions';
import { mutations } from './mutations';
import { AlertState } from './types';

export const state: AlertState = {
    type: undefined,
    message: undefined,
};

const namespaced = true;
export const alert = {
    namespaced,
    state,
    actions,
    mutations
};