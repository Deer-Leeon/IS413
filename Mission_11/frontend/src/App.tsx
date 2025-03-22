import React from "react";
import BookList from "./components/BookList";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  return (
    <div className="App">
      <h1 className="text-center my-4">Online Bookstore</h1>
      <BookList />
    </div>
  );
}

export default App;
