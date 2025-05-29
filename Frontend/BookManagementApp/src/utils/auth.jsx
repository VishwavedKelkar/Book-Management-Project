export function getToken() {
  return localStorage.getItem("token");
}

export function getUserRole() {
  return localStorage.getItem("role");
}

export function isAdmin() {
  return getUserRole() === "Admin";
}

export function isLoggedIn() {
  return !!getToken();
}
