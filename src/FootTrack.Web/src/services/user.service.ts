import axios from "axios";
import { UserLogin } from "@/models/UserLogin";
import { UserRegister } from "@/models/UserRegister";
const API_URL = process.env.VUE_APP_APIURL + "users";

export class UserService {
  async login(user: UserLogin) {
    const response = await axios.post(API_URL + "/login", {
      email: user.email,
      password: user.password
    });
    
    return response.data;
  }

  logout(): void {
    localStorage.removeItem('user-token');
    localStorage.removeItem('user');
    delete axios.defaults.headers.common['Authorization'];
  }

  async register(user: UserRegister) {
    const response = await axios.post(API_URL + "/register", {
      email: user.email,
      password: user.password
    });

    return response.data;
  }
}
