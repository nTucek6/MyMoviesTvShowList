export class ErrorResponse {
  Message: string
  StatusCode: number

  constructor() {
    this.Message = ''
    this.StatusCode = 0
  }
}
