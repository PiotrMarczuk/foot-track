export class User {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    token: string;

    constructor() {
        this.id = '';
        this.email = '';
        this.firstName = '';
        this.lastName = '';
        this.token = '';
    }
}