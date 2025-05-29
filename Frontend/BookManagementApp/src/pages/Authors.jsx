import React, { useEffect, useRef, useState } from "react";
import axios from "axios";
import { format } from "date-fns";
import { useNavigate } from "react-router-dom";

const BASE_URL = "https://localhost:7171";

const isAdmin = () => {
  const token = localStorage.getItem("token");
  if (!token) return false;

  try {
    const payloadBase64 = token.split(".")[1];
    const decoded = JSON.parse(atob(payloadBase64));
    const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    return decoded[roleClaim] === "Admin";
  } catch (err) {
    console.error("Error decoding token:", err);
    return false;
  }
};


const Authors = () => {
  const [authors, setAuthors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editAuthor, setEditAuthor] = useState(null);
  const [deleteTarget, setDeleteTarget] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const fetchAuthors = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await axios.get(`${BASE_URL}/api/authors`);
      setAuthors(response.data);
    } catch (err) {
      setError("Failed to fetch authors.");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`${BASE_URL}/api/authors/${deleteTarget.authorId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      alert("Author deleted successfully");
      fetchAuthors();
      setDeleteTarget(null);
    } catch (err) {
      alert("Error deleting author: " + (err.response?.data?.error || err.message));
    }
  };

  const handleEditSubmit = async (e) => {
    e.preventDefault();
    try {
      const { authorId, name, bio, dateOfBirth } = editAuthor;
      await axios.put(
        `${BASE_URL}/api/authors/${authorId}`,
        { authorName: name, bio, dateOfBirth },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );
      alert("Author updated successfully");
      setEditAuthor(null);
      fetchAuthors();
    } catch (err) {
      alert("Error updating author: " + (err.response?.data?.error || err.message));
    }
  };

  useEffect(() => {
    fetchAuthors();
  }, []);

  const modalRef = useRef(null);
  useEffect(() => {
    const handleOutsideClick = (e) => {
      if (editAuthor && modalRef.current && !modalRef.current.contains(e.target)) {
        setEditAuthor(null);
      }
    };
    document.addEventListener("mousedown", handleOutsideClick);
    return () => {
      document.removeEventListener("mousedown", handleOutsideClick);
    };
  }, [editAuthor]);

  return (
    <div className="p-6">
      <button
        onClick={() => navigate("/")}
        className="mb-4 bg-gray-700 text-white px-4 py-2 rounded hover:bg-gray-800"
      >
        ← Back to Home
      </button>

      <h2 className="text-3xl font-bold mb-6">Authors</h2>

      {loading && <p>Loading authors...</p>}
      {error && <p className="text-red-500">{error}</p>}

      {authors.length === 0 ? (
        <p className="text-gray-600">No authors found.</p>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {authors.map((author) => (
            <div
              key={author.authorId}
              className="bg-white rounded-2xl shadow p-6 border border-gray-200"
            >
              <h3 className="text-xl font-semibold mb-2">{author.name}</h3>
              <p className="text-gray-700 mb-1">
                <span className="font-medium">Bio:</span>{" "}
                {author.bio || <span className="italic text-gray-400">No bio</span>}
              </p>
              <p className="text-gray-700 mb-4">
                <span className="font-medium">Date of Birth:</span>{" "}
                {format(new Date(author.dateOfBirth), "dd MMM yyyy")}
              </p>
              {isAdmin() && (
                <div className="flex justify-between">
                  <button onClick={() => setEditAuthor(author)} className="bg-blue-500 text-white px-4 py-1 rounded hover:bg-blue-600">
                    Edit
                  </button>
                  <button onClick={() => setDeleteTarget(author)} className="bg-red-500 text-white px-4 py-1 rounded hover:bg-red-600">
                    Delete
                  </button>
                </div>
              )}
            </div>
          ))}
        </div>
      )}

      {/* Modal for Editing */}
      {editAuthor && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div
            ref={modalRef}
            className="bg-white p-6 rounded-xl shadow-lg max-w-lg w-full relative"
          >
            <h3 className="text-2xl font-bold mb-4">Edit Author</h3>

            <form onSubmit={handleEditSubmit}>
              <div className="mb-4">
                <label className="block mb-1 font-medium">Name</label>
                <input
                  type="text"
                  value={editAuthor.name || ""}
                  onChange={(e) => setEditAuthor({ ...editAuthor, name: e.target.value })}
                  required
                  className="w-full border rounded px-3 py-2"
                />
              </div>

              <div className="mb-4">
                <label className="block mb-1 font-medium">Bio</label>
                <textarea
                  value={editAuthor.bio || ""}
                  onChange={(e) => setEditAuthor({ ...editAuthor, bio: e.target.value })}
                  className="w-full border rounded px-3 py-2"
                  rows={3}
                />
              </div>

              <div className="mb-4">
                <label className="block mb-1 font-medium">Date of Birth</label>
                <input
                  type="date"
                  value={editAuthor.dateOfBirth?.split("T")[0] || ""}
                  onChange={(e) => setEditAuthor({ ...editAuthor, dateOfBirth: e.target.value })}
                  required
                  className="w-full border rounded px-3 py-2"
                />
              </div>

              <div className="flex justify-end space-x-3">
                <button
                  type="submit"
                  className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                >
                  Save
                </button>
                <button
                  type="button"
                  onClick={() => setEditAuthor(null)}
                  className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-500"
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Delete Confirmation Modal */}
      {deleteTarget && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div className="bg-white rounded-xl shadow-lg p-6 w-full max-w-md text-center">
            <h2 className="text-xl font-bold mb-4">Confirm Deletion</h2>
            <p className="mb-6">
              Are you sure you want to delete{" "}
              <span className="font-semibold">{deleteTarget.name}</span>?
            </p>
            <div className="flex justify-center space-x-4">
              <button
                onClick={handleDelete}
                className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
              >
                Yes, Delete
              </button>
              <button
                onClick={() => setDeleteTarget(null)}
                className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-500"
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Authors;
