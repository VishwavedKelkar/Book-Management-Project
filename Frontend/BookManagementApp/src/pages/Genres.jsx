import React, { useEffect, useRef, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const BASE_URL = "https://localhost:7171";

const Genre = () => {
  const [genres, setGenres] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editGenre, setEditGenre] = useState(null);
  const [deleteTarget, setDeleteTarget] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

const isAdmin = () => {
  const token = localStorage.getItem("token");
  if (!token) return false;

  try {
    const payloadBase64 = token.split(".")[1];
    const decoded = JSON.parse(atob(payloadBase64));
    
    const adminRoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    const role = decoded[adminRoleClaim];

    return role === "Admin";
  } catch (err) {
    console.error("Error decoding token:", err);
    return false;
  }
};


  const fetchGenres = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await axios.get(`${BASE_URL}/api/genre`);
      setGenres(response.data);
    } catch (err) {
      setError("Failed to fetch genres.");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`${BASE_URL}/api/genre/${deleteTarget.genreId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      alert("Genre deleted successfully");
      fetchGenres();
      setDeleteTarget(null);
    } catch (err) {
      alert("Error deleting genre: " + (err.response?.data?.error || err.message));
    }
  };

  const handleEditSubmit = async (e) => {
    e.preventDefault();
    try {
      const { genreId, genreName } = editGenre;
      await axios.put(
        `${BASE_URL}/api/genre/${genreId}`,
        { genreName },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );
      alert("Genre updated successfully");
      setEditGenre(null);
      fetchGenres();
    } catch (err) {
      alert("Error updating genre: " + (err.response?.data?.error || err.message));
    }
  };

  useEffect(() => {
    fetchGenres();
  }, []);

  const modalRef = useRef(null);
  useEffect(() => {
    const handleOutsideClick = (e) => {
      if (editGenre && modalRef.current && !modalRef.current.contains(e.target)) {
        setEditGenre(null);
      }
    };
    document.addEventListener("mousedown", handleOutsideClick);
    return () => {
      document.removeEventListener("mousedown", handleOutsideClick);
    };
  }, [editGenre]);

  return (
    <div className="p-6">
      <button
        onClick={() => navigate("/")}
        className="mb-4 bg-gray-700 text-white px-4 py-2 rounded hover:bg-gray-800"
      >
        ← Back to Home
      </button>

      <h2 className="text-3xl font-bold mb-6">Genres</h2>

      {loading && <p>Loading genres...</p>}
      {error && <p className="text-red-500">{error}</p>}

      {genres.length === 0 ? (
        <p className="text-gray-600">No genres found.</p>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {genres.map((genre) => (
            <div
              key={genre.genreId}
              className="bg-white rounded-2xl shadow p-6 border border-gray-200 flex flex-col justify-between"
            >
              <div>
                <h3 className="text-xl font-semibold mb-2">{genre.genreName}</h3>
              </div>

              {isAdmin() && (
                <div className="flex justify-between mt-6 pt-4 border-t border-gray-100">
                  <button
                    onClick={() => setEditGenre(genre)}
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 w-full mr-2"
                  >
                    Edit
                  </button>
                  <button
                    onClick={() => setDeleteTarget(genre)}
                    className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 w-full ml-2"
                  >
                    Delete
                  </button>
                </div>
              )}
            </div>
          ))}
        </div>
      )}

      {/* Modal for Editing */}
      {editGenre && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div
            ref={modalRef}
            className="bg-white p-6 rounded-xl shadow-lg max-w-lg w-full relative"
          >
            <h3 className="text-2xl font-bold mb-4">Edit Genre</h3>
            <form onSubmit={handleEditSubmit}>
              <div className="mb-4">
                <label className="block mb-1 font-medium">Genre Name</label>
                <input
                  type="text"
                  value={editGenre.genreName || ""}
                  onChange={(e) => setEditGenre({ ...editGenre, genreName: e.target.value })}
                  required
                  className="w-full border rounded px-3 py-2"
                />
              </div>

              <div className="flex justify-end space-x-4">
                <button
                  type="button"
                  onClick={() => setEditGenre(null)}
                  className="bg-gray-300 px-4 py-2 rounded hover:bg-gray-400"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                >
                  Save Changes
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Confirmation Modal for Deletion */}
      {deleteTarget && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div className="bg-white rounded-xl shadow-lg p-6 w-full max-w-md text-center">
            <h2 className="text-xl font-bold mb-4">Confirm Deletion</h2>
            <p className="mb-6">
              Are you sure you want to delete{" "}
              <span className="font-semibold">{deleteTarget.genreName}</span>?
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

export default Genre;
