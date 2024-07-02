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
    children: [
      {
        path: 'moviesadmin',
        name: 'Movies Admin',
        component: () => import('@/app/views/3.admin/movies/MoviesAdminView.vue'),
       /* beforeEnter: (to: any, from: any, next: any) => {
          const isAdmin = computed(() => useAuthentication().IsAdmin())
          if (!isAdmin.value) {
            next(baseRoute)
          } else {
            next()
          }
        }*/
       children: [
        {
          path: 'addeditmovie',
          name: 'Add & Edit movie',
          component: () => import('@/app/views/3.admin/movies/AddEditMovieView.vue'),
         /* beforeEnter: (to: any, from: any, next: any) => {
            const isAdmin = computed(() => useAuthentication().IsAdmin())
            //console.log(isAdmin.value);
            if (!isAdmin.value) {
              next(baseRoute)
            } else {
              next()
            }
          } */
        },
       ],
      },
   
      {
        path: 'viewcrew',
        name: 'View Crew',
        component: () => import('@/app/views/3.admin/crew/CrewsAdminView.vue'),
        /*beforeEnter: (to: any, from: any, next: any) => {
          const isAdmin = computed(() => useAuthentication().IsAdmin())
          if (!isAdmin.value) {
            next(baseRoute)
          } else {
            next()
          }
        }*/
       children:[
        {
          path: 'addeditperson',
          name: 'Add & Edit person',
          component: () => import('@/app/views/3.admin/crew/AddEditCrewView.vue'),
          props: true,
         /* beforeEnter: (to: any, from: any, next: any) => {
            const isAdmin = computed(() => useAuthentication().IsAdmin())
            if (!isAdmin.value) {
              next(baseRoute)
            } else {
              next()
            }
          }*/
        },
       ]
      },
    
      {
        path: 'tvshowadmin',
        name: 'TvShow Admin',
        component: () => import('@/app/views/3.admin/tvshow/TvShowAdminView.vue'),
        props: true,
        /*beforeEnter: (to: any, from: any, next: any) => {
          const isAdmin = computed(() => useAuthentication().IsAdmin())
          if (!isAdmin.value) {
            next(baseRoute)
          } else {
            next()
          }
        }*/
       children:[
        {
          path: 'addedittvshow',
          name: 'Add & Edit TVShow',
          component: () => import('@/app/views/3.admin/tvshow/AddEditTvShowView.vue'),
          props: true,
         /* beforeEnter: (to: any, from: any, next: any) => {
            const isAdmin = computed(() => useAuthentication().IsAdmin())
            if (!isAdmin.value) {
              next(baseRoute)
            } else {
              next()
            }
          }*/
        }
       ]
      },
    ]
  }
]
