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
    rol: '',
    user: {},
    spinners: 0,
  },
  mutations: {
    setToken: (state, token) => {
      state.token = token;
    },
    setRol: (state, rol) => {
      state.rol = rol;
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
    setRol: (state, rol) => {
      state.commit('setRol', rol);
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
    token: (state) => {
      return state.token;
    },
    rol: (state) => {
      return state.token;
    },
    user: (state) => {
      return state.user;
    },
    spinners: (state) => {
      return state.spinners;
    }
  },
  plugins: [vuexLocal.plugin]
})
