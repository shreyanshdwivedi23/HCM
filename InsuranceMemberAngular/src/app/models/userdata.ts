interface Dictionary<T> {
    [Key: string]: T;
}
export class UserData{
    userId: number=0;
    userName:string='';
    userPassword:string='';
    userConfirmPassword:string='';
    userRole:string='';
    isRegister:boolean=false;
    // lastName:string='';
    // roleCategory:string='';
    // email:string='';
    // state:string='';
    // address:string='';
    // city:string='';
    // dateOfBirth: Date = new Date() ;
}