import axios from "axios";
import { AxiosResponse } from "axios";
import { UserLogin } from "@/models/UserLogin";
import { UserRegister } from "@/models/UserRegister";
const API_URL = process.env.VUE_APP_APIURL + "users/";
const userStorageName = "user";

export class UserService {
  async login(user: UserLogin) {
    const response = await axios.post(API_URL + "login", {
      email: user.email,
      password: user.password
    });
    this.addUserToLocalStorage(response);
    return response.data;
  }

  logout(): void {
    localStorage.removeItem(userStorageName);
  }

  async register(user: UserRegister) {
    const response = await axios.post(API_URL + "register", {
      email: user.email,
      password: user.password
    });
    this.addUserToLocalStorage(response);

    return response.data;
  }

  private addUserToLocalStorage(response: AxiosResponse<any>): void {
    if (response.data) {
      localStorage.setItem(userStorageName, JSON.stringify(response.data));
    }
  }
}
