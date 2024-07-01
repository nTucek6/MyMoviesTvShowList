export class SaveTVShowDTO {
  Id: number
  Title: string
  Description: string
  Genres: string

  //Director: string
  //Writers: string
  Creators: string
  Actors: string

  Runtime: string
  TVShowImageData: Blob
  TotalSeason: number
  TotalEpisode: number

  constructor() {
    this.Id = 0
    this.Title = ''
    this.Description = ''
    this.Genres = ''
    //this.Director = ''
    //this.Writers = ''
    this.Creators = ''
    this.Actors = ''
    this.Runtime = ''
    this.TVShowImageData = new Blob([])
    this.TotalSeason = 0
    this.TotalEpisode = 0
  }
}
