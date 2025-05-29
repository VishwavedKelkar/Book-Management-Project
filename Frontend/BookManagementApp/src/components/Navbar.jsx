import { useContext, useState, useRef, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import AuthContext from "../auth/AuthContext";
import { LogOut, User, ChevronDown } from "lucide-react";

function Navbar() {
  const { user, logout } = useContext(AuthContext);
  const navigate = useNavigate();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const dropdownRef = useRef(null);

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setDropdownOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <nav className="bg-white border-b border-gray-300 shadow-sm sticky top-0 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex justify-between items-center">
        <Link
          to="/"
          className="text-xl font-semibold text-gray-800 tracking-tight hover:text-blue-700 transition"
        >
          📚 Book Management
        </Link>

        <div className="flex items-center gap-6">
          {user && (
            <>
              <Link
                to="/authors"
                className="text-sm font-medium text-gray-700 hover:text-blue-700 transition"
              >
                Authors
              </Link>
              <Link
                to="/genres"
                className="text-sm font-medium text-gray-700 hover:text-blue-700 transition"
              >
                Genres
              </Link>
            </>
          )}

          <div className="relative flex items-center gap-4" ref={dropdownRef}>
            {!user ? (
              <>
                <Link
                  to="/login"
                  className="text-sm font-medium px-4 py-2 rounded-md text-gray-700 hover:text-blue-700 hover:bg-gray-100 transition"
                >
                  Login
                </Link>
                <Link
                  to="/register"
                  className="text-sm font-medium px-4 py-2 rounded-md bg-blue-600 text-white hover:bg-blue-700 transition"
                >
                  Register
                </Link>
              </>
            ) : (
              <>
                <button
                  onClick={() => setDropdownOpen(!dropdownOpen)}
                  className="flex items-center gap-2 px-4 py-2 rounded-md bg-blue-600 text-white text-sm font-medium hover:bg-blue-700 transition"
                >
                  <User size={18} />
                  <span>{user?.username || "User"}</span>
                  <ChevronDown size={16} />
                </button>

                {dropdownOpen && (
                  <div className="absolute right-0 top-full mt-2 w-56 bg-white text-gray-900 rounded-md shadow-md border border-gray-200 py-2 z-50">
                    <div className="px-4 py-2">
                      <p className="font-semibold">{user?.username || "No Name"}</p>
                      <p className="text-sm text-gray-600">{user?.email || "No Email"}</p>
                    </div>
                    <hr className="my-2 border-gray-200" />
                    <button
                      onClick={handleLogout}
                      className="w-full text-left px-4 py-2 text-red-600 hover:bg-red-50 flex items-center gap-2 text-sm"
                    >
                      <LogOut size={18} />
                      <span>Logout</span>
                    </button>
                  </div>
                )}
              </>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
