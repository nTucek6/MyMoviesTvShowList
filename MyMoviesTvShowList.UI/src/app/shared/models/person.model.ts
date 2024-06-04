export class PersonDTO{
    Id:number;
    FirstName:string;
    LastName:string;
    BirthDate:string;
    BirthPlace:string;
    PersonImageData:string;

    constructor(){
        this.Id = 0;
        this.FirstName = '';
        this.LastName = '';
        this.BirthDate = ''
        this.BirthPlace = '';
        this.PersonImageData = ''
    }
}