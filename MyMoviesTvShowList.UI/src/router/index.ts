import { createRouter, createWebHistory } from 'vue-router'
import FrontpageView from '../views/FrontpageView.vue'
import MoviesAdminView from '@/views/MoviesAdminView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'frontpage',
      component: FrontpageView
    },
   /* {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    } */
    {
      path:'/moviesadmin',
      name:'movies admin',
      component: MoviesAdminView
    },
    {
      path:'/login',
      name:"login",
      component: LoginView
    },
    {
      path:'/register',
      name:"register",
      component: RegisterView
    }
  ]
})

export default router
