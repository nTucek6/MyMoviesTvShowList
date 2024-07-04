
export class ChangeWatchStatusDTO{
    Id:number
    UserId:number
    Status:number
    Score?:number
    Review?:string

    constructor()
    {
        this.Id = 0,
        this.UserId = 0,
        this.Status = 0
    }

}