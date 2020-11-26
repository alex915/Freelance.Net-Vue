import Vue from 'vue'
import Vuex from 'vuex'
import VuexPersistence from 'vuex-persist'

Vue.use(Vuex)

const vuexLocal = new VuexPersistence<any>({
  storage: window.localStorage
})

export default new Vuex.Store({
  state: {
    token: '',
  },
  mutations: {
    setToken: (state, token) => {
      state.token = token;
    }
  },
  actions: {
    setToken: (state, token) => {
      state.commit('setToken', token);
    }
  },
  getters: {
    getToken: (state) => {
      return state.token;
    }
  },
  plugins: [vuexLocal.plugin]
})
