import store from '@/store';
import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const roleAdmin = "Admin".toLowerCase();
const roleWorker = "Worker".toLowerCase();
const roleUser = "User".toLowerCase();

function authGuard(to: any, from: any, next: any) {
  const stateRole = store.state.rol.toLowerCase();
  if (store.state.rol && stateRole !== roleAdmin) {
    next();
  } else if(store.state.rol && stateRole === roleAdmin) {
    next('/admin');
  } else {
    next('/');
  }
}
function userGuard(to: any, from: any, next: any) {
  const stateRole = store.state.rol.toLowerCase();
  if (store.state.rol && stateRole === roleUser) {
    next();
  } else if(store.state.rol && stateRole !== roleUser) {
    next('/');
  } else {
    next('/');
  }
}
function workerGuard(to: any, from: any, next: any) {
  const stateRole = store.state.rol.toLowerCase();
  if (store.state.rol && stateRole === roleWorker) {
    next();
  } else if(store.state.rol && stateRole !== roleWorker) {
    next('/');
  } else {
    next('/');
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
    component: Home
  },
  {
    path: '/contacts',
    name: 'Contacts',
    beforeEnter: workerGuard,
    component: () => import('../views/Contacts.vue'),
  
  },
  {
    path: '/new',
    name: 'New Company',
    beforeEnter: workerGuard,
    component: () => import('../views/NewCompany.vue'),
  },
  {
    path: '/update',
    name: 'Update Company',
    beforeEnter: workerGuard,
    component: () => import('../views/UpdateCompany.vue'),
  },
  {
    path: '/companies',
    name: 'Companies',
    beforeEnter: userGuard,
    component: () => import('../views/Companies.vue'),
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
