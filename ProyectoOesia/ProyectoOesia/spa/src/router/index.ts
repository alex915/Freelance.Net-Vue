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
    path: '/new',
    name: 'New Company',
    component: () => import( '../views/NewCompanyView.vue')
  },{
    path: '/search',
    name: 'Search',
  
    component: () => import('../views/Search.vue')
  },
  {
    path: '/profile',
    name: 'UpdateUser',
  
    component: () => import('../views/auth/UpdateUser.vue')
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
