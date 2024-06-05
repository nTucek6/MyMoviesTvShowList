import { Select } from './select.model'
import { PersonDTO } from './person.model'
import type { ActorDTO } from './actor.model'

export class TVShowDTO{
    Id: number
    Title: string
    Description: string
    Rating: number
    Genres: Select[]
    Creators: PersonDTO[]
    Actors: ActorDTO[]
    Runtime: string
    TotalSeason:number
    TotalEpisode:number
    RatingsCount: number
    TVShowImageData: Blob
  
    constructor() {
      this.Id = 0
      this.Title = ''
      this.Description = ''
      this.Rating = 0
      this.Genres = []
      this.Creators = []
      this.Actors = []
      this.Runtime = ''
      this.RatingsCount = 0
      this.TotalSeason = 0
      this.TotalEpisode  = 0
      this.TVShowImageData = new Blob([]);
    }
}