import { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { Eye, EyeOff } from "lucide-react";

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

export default function Register() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [serverError, setServerError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const form = useForm({
    defaultValues: {
      username: "",
      email: "",
      password: "",
    },
  });

  const onSubmit = async (data) => {
    setServerError("");
    setSuccessMessage("");
    setLoading(true);

    try {
      const res = await fetch("https://localhost:7171/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });

      const result = await res.json();
      setLoading(false);

      if (res.ok) {
        setSuccessMessage("Registration successful! Redirecting to login...");
        setTimeout(() => navigate("/login"), 2000);
      } else {
        setServerError(result.message || "Registration failed");
      }
    } catch (err) {
      setLoading(false);
      setServerError("Something went wrong. Please try again.");
    }
  };

  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100 px-4">
      <div className="w-full max-w-md bg-white p-8 rounded-xl shadow-md relative">
        <h2 className="text-3xl font-semibold text-center text-gray-800 mb-6">Register</h2>
        <button
          onClick={() => navigate("/")}
          className="absolute top-4 left-4 text-sm text-blue-600 hover:underline"
        >
          ← Back to Home
        </button>
        
        {serverError && (
          <p className="mb-4 text-center text-red-600 font-medium">{serverError}</p>
        )}
        {successMessage && (
          <p className="mb-4 text-center text-green-600 font-medium">{successMessage}</p>
        )}



        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5">

            {/* Username */}
            <FormField
              control={form.control}
              name="username"
              render={({ field }) => (
                <FormItem>
                  <Label htmlFor="username">Username</Label>
                  <FormControl>
                    <Input
                      id="username"
                      placeholder="Enter your username"
                      {...field}
                      aria-invalid={form.formState.errors.username ? "true" : "false"}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            {/* Email */}
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <Label htmlFor="email">Email</Label>
                  <FormControl>
                    <Input
                      id="email"
                      type="email"
                      placeholder="Enter your email"
                      {...field}
                      aria-invalid={form.formState.errors.email ? "true" : "false"}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            {/* Password */}
            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem>
                  <Label htmlFor="password">Password</Label>
                  <FormControl>
                    <div className="relative">
                      <Input
                        id="password"
                        type={showPassword ? "text" : "password"}
                        placeholder="Enter your password"
                        {...field}
                        className="pr-10"
                      />
                      <button
                        type="button"
                        onClick={() => setShowPassword(!showPassword)}
                        className="absolute top-1/2 right-3 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 focus:outline-none"
                        tabIndex={-1}
                        aria-label={showPassword ? "Hide password" : "Show password"}
                      >
                        {showPassword ? <EyeOff size={20} /> : <Eye size={20} />}
                      </button>
                    </div>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            {/* Submit Button */}
            <Button type="submit" disabled={loading} className="w-full mt-2">
              {loading ? "Registering..." : "Register"}
            </Button>
          </form>
        </Form>
      </div>
    </div>
  );
}
