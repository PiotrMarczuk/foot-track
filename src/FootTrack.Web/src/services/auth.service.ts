import axios from 'axios';
import { AxiosResponse } from 'axios';
import { UserLoginDto } from '../dtos/UserLoginDto';
import { UserRegisterDto } from '../dtos/UserRegisterDto';

const API_URL = "http://localhost:5000/api/v1/users/";
const userStorageName = 'user';

export class AuthService {
    
    async login(user: UserLoginDto) {
        const response = await axios.post(API_URL + 'login', user);
        console.log(response);
        this.addUserToLocalStorage(response);
    }

    logout() {
        localStorage.removeItem(userStorageName);
    }

    async register(user: UserRegisterDto) {
        const response = await axios.post(API_URL + 'register', user)
        console.log(response);
        this.addUserToLocalStorage(response);
    }

    private addUserToLocalStorage(response: AxiosResponse<any>): void {
        if (response.data) {
            localStorage.setItem(userStorageName, JSON.stringify(response.data));
        }
    }
}


