import Vue from 'vue'
import Vuex from 'vuex'
import qs from 'qs'
import { routes } from './../../../routes'
import { http } from './../../api'

Vue.use(Vuex)

const state = {
    loginModel: {
        email: '',
        password: '',
        remember: false
    }
}

const getters = {
    loginModel: state => state.loginModel
}

const mutations = {

}

const actions = {
    login({ context }, model) {
        let requestPayload = qs.stringify({
            'username': model.email,
            'password': model.password,
            'grant_type': 'password'
        });

        http.post('http://localhost:5001/connect/token',requestPayload , { headers: {'Content-Type': 'application/x-www-form-urlencoded'}, data: {} })
            .then((response) => {
                localStorage.setItem('__TOKEN__', JSON.stringify(response.data));
            })
            .catch((error) => {
                console.log(error);
            })
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
}
