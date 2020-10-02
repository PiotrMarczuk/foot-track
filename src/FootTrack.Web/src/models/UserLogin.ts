export class UserLogin {
  private _email = "";
  private _password = "";

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
}
