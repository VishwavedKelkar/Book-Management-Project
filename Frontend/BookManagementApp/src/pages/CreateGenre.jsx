import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import AuthContext from "../auth/AuthContext";

import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "../components/ui/form";
import { Button } from "../components/ui/button";
import { Input } from "../components/ui/input";
import { Label } from "../components/ui/label";

const BACKEND_URL = "https://localhost:7171";

export default function CreateGenre() {
  const { user } = useContext(AuthContext);
  const navigate = useNavigate();
  const [serverError, setServerError] = useState("");
  const [loading, setLoading] = useState(false);
  const [successGenre, setSuccessGenre] = useState(null); 

  const form = useForm({
    defaultValues: {
      genreName: "",
    },
  });

  useEffect(() => {
    if (!user || user.role !== "Admin") {
      navigate("/genres");
    }
  }, [user, navigate]);

  const onSubmit = async (data) => {
    setServerError("");
    setLoading(true);

    try {
      const token = localStorage.getItem("token");
      const res = await fetch(`${BACKEND_URL}/api/genre`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          GenreName: data.genreName,
        }),
      });

      if (res.ok) {
        setSuccessGenre(data.genreName); 
        form.reset();                    
      } else {
        const result = await res.json();
        setServerError(result.message || "Failed to create genre");
      }
    } catch (err) {
      setServerError("Network error. Please try again later.");
    } finally {
      setLoading(false);
    }
  };

  // Close modal handler
  const closeModal = () => {
    setSuccessGenre(null);
    navigate("/genres");  
  };

  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100 px-4">
      <div className="w-full max-w-md bg-white p-8 rounded-xl shadow-md relative">
        <button
          onClick={() => navigate("/")}
          className="absolute top-4 left-4 text-sm text-blue-600 hover:underline"
        >
          ← Back to Home
        </button>

        <h2 className="text-2xl font-semibold text-center text-gray-800 mb-6">
          Create New Genre
        </h2>

        {serverError && (
          <p className="mb-4 text-center text-red-600 font-medium">{serverError}</p>
        )}

        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5" noValidate>
            <FormField
              control={form.control}
              name="genreName"
              rules={{ required: "Genre name is required" }}
              render={({ field }) => (
                <FormItem>
                  <Label htmlFor="genreName">Genre Name</Label>
                  <FormControl>
                    <Input
                      id="genreName"
                      placeholder="Enter genre name"
                      {...field}
                      aria-invalid={form.formState.errors.genreName ? "true" : "false"}
                      autoComplete="off"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <Button type="submit" disabled={loading} className="w-full mt-2">
              {loading ? "Creating..." : "Create Genre"}
            </Button>
          </form>
        </Form>

        {/* Success Modal */}
        {successGenre && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white p-6 rounded-lg shadow-lg max-w-sm w-full text-center">
              <h3 className="text-xl font-semibold mb-4">Success!</h3>
              <p className="mb-6">{successGenre} genre created successfully.</p>
              <button
                onClick={closeModal}
                className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
              >
                OK
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
