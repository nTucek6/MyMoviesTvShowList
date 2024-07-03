import { useAuthentication } from '@/stores/admin/authentication'
import { computed } from 'vue'

const baseRoute: string = '/admin/login'

export const admin = [
  {
    name: 'Admin Login',
    path: '/admin/login',
    component: () => import('@/app/views/3.admin/AdminLoginView.vue')
  },
  {
    name: 'Admin',
    path: '/admin',
    component: () => import('@/AdminLayout.vue'),
    beforeEnter: CheckAdminLogin,
    children: [
      {
        path: 'moviesadmin',
        name: 'Movies Admin',
        component: () => import('@/app/views/3.admin/movies/MoviesAdminView.vue'),
        beforeEnter: CheckAdminLogin,
        children: [
          {
            path: 'addeditmovie',
            name: 'Add & Edit movie',
            component: () => import('@/app/views/3.admin/movies/AddEditMovieView.vue'),
            beforeEnter: CheckAdminLogin
          }
        ]
      },

      {
        path: 'viewcrew',
        name: 'View Crew',
        component: () => import('@/app/views/3.admin/crew/CrewsAdminView.vue'),
        beforeEnter: CheckAdminLogin,
        children: [
          {
            path: 'addeditperson',
            name: 'Add & Edit person',
            component: () => import('@/app/views/3.admin/crew/AddEditCrewView.vue'),
            props: true,
            beforeEnter: CheckAdminLogin
          }
        ]
      },
      {
        path: 'tvshowadmin',
        name: 'TvShow Admin',
        component: () => import('@/app/views/3.admin/tvshow/TvShowAdminView.vue'),
        props: true,
        beforeEnter: CheckAdminLogin,
        children: [
          {
            path: 'addedittvshow',
            name: 'Add & Edit TVShow',
            component: () => import('@/app/views/3.admin/tvshow/AddEditTvShowView.vue'),
            props: true,
            beforeEnter: CheckAdminLogin
          }
        ]
      }
    ]
  }
]

function CheckAdminLogin(to: any, from: any, next: any) {
  const isAdminAuthenticated = computed(() => useAuthentication().adminLogIn)
  if (isAdminAuthenticated.value) {
    next()
  } else {
    next(baseRoute)
  }
}
