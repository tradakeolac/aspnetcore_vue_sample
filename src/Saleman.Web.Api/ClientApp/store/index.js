import Vue from 'vue'
import Vuex from 'vuex'
import register from './modules/accounts/register'
import login from './modules/accounts/login'
import topheader from './modules/ui/top-header'
import midheader from './modules/ui/mid-header'
import bottomheader from './modules/ui/bottom-header'

Vue.use(Vuex)

const INITIAL_STATE = {
    loggedIn: false,
    loggedInUser: {}
};

export default new Vuex.Store({
    INITIAL_STATE,
    modules: {
        topheader,
        midheader,
        bottomheader,
        login,
        register
    }
});
