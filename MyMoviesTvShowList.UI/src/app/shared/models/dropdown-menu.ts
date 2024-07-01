export interface DropdownMenu {
  name: string
  children: ChildrenRoutes[]
}

interface ChildrenRoutes {
  name: string
  route: string
}
