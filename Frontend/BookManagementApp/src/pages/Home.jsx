import { useEffect, useState, useContext } from "react";
import Navbar from "../components/Navbar";
import { Link } from "react-router-dom";
import AuthContext from "../auth/AuthContext";

const BACKEND_URL = "https://localhost:7171";

function Home() {
  const [books, setBooks] = useState([]);
  const [images, setImages] = useState([]);
  const [error, setError] = useState("");

  const { user } = useContext(AuthContext);

  useEffect(() => {
    async function fetchData() {
      try {
        const token = localStorage.getItem("token");
        const headers = token ? { Authorization: `Bearer ${token}` } : {};

        const booksRes = await fetch(BACKEND_URL + "/api/book", { headers });
        if (!booksRes.ok) throw new Error("Failed to fetch books");
        const booksData = await booksRes.json();

        const imagesRes = await fetch(BACKEND_URL + "/api/bookimage", { headers });
        if (!imagesRes.ok) throw new Error("Failed to fetch book images");
        const imagesData = await imagesRes.json();

        setBooks(booksData);
        setImages(imagesData);
      } catch (err) {
        setError(err.message || "Something went wrong");
      }
    }

    fetchData();
  }, []);

  const getImageUrl = (bookId) => {
    const image = images.find((img) => img.bookId === bookId);
    return image ? BACKEND_URL + image.imageUrl : null;
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 to-white">
      <Navbar />
      <main className="max-w-7xl mx-auto px-4 py-20 text-gray-700">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-10 gap-4">
          <h1 className="text-2xl sm:text-3xl font-bold tracking-tight">
            Welcome <span className="text-2xl sm:text-3xl font-bold tracking-tight text-blue-600">{user?.username || "Guest"}</span> to the <span className="text-blue-600">Book Management System</span>
          </h1>

          {user?.username === "admin" && (
            <div className="flex flex-wrap gap-3">
              <Link to="/create-book">
                <button className="px-5 py-2.5 bg-blue-600 text-white text-sm font-semibold rounded-lg shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition">
                  + Create Book
                </button>
              </Link>
              <Link to="/create-author">
                <button className="px-5 py-2.5 bg-green-600 text-white text-sm font-semibold rounded-lg shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition">
                  + Create Author
                </button>
              </Link>
              <Link to="/create-genre">
                <button className="px-5 py-2.5 bg-purple-600 text-white text-sm font-semibold rounded-lg shadow-md hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500 transition">
                  + Create Genre
                </button>
              </Link>
            </div>
          )}
        </div>

        {error && (
          <p className="text-red-500 bg-red-100 border border-red-200 px-4 py-3 rounded mb-8">
            {error}
          </p>
        )}

        <div className="grid gap-8 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
          {books.map((book) => (
            <div
              key={book.bookId}
              className="bg-white hover:shadow-xl transition-shadow shadow-md rounded-xl p-5 border border-gray-200"
            >
              {getImageUrl(book.bookId) && (
                <Link to={`/books/${book.bookId}`}>
                  <img
                    src={getImageUrl(book.bookId)}
                    alt={book.title}
                    className="w-full h-52 object-cover rounded-md mb-4 hover:opacity-90 transition"
                  />
                </Link>
              )}
              <Link to={`/books/${book.bookId}`}>
                <h2 className="text-lg font-semibold text-gray-900 hover:text-blue-600 transition cursor-pointer mb-1">
                  {book.title}
                </h2>
              </Link>
              <p className="text-sm text-gray-600 mb-1">Author: {book.authorName}</p>
              <p className="text-sm text-gray-500">Genre: {book.genreName}</p>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
}

export default Home;
