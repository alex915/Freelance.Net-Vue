import store from '@/store';
import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

function authGuard(to: any, from: any, next: any){
  if(store.state.token) {
      next();
  } else{
      next('/login');
  }
}

function noAuthGuard(to: any, from: any, next: any){
  if(store.state.token) {
    next('/');
  } else{
    next();
  }
}

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    beforeEnter: authGuard,
    component: Home
  },
  {
    path: '/search',
    name: 'Search',
    beforeEnter: authGuard,
    component: () => import('../views/Search.vue'),
  },
  {
    path: '/profile',
    name: 'UpdateUser',
    beforeEnter: authGuard,
    component: () => import('../views/auth/UpdateUser.vue')
  },
    {
      path: '/login',
      name: 'Login',
      beforeEnter: noAuthGuard,
      component: () => import('../views/auth/Login.vue')
  },
    {
      path: '/register',
      name: 'Register',
      beforeEnter: noAuthGuard,
      component: () => import('../views/auth/Register.vue')
  },
  {
    path: '**',
    redirect: 'Home',
  }
]

const router = new VueRouter({
  routes
})

export default router
