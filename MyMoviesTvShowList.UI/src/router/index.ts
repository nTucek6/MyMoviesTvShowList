import { createRouter, createWebHistory } from 'vue-router'
import { computed } from 'vue'
import FrontpageView from '@/app/views/1.public/FrontpageView.vue'
import { useAuthentication } from '@/stores/authentication'

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
      path: '/login',
      name: 'Login',
      component: () => import('@/app/views/1.public/LoginView.vue'),
      beforeEnter: (to, from, next) => {
        const isAuthenticated = computed(() => useAuthentication().userLogIn)
        if (isAuthenticated.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/register',
      name: 'Register',
      component: () => import('@/app/views/1.public/RegisterView.vue'),
      beforeEnter: (to, from, next) => {
        const isAuthenticated = computed(() => useAuthentication().userLogIn)
        console.log(isAuthenticated.value)
        if (isAuthenticated.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/moviesadmin',
      name: 'Movies Admin',
      component: () => import('@/app/views/3.admin/movies/MoviesAdminView.vue'),
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/addeditmovie',
      name: 'Add & Edit movie',
      component: () => import('@/app/views/3.admin/movies/AddEditMovieView.vue'),
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        //console.log(isAdmin.value);
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/viewcrew',
      name: 'View Crew',
      component: () => import('@/app/views/3.admin/crew/CrewsAdminView.vue'),
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/addeditperson',
      name: 'Add & Edit person',
      component: () => import('@/app/views/3.admin/crew/AddEditCrewView.vue'),
      props: true,
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/tvshowadmin',
      name: 'TvShow Admin',
      component: () => import('@/app/views/3.admin/tvshow/TvShowAdminView.vue'),
      props: true,
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },
    {
      path: '/addedittvshow',
      name: 'Add & Edit TVShow',
      component: () => import('@/app/views/3.admin/tvshow/AddEditTvShowView.vue'),
      props: true,
      beforeEnter: (to, from, next) => {
        const isAdmin = computed(() => useAuthentication().IsAdmin())
        if (!isAdmin.value) {
          next('/')
        } else {
          next()
        }
      }
    },

    {
      path: '/moviessearch',
      name: 'Movies Search',
      component: () => import('@/app/views/1.public/MovieSearchView.vue')
    },
    {
      path: '/topmovies',
      name: 'Top Movies',
      component: () => import('@/app/views/1.public/TopMoviesView.vue')
    },
    {
      path: '/profile/:username',
      name: 'Profile',
      component: () => import('@/app/views/2.user/ProfileView.vue')
    },
    {
      path: '/movielist/:username',
      name: 'Movie List',
      component: () => import('@/app/views/2.user/MoviesListView.vue')
    }
  ]
})

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
