import { computed } from 'vue'
import { useAuthentication } from '@/stores/admin/authentication'
import FrontpageView from '@/app/views/1.public/FrontpageView.vue'
import MainLayout from '@/MainLayout.vue'

export const user = [
  {
    path: '/',
    name: 'MainLayout',
    component: MainLayout,
    children: [
      {
        path: '/',
        name: 'Frontpage',
        component: FrontpageView
      },
      {
        path: '/login',
        name: 'Login',
        component: () => import('@/app/views/1.public/LoginView.vue'),
        beforeEnter: (to: any, from: any, next: any) => {
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
        beforeEnter: (to: any, from: any, next: any) => {
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
      },
      {
        path: '/accountsettings',
        name: 'Account settings',
        component: () => import('@/app/views/2.user/AccountSettings.vue')
      },
      {
        path: '/movie/:id/:title',
        name: 'Movie Info',
        component: () => import('@/app/views/1.public/MovieInfo.vue')
      }
    ]
  }
]
