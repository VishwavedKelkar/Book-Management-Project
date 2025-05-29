import React, { createContext, useEffect, useState } from "react";

const AuthContext = createContext({
  token: null,
  user: null,
  login: () => {},
  logout: () => {},
});

export function AuthProvider({ children }) {
  const [token, setToken] = useState(null);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true); 

  useEffect(() => {
    const storedToken = localStorage.getItem("token");
    const storedUser = localStorage.getItem("user");

    if (storedToken && storedUser) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));
    }

    setLoading(false);
  }, []);

  const login = (newToken, userInfo) => {
    localStorage.setItem("token", newToken);
    localStorage.setItem("user", JSON.stringify(userInfo));
    localStorage.setItem("role", userInfo.role);
    localStorage.setItem("username", userInfo.username); 
    setToken(newToken);
    setUser(userInfo);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    localStorage.removeItem("role");
    localStorage.removeItem("username");
    setToken(null);
    setUser(null);
  };

  if (loading) return <div className="text-center mt-8">Loading user data...</div>;

  return (
    <AuthContext.Provider value={{ token, user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export default AuthContext;
