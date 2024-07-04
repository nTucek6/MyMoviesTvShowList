export default class UserDTO {
  Id: number
  Username: string
  Email: string
  Role: string

  constructor() {
    this.Id = 0
    this.Username = ''
    this.Email = ''
    this.Role = ''
  }
}
