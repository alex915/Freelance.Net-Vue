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
    spinners: 0,
  },
  mutations: {
    setToken: (state, token) => {
      state.token = token;
    },
    setUser: (state, user) => {
      state.user = user;
    },
    showSpinner(state) {
      state.spinners++;
    },
    removeSpinner(state) {
      if (state.spinners > 0) {
        state.spinners--;
      }
    },
  },
  actions: {
    setToken: (state, token) => {
      state.commit('setToken', token);
    },
    setUser: (state, user) => {
      state.commit('setUser', user);
    },
    showSpinner: (state) => {
      state.commit('showSpinner');
    },
    removeSpinner: (state) => {
      state.commit('removeSpinner');
    },
  },
  getters: {
    getToken: (state) => {
      return state.token;
    },
    getUser: (state) => {
      return state.user;
    },
    spinners: (state) => {
      return state.spinners;
    }
  },
  plugins: [vuexLocal.plugin]
})
