export class SaveTVShowDTO {
  Id: number
  Title: string
  Description: string
  Genres: string

  Director: string
  Writers: string
  Actors: string

  ReleaseDate: string
  TVShowImageData: Blob
  TotalSeason: number
  TotalEpisode: number

  constructor() {
    this.Id = 0
    this.Title = ''
    this.Description = ''
    this.Genres = ''
    this.Director = ''
    this.Writers = ''
    this.Actors = ''
    this.ReleaseDate = ''
    this.TVShowImageData = new Blob([])
    this.TotalSeason = 0
    this.TotalEpisode = 0
  }
}
