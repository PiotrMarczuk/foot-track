export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  token: string;
}

export interface UserStatus {
  loggingIn: boolean;
  loggedIn: boolean;
  registering: boolean;
}

export interface ProfileState {
  user?: User;
  status?: UserStatus;
}
