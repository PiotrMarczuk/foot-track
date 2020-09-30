
import { required, minLength, email } from "@rxweb/reactive-forms";

export class UserLogin {
    private _email = '';
    private _password = '';

    @required()
    @email()
    set email(value: string) {
        this._email = value;
    }

    get email(): string {
        return this._email;
    }

    @required()
    @minLength({ value: 6 })
    set password(value: string) {
        this._password = value;
    }

    get password() {
        return this._password;
    }
}
