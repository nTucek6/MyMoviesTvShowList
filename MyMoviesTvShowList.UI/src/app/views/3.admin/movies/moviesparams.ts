import type { LinkParams } from '@/app/shared/models/link-params'

export const moviesParams: LinkParams[] = [
  {
    route: '/admin/moviesadmin',
    title: 'View all movies'
  },
  {
    route: '/admin/moviesadmin/addeditmovie',
    title: 'Add movie'
  }
]
