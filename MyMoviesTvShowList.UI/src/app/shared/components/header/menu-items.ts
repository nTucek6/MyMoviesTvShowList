import type { DropdownMenu } from '@/app/shared/models/dropdown-menu'

export const dropdownMenu: DropdownMenu[] = [
  {
    name: 'Movies',
    children: [
      {
        name: 'Movies Search',
        route: '/moviessearch'
      },
      {
        name: 'Top Movies',
        route: 'topmovies'
      }
    ]
  },
  {
    name: 'Communitiy',
    children: []
  }
];