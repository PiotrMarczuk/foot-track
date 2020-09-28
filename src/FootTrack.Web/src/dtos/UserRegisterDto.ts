
export class UserRegisterDto {
    email: string;
    password: string;
    firstName: string;
    lastName: string;


    constructor() {
        this.email = '';
        this.password = '';
        this.firstName = '';
        this.lastName = '';
    }
}

