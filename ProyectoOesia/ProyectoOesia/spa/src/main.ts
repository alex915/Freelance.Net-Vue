import Vue from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import store from './store';
import {TokenInterceptor} from '@/interceptors/token.interceptor'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'

Vue.config.productionTip = false;
Vue.use(Vuetify);
TokenInterceptor();

new Vue({
  vuetify: new Vuetify({}),
  router,
  store,
  render: h => h(App)
}).$mount('#app');
