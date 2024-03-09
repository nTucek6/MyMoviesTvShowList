export class UserRegisterDTO {
  Email: string
  Username: string
  Password: string

  constructor() {
    this.Email = ''
    this.Username = ''
    this.Password = ''
  }
}
