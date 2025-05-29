import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import { toast } from "react-toastify";

const BACKEND_URL = "https://localhost:7171";

function UploadImage() {
  const { bookId } = useParams();
  const navigate = useNavigate();

  const [imageFile, setImageFile] = useState(null);
  const [previewUrl, setPreviewUrl] = useState(null);
  const [book, setBook] = useState(null);

  useEffect(() => {
    const fetchBook = async () => {
      try {
        const token = localStorage.getItem("token");
        const res = await fetch(`${BACKEND_URL}/api/book/${bookId}`, {
          headers: { Authorization: `Bearer ${token}` },
        });

        if (!res.ok) throw new Error("Failed to fetch book");

        const data = await res.json();
        setBook(data);
      } catch (err) {
        toast.error("Could not load book details");
      }
    };

    fetchBook();
  }, [bookId]);

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    setImageFile(file);
    setPreviewUrl(file ? URL.createObjectURL(file) : null);
  };

  const handleUpload = async () => {
    if (!imageFile) {
      toast.error("Please select an image first");
      return;
    }

    try {
      const token = localStorage.getItem("token");
      const formData = new FormData();
      formData.append("Image", imageFile);
      formData.append("BookId", bookId);

      const res = await fetch(`${BACKEND_URL}/api/bookimage/upload`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        body: formData,
      });

      if (!res.ok) throw new Error("Image upload failed");

      toast.success("Image uploaded successfully!");
      navigate("/"); 
    } catch (err) {
      toast.error(err.message || "Failed to upload image");
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />
      <main className="max-w-xl mx-auto p-6">
        <div className="bg-white rounded-2xl shadow-lg p-8">
          <h1 className="text-3xl font-semibold text-center text-gray-800 mb-6">
            Upload Cover Image
          </h1>

          {book && (
            <div className="mb-6 text-center">
              <p className="text-lg font-medium">Book: {book.title}</p>
              <p className="text-sm text-gray-600">ID: {book.bookId}</p>
            </div>
          )}

          <div>
            <input
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              className="block w-full text-sm text-gray-500 file:mr-4 file:py-2 file:px-4 file:rounded file:border-0 file:text-sm file:font-semibold file:bg-blue-50 file:text-blue-700 hover:file:bg-blue-100"
            />
          </div>

          {previewUrl && (
            <img
              src={previewUrl}
              alt="Preview"
              className="mt-4 h-48 object-cover rounded-md shadow mx-auto"
            />
          )}

          <div className="pt-6 flex justify-center gap-4">
            <button
              onClick={handleUpload}
              className="px-6 py-2 bg-blue-600 text-white font-medium rounded-lg hover:bg-blue-700 transition"
            >
              Upload Image
            </button>

            <button
              onClick={() => navigate("/")}
              className="px-6 py-2 bg-gray-300 text-gray-800 font-medium rounded-lg hover:bg-gray-400 transition"
            >
              Cancel
            </button>
          </div>
        </div>
      </main>
    </div>
  );
}

export default UploadImage;
