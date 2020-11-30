import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },{
    path: '/search',
    name: 'Search',
  
    component: () => import('../views/Search.vue')
  },
    {
      //ruta para acceder al login
      path: '/login',
      name: 'Login',
      component: () => import('../views/auth/Login.vue')
  },
    {
      //accede al registro de usuarios
      path: '/register',
      name: 'Register',
      component: () => import('../views/auth/Register.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
