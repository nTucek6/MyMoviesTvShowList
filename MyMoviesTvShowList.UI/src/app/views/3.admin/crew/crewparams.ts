import type { LinkParams } from '@/app/shared/models/link-params'

export const crewParams: LinkParams[] = [
  {
    route: '/admin/viewcrew',
    title: 'View all crew'
  },
  {
    route: '/admin/viewcrew/addeditperson',
    title: 'Add person'
  }
]
