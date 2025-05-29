import { useState, useContext } from "react";
import { useForm } from "react-hook-form";
import { Eye, EyeOff } from "lucide-react";
import { useNavigate } from "react-router-dom";
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

import {jwtDecode} from "jwt-decode";

export default function Login() {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const [showPassword, setShowPassword] = useState(false);
  const [serverError, setServerError] = useState("");
  const [loading, setLoading] = useState(false);

  const form = useForm({
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const onSubmit = async (data) => {
    setServerError("");
    setLoading(true);
    try {
      const res = await fetch("https://localhost:7171/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });

      const result = await res.json();
      setLoading(false);

      if (res.ok && result.token) {
        const decoded = jwtDecode(result.token);
        const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        
        // Saving username in localStorage for Home page condition - hiding or visibility depends on regular user or Admin
        localStorage.setItem("username", data.username);

        login(result.token, {
          username: result.username,
          email: result.email,
          role, 
        });
        navigate("/");
      } else {
        setServerError(result.message || "Invalid username or password");
      }
    } catch (err) {
      setLoading(false);
      setServerError("Network error, please try again later");
      console.log(err);
    }
  };

  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100 px-4">
      <div className="w-full max-w-md bg-white p-8 rounded-xl shadow-md relative">
        {/* Back Button */}
        <button
          onClick={() => navigate("/")}
          className="absolute top-4 left-4 text-sm text-blue-600 hover:underline"
        >
          ← Back to Home
        </button>

        <h2 className="text-3xl font-semibold text-center text-gray-800 mb-6">Login</h2>

        {serverError && (
          <p className="mb-4 text-center text-red-600 font-medium">{serverError}</p>
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
                        aria-invalid={form.formState.errors.password ? "true" : "false"}
                        className="pr-10"
                      />
                      <button
                        type="button"
                        onClick={() => setShowPassword(!showPassword)}
                        className="absolute top-1/2 right-3 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 focus:outline-none"
                        aria-label={showPassword ? "Hide password" : "Show password"}
                        tabIndex={-1}
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
              {loading ? "Logging in..." : "Login"}
            </Button>
          </form>
        </Form>
      </div>
    </div>
  );
}
