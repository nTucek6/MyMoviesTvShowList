export class SavePersonDTO {
  Id: number
  FirstName: string
  LastName: string
  BirthDate: string
  BirthPlace: string
  PersonImage: Blob
  constructor() {
    this.Id = 0
    this.FirstName = ''
    this.LastName = ''
    this.BirthDate = ''
    this.BirthPlace = ''
    this.PersonImage = new Blob()
  }
}
