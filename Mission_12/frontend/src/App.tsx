import { useState, Component, ErrorInfo, ReactNode } from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import BookList from "./components/BookList";
import Cart from "./components/Cart";

class ErrorBoundary extends Component<
  { children: ReactNode },
  { hasError: boolean }
> {
  state = { hasError: false };

  static getDerivedStateFromError() {
    return { hasError: true };
  }

  componentDidCatch(error: Error, info: ErrorInfo) {
    console.error("ErrorBoundary caught:", error, info);
  }

  render() {
    if (this.state.hasError) {
      return (
        <div className="alert alert-danger m-3">
          <h2>Something went wrong with the application.</h2>
          <p>Please refresh the page or try again later.</p>
        </div>
      );
    }
    return this.props.children;
  }
}

function App() {
  const [showCart, setShowCart] = useState(false);

  return (
    <ErrorBoundary>
      <Router>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <div className="container">
            <Link className="navbar-brand" to="/">
              Bookstore
            </Link>
            <Button variant="outline-light" onClick={() => setShowCart(true)}>
              Cart
            </Button>
          </div>
        </nav>

        <div className="App container mt-4">
          <Routes>
            <Route
              path="/"
              element={
                <>
                  <BookList />
                  <Cart
                    show={showCart}
                    handleClose={() => setShowCart(false)}
                  />
                </>
              }
            />
          </Routes>
        </div>
      </Router>
    </ErrorBoundary>
  );
}

export default App;
