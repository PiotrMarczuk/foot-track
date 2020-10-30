export class UserRegister {
  private _email = "";
  private _password = "";
  private _firstName = "";
  private _lastName = "";

  set email(value: string) {
    this._email = value;
  }

  get email(): string {
    return this._email;
  }

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
