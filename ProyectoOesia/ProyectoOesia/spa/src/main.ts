import Vue from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import store from './store';
import {TokenInterceptor} from '@/interceptors/token.interceptor'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'
import {spinnerPlugin} from '@/plugins/SpinnerPlugin.ts'

Vue.config.productionTip = false;
Vue.use(Vuetify);
Vue.use(spinnerPlugin);
TokenInterceptor();

new Vue({
  vuetify: new Vuetify({}),
  router,
  store,
  render: h => h(App)
}).$mount('#app');
