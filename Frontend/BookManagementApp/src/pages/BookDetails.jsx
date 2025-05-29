import AuthContext from "@/auth/AuthContext";
import { useContext, useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";

const BACKEND_URL = "https://localhost:7171";

function BookDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { user } = useContext(AuthContext);
  const [book, setBook] = useState(null);
  const [images, setImages] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    async function fetchData() {
      try {
        const bookRes = await fetch(`${BACKEND_URL}/api/book/${id}`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (bookRes.status === 401) {
          navigate("/login");
          return;
        }
        if (!bookRes.ok) throw new Error("Failed to fetch book details");
        const bookData = await bookRes.json();

        const imagesRes = await fetch(`${BACKEND_URL}/api/bookimage`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (!imagesRes.ok) throw new Error("Failed to fetch book images");
        const imagesData = await imagesRes.json();

        setBook(bookData);
        setImages(imagesData);
      } catch (err) {
        setError(err.message);
      }
    }

    fetchData();
  }, [id, navigate]);

  const handleDelete = async () => {
    const confirmDelete = window.confirm("Are you sure you want to delete this book?");
    if (!confirmDelete) return;

    try {
      const token = localStorage.getItem("token");
      const response = await fetch(`${BACKEND_URL}/api/book/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) throw new Error("Failed to delete book");
      alert("Book deleted successfully");
      navigate("/");
    } catch (err) {
      alert(err.message);
    }
  };

  if (error)
    return (
      <div className="max-w-xl mx-auto p-6 text-center text-red-600 font-semibold">
        {error}
      </div>
    );

  if (!book)
    return (
      <div className="max-w-xl mx-auto p-6 text-center text-gray-600 font-medium">
        Loading book details...
      </div>
    );

  const image = images.find((img) => img.bookId === book.bookId);
  const imageUrl = image ? BACKEND_URL + image.imageUrl : null;
  const isAdmin = user?.role === "Admin";

  if (!user) {
    return <div className="text-center mt-8">Loading user info...</div>;
  }

  return (
    <div className="max-w-4xl mx-auto mt-12 p-8 bg-white rounded-lg shadow-lg border border-gray-200">
      <Link
        to="/"
        className="inline-block mb-6 px-4 py-2 bg-gray-100 hover:bg-gray-200 text-gray-700 rounded-md shadow-sm transition"
      >
        ← Back to Home
      </Link>

      <div className="flex flex-col md:flex-row md:gap-10">
        {imageUrl && (
          <img
            src={imageUrl}
            alt={book.title}
            className="w-full md:w-64 h-auto object-cover rounded-lg shadow-md border border-gray-300 mb-6 md:mb-0"
          />
        )}

        <div className="flex-1 text-gray-800 space-y-5">
          <div>
            <h2 className="text-xl font-semibold mb-1">Title:</h2>
            <p className="text-lg">{book.title}</p>
          </div>

          <div>
            <h2 className="text-xl font-semibold mb-1">Author:</h2>
            <p className="text-lg">{book.authorName}</p>
          </div>

          <div>
            <h2 className="text-xl font-semibold mb-1">Genre:</h2>
            <p className="text-lg">{book.genreName}</p>
          </div>

          <div>
            <h2 className="text-xl font-semibold mb-1">Published Date:</h2>
            <p className="text-lg">
              {new Date(book.publishedDate).toLocaleDateString(undefined, {
                year: "numeric",
                month: "long",
                day: "numeric",
              })}
            </p>
          </div>

          {book.pageCount !== null && (
            <div>
              <h2 className="text-xl font-semibold mb-1">Page Count:</h2>
              <p className="text-lg">{book.pageCount}</p>
            </div>
          )}

          {book.description && (
            <div>
              <h2 className="text-xl font-semibold mb-1">Description:</h2>
              <p className="text-lg whitespace-pre-line leading-relaxed">
                {book.description}
              </p>
            </div>
          )}
        </div>
      </div>

      {isAdmin && (
        <div className="flex gap-4 mt-8 justify-start">
          <Link
            to={`/edit-book/${book.bookId}`}
            className="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-md transition"
          >
            Edit Book
          </Link>
          <button
            onClick={handleDelete}
            className="px-4 py-2 bg-red-600 hover:bg-red-700 text-white rounded-md transition"
          >
            Delete Book
          </button>
        </div>
      )}
    </div>
  );
}

export default BookDetails;
