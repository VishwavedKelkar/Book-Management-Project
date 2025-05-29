// src/pages/DeleteBook.jsx
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

const BACKEND_URL = "https://localhost:7171";

function DeleteBook() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [book, setBook] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    async function fetchBook() {
      const res = await fetch(`${BACKEND_URL}/api/book/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      if (res.ok) {
        const data = await res.json();
        setBook(data);
      } else {
        setError("Failed to fetch book");
      }
    }

    fetchBook();
  }, [id, navigate]);

  const handleDelete = async () => {
    const token = localStorage.getItem("token");
    const res = await fetch(`${BACKEND_URL}/api/book/${id}`, {
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });

    if (res.ok) {
      navigate("/");
    } else {
      setError("Failed to delete book");
    }
  };

  if (!book) return <div className="text-center mt-10">Loading...</div>;

  return (
    <div className="max-w-xl mx-auto mt-10 p-6 bg-white rounded shadow text-center">
      <h2 className="text-2xl font-bold mb-4">Delete Book</h2>
      {error && <p className="text-red-600">{error}</p>}
      <p className="mb-6">
        Are you sure you want to delete the book titled{" "}
        <span className="font-semibold">{book.title}</span>?
      </p>
      <button
        onClick={handleDelete}
        className="bg-red-600 text-white px-4 py-2 rounded mr-2 hover:bg-red-700"
      >
        Yes, Delete
      </button>
      <button
        onClick={() => navigate(`/book/${id}`)}
        className="bg-gray-300 text-black px-4 py-2 rounded hover:bg-gray-400"
      >
        Cancel
      </button>
    </div>
  );
}

export default DeleteBook;
