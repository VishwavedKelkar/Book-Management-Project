import './App.css'
import {  Route, Routes } from 'react-router-dom'
import Login from './pages/Login'
import Home from './pages/Home'
import Register from './pages/Register'
import BookDetails from './pages/BookDetails'
import DeleteBook from './pages/DeleteBook'
import EditBook from './pages/EditBook'
import CreateBook from './pages/CreateBook'
import UploadImage from './pages/UploadImage'
import CreateAuthor from './pages/CreateAuthor'
import Authors from './pages/Authors'
import Genres from './pages/Genres'
import CreateGenre from './pages/CreateGenre'
import NotFound from './pages/NotFound'

function App() {
  return (
    <>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/create-book" element={<CreateBook />} />
          <Route path="/edit-book/:id" element={< EditBook/>}/>
          <Route path="/books/:id" element={<BookDetails />} />
          <Route path="/delete/:id" element={< DeleteBook/>}/>
          <Route path="/upload-image/:bookId" element={<UploadImage />}/>
          
          <Route path="/create-author" element={<CreateAuthor />}/>
          <Route path="/authors" element={<Authors />}/>
          <Route path="/genres" element={<Genres />}/>
          <Route path="/create-genre" element={<CreateGenre />}/>


          <Route path="*" element={<NotFound />} />

        </Routes>
    </>
  )
}

export default App
