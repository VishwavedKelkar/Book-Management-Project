import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import Navbar from "../components/Navbar";

const BACKEND_URL = "https://localhost:7171";

function CreateAuthor() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const navigate = useNavigate();

  const onSubmit = async (data) => {
    try {
      const token = localStorage.getItem("token");
      const response = await fetch(`${BACKEND_URL}/api/authors`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.error || "Failed to create author");
      }

      toast.success("Author created successfully!");
      navigate("/");
    } catch (error) {
      toast.error(error.message || "An error occurred");
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <div className="max-w-3xl mx-auto px-4 py-16">
        <h2 className="text-2xl font-bold mb-8 text-gray-800">Create Author</h2>

        <form
          onSubmit={handleSubmit(onSubmit)}
          className="space-y-6 bg-white p-8 rounded-lg shadow-md border border-gray-200"
        >
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Author Name
            </label>
            <input
              {...register("authorName", { required: "Author name is required" })}
              type="text"
              className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            {errors.authorName && (
              <p className="text-red-500 text-sm mt-1">{errors.authorName.message}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Bio</label>
            <textarea
              {...register("bio")}
              className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              rows="3"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Date of Birth
            </label>
            <input
              {...register("dateOfBirth", { required: "Date of Birth is required" })}
              type="date"
              className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            {errors.dateOfBirth && (
              <p className="text-red-500 text-sm mt-1">{errors.dateOfBirth.message}</p>
            )}
          </div>

          <div className="pt-4 flex justify-center gap-4">
            <button
              type="submit"
              className="w-full py-2.5 bg-green-600 text-white text-sm font-semibold rounded-lg shadow-md hover:bg-green-700 transition"
            >
              Submit
            </button>

            <button
              type="button"
              onClick={() => navigate("/")}
              className="w-full py-2.5 bg-gray-300 text-gray-800 text-sm font-semibold rounded-lg shadow-md hover:bg-gray-400 transition"
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default CreateAuthor;
