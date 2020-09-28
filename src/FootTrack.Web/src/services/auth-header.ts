import { User } from '../models/User';


export default function authHeader() {
    const userStorageName = 'user';

    let user = JSON.parse(localStorage.getItem(userStorageName)) as User;

    if (user && user.token) {
        return { Authorization: 'Bearer' + user.token }
    } else {
        return {};
    }
}