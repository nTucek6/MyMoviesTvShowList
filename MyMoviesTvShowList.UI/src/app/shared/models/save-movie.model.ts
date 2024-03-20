export class SaveMovieDTO{
    Id: number
    MovieName: string
    Synopsis: string
    Genres: string
    Director: string
    Writers: string
    Actors: string
    ReleaseDate: string
    Duration: number 
    MovieImageData: Blob 

    constructor(){
        this.Id= 0;
        this.MovieName= '';
        this.Synopsis= '';
        this.Genres= '';
        this.Director= '';
        this.Writers= '';
        this.Actors= '';
        this.ReleaseDate= ''; 
        this.Duration= 0; 
        this.MovieImageData= new Blob([]); 
    }

}