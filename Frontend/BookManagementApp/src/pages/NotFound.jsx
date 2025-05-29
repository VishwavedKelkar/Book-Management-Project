import React from "react";
import { Link, useNavigate } from "react-router-dom";

const NotFound = () => {
  const navigate = useNavigate();

  const handleGoBack = () => {
    navigate(-1); 
  };

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center px-4">
      <div className="max-w-2xl mx-auto text-center">
        <div className="mb-8">
          <h1 className="text-9xl font-bold text-blue-600 mb-4">404</h1>
          <div className="w-24 h-1 bg-blue-600 mx-auto mb-8"></div>
        </div>

        {/* Error Message */}
        <div className="mb-12">
          <h2 className="text-3xl font-semibold text-gray-800 mb-4">
            Oops! Page Not Found
          </h2>
          <p className="text-gray-600 text-lg mb-2">
            The page you're looking for doesn't exist or has been moved.
          </p>
          <p className="text-gray-500">
            Don't worry, it happens to the best of us!
          </p>
        </div>

        {/* Book Icon/Illustration */}
        <div className="mb-12">
          <div className="w-32 h-32 mx-auto bg-blue-100 rounded-full flex items-center justify-center mb-6">
            <svg 
              className="w-16 h-16 text-blue-600" 
              fill="currentColor" 
              viewBox="0 0 20 20"
            >
              <path d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
              <path fillRule="evenodd" d="M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4zm2 6a1 1 0 011-1h6a1 1 0 110 2H7a1 1 0 01-1-1zm1 3a1 1 0 100 2h6a1 1 0 100-2H7z" clipRule="evenodd" />
            </svg>
          </div>
        </div>

        <div className="flex flex-col sm:flex-row gap-4 justify-center items-center">
          <Link
            to="/"
            className="px-8 py-3 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 transition duration-200 shadow-lg hover:shadow-xl transform hover:-translate-y-0.5"
          >
            🏠 Go to Home
          </Link>
          
          <button
            onClick={handleGoBack}
            className="px-8 py-3 bg-gray-600 text-white rounded-lg font-semibold hover:bg-gray-700 transition duration-200 shadow-lg hover:shadow-xl transform hover:-translate-y-0.5"
          >
            ← Go Back
          </button>
          
          <Link
            to="/"
            className="px-8 py-3 bg-green-600 text-white rounded-lg font-semibold hover:bg-green-700 transition duration-200 shadow-lg hover:shadow-xl transform hover:-translate-y-0.5"
          >
            📚 Browse Books
          </Link>
        </div>

        <div className="mt-12 pt-8 border-t border-gray-200">
          <p className="text-gray-500 mb-4">Need help? Try these pages:</p>
          <div className="flex flex-wrap justify-center gap-6">
            <Link 
              to="/authors" 
              className="text-blue-600 hover:text-blue-800 hover:underline transition"
            >
              Authors
            </Link>
            <Link 
              to="/genres" 
              className="text-blue-600 hover:text-blue-800 hover:underline transition"
            >
              Genres
            </Link>
            <Link 
              to="/login" 
              className="text-blue-600 hover:text-blue-800 hover:underline transition"
            >
              Login
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default NotFound;