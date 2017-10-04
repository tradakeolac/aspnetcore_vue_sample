import Vue from 'vue'
import Vuex from 'vuex'
import qs from 'qs'
import { routes } from './../../../routes'
import { http } from './../../api'

Vue.use(Vuex)

const state = {
    registerModel: {
                    email: '',
                    password: '',
                    confirmPassword: ''
                }
}

const getters = {
    registerModel: state => state.registerModel
}

const mutations = {
    register(state) {

    }
}

const actions = {
    register({ context }, model) {
        http.post('/accounts/register', model)
            .then((response) => {
                context.commit('register')
            })
            .catch((error) => {
                console.log(error)
            })
    },
}

export default {
    state,
    getters,
    actions,
    mutations,
}
