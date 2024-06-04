import type { LinkParams } from "@/app/shared/models/link-params"

export const moviesParams: LinkParams[] = [
    {
      route: '/moviesadmin',
      title: 'View all movies'
    },
    {
      route: '/addeditmovie',
      title: 'Add movie'
    }
  ]