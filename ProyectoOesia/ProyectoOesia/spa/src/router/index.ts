import store from '@/store';
import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const roleAdmin = "Admin".toLowerCase();

function authGuard(to: any, from: any, next: any) {
  const stateRole = store.state.rol.toLowerCase();
  if (store.state.rol && stateRole !== roleAdmin) {
    next();
  } else if(store.state.rol && stateRole === roleAdmin) {
    next('/admin');
  } else {
    next('/login');
  }
}

function adminGuard(to: any, from: any, next: any) {
  const stateRole = store.state.rol.toLowerCase();
  if (!store.state.token) {
    next('/login');
  } else if(stateRole === roleAdmin) {
    next();
  } else {
    next('/')
  }
}

function noAuthGuard(to: any, from: any, next: any) {
  if (!store.state.token) {
    next();
  } else {
    next('/');
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
    path: '/new',
    name: 'New Company',
    beforeEnter: authGuard,
    component: () => import('../views/NewCompany.vue'),
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
    path: '/admin',
    name: 'Admin',
    beforeEnter: adminGuard,
    component: () => import('../views/admin/Admin.vue')
  },
  {
    path: '/users',
    name: 'Users',
    beforeEnter: adminGuard,
    component: () => import('../views/admin/Users.vue')
  },
  {
    path: '*',
    redirect: '/',
  }
]

const router = new VueRouter({
  routes
})

export default router
