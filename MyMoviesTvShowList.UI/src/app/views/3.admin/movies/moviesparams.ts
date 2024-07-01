import type { LinkParams } from '@/app/shared/models/link-params'

export const moviesParams: LinkParams[] = [
  {
    route: '/admin/moviesadmin',
    title: 'View all movies'
  },
  {
    route: '/admin/addeditmovie',
    title: 'Add movie'
  }
]
