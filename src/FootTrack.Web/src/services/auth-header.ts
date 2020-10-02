export default function authHeader() {
  const userStorageName = "user";

  const user = JSON.parse(localStorage.getItem(userStorageName) || "{}");

  if (user && user.token) {
    return { Authorization: "Bearer" + user.token };
  } else {
    return {};
  }
}
