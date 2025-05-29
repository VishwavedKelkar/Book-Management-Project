import { useForm } from "react-hook-form";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import { toast } from "react-toastify";

const BACKEND_URL = "https://localhost:7171";

function CreateBook() {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm();

  const [authors, setAuthors] = useState([]);
  const [genres, setGenres] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAuthorsAndGenres = async () => {
      try {
        const token = localStorage.getItem("token");
        const [authorRes, genreRes] = await Promise.all([
          fetch(`${BACKEND_URL}/api/authors`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${BACKEND_URL}/api/genre`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
        ]);

        if (!authorRes.ok || !genreRes.ok) {
          throw new Error("Failed to fetch authors or genres");
        }

        const authorsData = await authorRes.json();
        const genresData = await genreRes.json();

        setAuthors(authorsData);
        setGenres(genresData);
      } catch (err) {
        toast.error("Failed to load authors or genres");
      }
    };

    fetchAuthorsAndGenres();
  }, []);

  const onSubmit = async (data) => {
    const token = localStorage.getItem("token");

    // Mapping of frontend keys to backend keys 
    const payload = {
      Title: data.title,
      ISBN: data.isbn,
      PublishedDate: data.publishedDate,
      Description: data.description,
      PageCount: data.pageCount ? Number(data.pageCount) : null,
      AuthorId: data.authorId ? Number(data.authorId) : null,
      GenreId: data.genreId ? Number(data.genreId) : null,
    };

    try {
      const bookRes = await fetch(`${BACKEND_URL}/api/book`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      });

      if (!bookRes.ok) {
        const errorData = await bookRes.json();
        throw new Error(errorData.title || "Failed to create book");
      }

      const createdBook = await bookRes.json();

      toast.success("Book created successfully!");
      reset();

      navigate(`/upload-image/${createdBook.bookId}`);
    } catch (err) {
      toast.error(err.message || "Error creating book");
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />
      <main className="max-w-3xl mx-auto p-6">
        <div className="bg-white rounded-2xl shadow-lg p-8">
          <h1 className="text-3xl font-semibold text-center text-gray-800 mb-6">
            Create New Book
          </h1>

          <form onSubmit={handleSubmit(onSubmit)} className="grid gap-5">
            {/* Title */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">Title</label>
              <input
                {...register("title", { required: true })}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
              />
              {errors.title && (
                <p className="text-red-500 text-sm mt-1">Title is required</p>
              )}
            </div>

            {/* ISBN */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">ISBN</label>
              <input
                {...register("isbn", { required: true })}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
              />
              {errors.isbn && (
                <p className="text-red-500 text-sm mt-1">ISBN is required</p>
              )}
            </div>

            {/* Published Date */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">
                Published Date
              </label>
              <input
                type="date"
                {...register("publishedDate")}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
              />
            </div>

            {/* Description */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">Description</label>
              <textarea
                {...register("description")}
                rows="3"
                className="w-full rounded-md border border-gray-300 px-4 py-2"
              />
            </div>

            {/* Page Count */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">Page Count</label>
              <input
                type="number"
                {...register("pageCount")}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
              />
            </div>

            {/* Author */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">Author</label>
              <select
                {...register("authorId", { required: true })}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
                defaultValue=""
              >
                <option value="" disabled>
                  Select an author
                </option>
                {authors.map((author) => (
                  <option key={author.authorId} value={author.authorId}>
                    {author.name}
                  </option>
                ))}
              </select>
              {errors.authorId && (
                <p className="text-red-500 text-sm mt-1">Author is required</p>
              )}
            </div>

            {/* Genre */}
            <div>
              <label className="block mb-1 font-medium text-gray-700">Genre</label>
              <select
                {...register("genreId", { required: true })}
                className="w-full rounded-md border border-gray-300 px-4 py-2"
                defaultValue=""
              >
                <option value="" disabled>
                  Select a genre
                </option>
                {genres.map((genre) => (
                  <option key={genre.genreId} value={genre.genreId}>
                    {genre.name}
                  </option>
                ))}
              </select>
              {errors.genreId && (
                <p className="text-red-500 text-sm mt-1">Genre is required</p>
              )}
            </div>

            <div className="pt-4 flex justify-center gap-4">
              <button
                type="submit"
                className="px-6 py-2 bg-blue-600 text-white font-medium rounded-lg hover:bg-blue-700 transition"
              >
                Submit
              </button>
              <button
                type="button"
                onClick={() => navigate("/")}
                className="px-6 py-2 bg-gray-300 text-gray-800 font-medium rounded-lg hover:bg-gray-400 transition"
              >
                Cancel
              </button>
            </div>
          </form>
        </div>
      </main>
    </div>
  );
}

export default CreateBook;
