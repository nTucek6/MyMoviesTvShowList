export class MediaListDTO {
  Id: number
  Title: string
  Type: string

  constructor() {
    ;(this.Id = 0), (this.Title = '')
    this.Type = ''
  }
}
