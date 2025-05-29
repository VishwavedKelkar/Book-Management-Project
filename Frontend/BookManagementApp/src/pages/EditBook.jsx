import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";

const BACKEND_URL = "https://localhost:7171";

function EditBook() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [book, setBook] = useState(null);
  const [authors, setAuthors] = useState([]);
  const [genres, setGenres] = useState([]);
  const [images, setImages] = useState([]);
  const [error, setError] = useState("");
  const [formData, setFormData] = useState({
    title: "",
    ISBN: "",
    authorId: "",
    genreId: "",
    publishedDate: "",
    pageCount: "",
    description: "",
  });
  const [selectedImageFile, setSelectedImageFile] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    async function fetchData() {
      try {
        const [bookRes, authorsRes, genresRes, imagesRes] = await Promise.all([
          fetch(`${BACKEND_URL}/api/book/${id}`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${BACKEND_URL}/api/authors`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${BACKEND_URL}/api/genre`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${BACKEND_URL}/api/bookimage`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
        ]);

        if (!bookRes.ok) throw new Error("Failed to fetch book");
        if (!authorsRes.ok) throw new Error("Failed to fetch authors");
        if (!genresRes.ok) throw new Error("Failed to fetch genres");
        if (!imagesRes.ok) throw new Error("Failed to fetch images");

        const [bookData, authorsData, genresData, imagesData] = await Promise.all([
          bookRes.json(),
          authorsRes.json(),
          genresRes.json(),
          imagesRes.json(),
        ]);

        setBook(bookData);
        setAuthors(authorsData);
        setGenres(genresData);
        setImages(imagesData);

        setFormData({
          title: bookData.title || "",
          ISBN: bookData.isbn ,
          authorId: bookData.authorId || "",
          genreId: bookData.genreId || "",
          publishedDate: bookData.publishedDate
            ? new Date(bookData.publishedDate).toISOString().slice(0, 10)
            : "",
          pageCount: bookData.pageCount?.toString() || "",
          description: bookData.description || "",
        });
      } catch (err) {
        setError(err.message);
      }
    }

    fetchData();
  }, [id, navigate]);

  const image = images.find((img) => img.bookId === book?.bookId);
  const imageUrl = image ? BACKEND_URL + image.imageUrl : null;

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleImageChange = (e) => {
    if (e.target.files?.length > 0) {
      setSelectedImageFile(e.target.files[0]);
    }
  };

  const convertFileToBase64 = (file) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result);
      reader.onerror = reject;
      reader.readAsDataURL(file);
    });

  const handleSubmit = async (e) => {
    e.preventDefault();

    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    try {
      const formPayload = new FormData();
      formPayload.append("title", formData.title);
      formPayload.append("ISBN", formData.ISBN);
      formPayload.append("authorId", formData.authorId);
      formPayload.append("genreId", formData.genreId);
      formPayload.append("publishedDate", formData.publishedDate);
      formPayload.append("pageCount", Number(formData.pageCount));
      formPayload.append("description", formData.description);

      const bookUpdateRes = await fetch(`${BACKEND_URL}/api/book/${id}`, {
        method: "PUT",
        headers: { Authorization: `Bearer ${token}` },
        body: formPayload,
      });

      if (!bookUpdateRes.ok) throw new Error("Failed to update book");

      if (selectedImageFile && image?.bookImageId) {
        const base64 = await convertFileToBase64(selectedImageFile);
        const imgRes = await fetch(`${BACKEND_URL}/api/bookimage/${image.bookImageId}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ imageUrl: base64 }),
        });

        if (!imgRes.ok) throw new Error("Failed to update image");
      }

      alert("Book updated successfully");
      navigate(`/books/${id}`);
    } catch (err) {
      alert(err.message);
    }
  };

  if (error) {
    return <div className="text-center mt-12 text-red-600">{error}</div>;
  }

  if (!book) {
    return <div className="text-center mt-12 text-gray-600">Loading book details...</div>;
  }

  return (
    <div className="max-w-4xl mx-auto mt-12 p-8 bg-white rounded-2xl shadow-lg border border-gray-200">
      <Link
        to={`/books/${id}`}
        className="inline-block mb-6 text-blue-600 hover:underline hover:text-blue-700 transition font-medium"
      >
        ← Back to Book Details: <span className="italic">{book.title}</span>
      </Link>

      <h1 className="text-3xl font-semibold text-center mb-8 text-gray-800">Edit Book Information</h1>

      {imageUrl ? (
        <div className="flex justify-center mb-6">
          <img
            src={imageUrl}
            alt={`Cover for ${book.title}`}
            className="w-52 h-auto rounded-lg border border-gray-300 shadow"
          />
        </div>
      ) : (
        <p className="text-center mb-6 text-gray-400">No image available</p>
      )}

      <div className="flex justify-center mb-6">
        <label
          htmlFor="imageFile"
          className="cursor-pointer px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition font-medium"
        >
          {selectedImageFile ? selectedImageFile.name : "Choose New Book Image"}
        </label>
        <input
          id="imageFile"
          type="file"
          accept="image/*"
          onChange={handleImageChange}
          className="hidden"
        />
      </div>

      <form onSubmit={handleSubmit} className="space-y-5">
        <div>
          <label className="block font-medium mb-1 text-gray-700">Title</label>
          <input
            type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">ISBN</label>
          <input
            type="text"
            name="ISBN"
            value={formData.ISBN}
            readOnly
            className="w-full p-2 border border-gray-300 rounded-md bg-gray-100 text-gray-600 cursor-not-allowed"
          />
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">Author</label>
          <select
            name="authorId"
            value={formData.authorId}
            onChange={handleChange}
            required
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Select Author</option>
            {authors.map((a) => (
              <option key={a.authorId} value={a.authorId}>
                {a.name}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">Genre</label>
          <select
            name="genreId"
            value={formData.genreId}
            onChange={handleChange}
            required
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Select Genre</option>
            {genres.map((g) => (
              <option key={g.genreId} value={g.genreId}>
                {g.genreName }
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">Published Date</label>
          <input
            type="date"
            name="publishedDate"
            value={formData.publishedDate}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">Page Count</label>
          <input
            type="number"
            name="pageCount"
            value={formData.pageCount}
            min="1"
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label className="block font-medium mb-1 text-gray-700">Description</label>
          <textarea
            name="description"
            value={formData.description}
            onChange={handleChange}
            rows={5}
            className="w-full p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="flex justify-center mt-8">
          <button
            type="submit"
            className="px-8 py-3 bg-blue-600 text-white rounded-md font-semibold hover:bg-blue-700 transition"
          >
            Save Changes
          </button>
        </div>
      </form>
    </div>
  );
}

export default EditBook;
