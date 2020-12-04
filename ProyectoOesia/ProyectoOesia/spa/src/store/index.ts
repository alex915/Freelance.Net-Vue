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
    user: {},
  },
  mutations: {
    setToken: (state, token) => {
      state.token = token;
    },
    setUser: (state, user) => {
      state.user = user;
    }
  },
  actions: {
    setToken: (state, token) => {
      state.commit('setToken', token);
    },
    setUser: (state, user) => {
      state.commit('setUser', user);
    }
  },
  getters: {
    getToken: (state) => {
      return state.token;
    },
    getUser: (state) => {
      return state.user;
    }
  },
  plugins: [vuexLocal.plugin]
})
