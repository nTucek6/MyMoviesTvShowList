import { Select } from './select.model'
import { PersonDTO } from './person.model'
import type { ActorDTO } from './actor.model'

export class MoviesDTO {
  Id: number
  MovieName: string
  Synopsis: string
  Rating: number
  Genres: Select[]
  Director: PersonDTO[]
  Writers: PersonDTO[]
  Actors: ActorDTO[]
  ReleaseDate: string
  Duration: number
  RatingsCount: number
  MovieImageData: string
  //MovieImageData: Blob

  constructor() {
    this.Id = 0
    this.MovieName = ''
    this.Synopsis = ''
    this.Rating = 0
    this.Genres = []
    this.Director = []
    this.Writers = []
    this.Actors = []
    this.ReleaseDate = ''
    this.Duration = 0
    this.RatingsCount = 0
    this.MovieImageData = ''
    //this.MovieImageData = new Blob([])
  }
}
