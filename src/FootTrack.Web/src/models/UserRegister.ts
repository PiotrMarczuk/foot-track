
import { required, minLength, email, compare, notEmpty } from "@rxweb/reactive-forms";

export class UserRegister {

    private _email = '';
    private _password = '';
    private _firstName = '';
    private _lastName = '';

    @required()
    @email()
    @notEmpty()
    set email(value: string) {
        this._email = value;
    }

    get email(): string {
        return this._email;
    }

    @required()
    @minLength({ value: 6 })
    @notEmpty()
    set password(value: string) {
        this._password = value;
    }

    get password() {
        return this._password;
    }

    set firstName(value: string) {
        this._firstName = value;
    }
    get firstName() {
        return this._firstName;
    }

    set lastName(value: string) {
        this._lastName = value;
    }
    get lastName() {
        return this._lastName;
    }
}

