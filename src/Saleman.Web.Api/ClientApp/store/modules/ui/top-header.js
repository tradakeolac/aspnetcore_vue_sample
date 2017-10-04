import Vue from 'vue'
import Vuex from 'vuex'
import qs from 'qs'
import { routes } from './../../../routes'
import { http } from './../../api'

Vue.use(Vuex)

const state = {
    storeInformation: []
}

const getters = {
    socialInformation: state => state.storeInformation.filter((el) => el.type == 'social'),
    contactInformation: state => state.storeInformation.filter((el) => el.type != 'social')
}

const mutations = {
    register(state, { information }) {
        state.storeInformation = information
    }
}

const actions = {
    fetchStoreInformation({ commit }, storeId) {
        http.get(`stores/${storeId}/contactinformation`)
            .then((response) => {
                commit('register', { information: response.data })
            })
            .catch((error) => {
                console.log(error)
            })
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
}
