import { createRouter, createWebHistory } from 'vue-router'
import {computed} from 'vue'
import FrontpageView from '../views/1.public/FrontpageView.vue'
import MoviesAdminView from '@/views/3.admin/MoviesAdminView.vue'
import LoginView from '@/views/1.public/LoginView.vue'
import RegisterView from '@/views/1.public/RegisterView.vue'
import MovieSearchView from '@/views/1.public/MovieSearchView.vue'
import TopMoviesView from '@/views/1.public/TopMoviesView.vue'
import ProfileView from '@/views/1.public/ProfileView.vue'
import { useAuthentication } from '@/stores/Authentication/authentication'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  linkActiveClass: 'active',
  routes: [
    {
      path: '/',
      name: 'Frontpage',
      component: FrontpageView
    },
    {
      path:'/moviesadmin',
      name:'Movies Admin',
      component: MoviesAdminView,
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin());
        console.log(isAdmin.value);
        if (!isAdmin.value) {
          next('/');
        }
        else
        {
          next()
        } 
      },
    },
    {
      path:'/login',
      name:"Login",
      component: LoginView,
      beforeEnter: (to, from, next) => {
        const isAuthenticated = computed(() => useAuthentication().userLogIn);
        if (isAuthenticated.value) {
          next('/');
        }
        else
        {
          next()
        } 
      },
    },
    {
      path:'/register',
      name:"Register",
      component: RegisterView,
      beforeEnter: (to, from, next) => {
        const isAuthenticated = computed(() => useAuthentication().userLogIn);
        console.log(isAuthenticated.value);
        if (isAuthenticated.value) {
          next('/');
        }
        else
        {
          next()
        } 
      },
    },
    {
      path:'/moviessearch',
      name:"Movies Search",
      component: MovieSearchView
    },
    {
      path:'/topmovies',
      name:"Top Movies",
      component: TopMoviesView
    },
    {
      path:'/profile/:username',
      name:"Profile",
      component: ProfileView
    },
  ]
});

/*


const isAuthenticated = computed(() => authentication.userLogIn);


router.beforeEach((to, from, next) => {


  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (isAuthenticated) {
      // Redirect to the login page or another route
      next('/');
    } else {
      next();
    }
  } else {
    next();
  }
});

*/

export default router

   /* {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    } */