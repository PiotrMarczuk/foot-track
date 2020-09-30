export default function authHeader() {
    const userStorageName = 'user';

    let user = JSON.parse(localStorage.getItem(userStorageName) || '{}');

    if (user && user.token) {
        return { Authorization: 'Bearer' + user.token }
    } else {
        return {};
    }
}