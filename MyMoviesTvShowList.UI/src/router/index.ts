import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'
import { user } from './user'
import { admin } from './admin'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  linkActiveClass: 'active',
  routes: [...user, ...admin]
})

export default router
