export function getToken() {
  return localStorage.getItem("token");
}

export function getUserRole() {
  const user = JSON.parse(localStorage.getItem("user"));
  return user?.role || null;
}


export function isAdmin() {
  return getUserRole() === "Admin";
}

export function isLoggedIn() {
  return !!getToken();
}
