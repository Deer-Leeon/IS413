import { useState, useEffect } from "react";
import {
  Table,
  Pagination,
  FormSelect,
  Container,
  Form,
  Button,
  Badge,
  Row,
  Col,
  Modal,
} from "react-bootstrap";
import axios from "axios";

export interface Book {
  bookID: number;
  title: string;
  author: string;
  publisher: string;
  isbn: string;
  classification: string;
  category: string;
  pageCount: number;
  price: number;
}

interface CartItem extends Book {
  quantity: number;
}

export default function BookList() {
  const [books, setBooks] = useState<Book[]>([]);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(20);
  const [totalCount, setTotalCount] = useState(0);
  const [sortBy, setSortBy] = useState("title");
  const [searchTitle, setSearchTitle] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("");
  const [showCart, setShowCart] = useState(false);
  const [cartCount, setCartCount] = useState(0);
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  // Reset page to 1 when filters change
  useEffect(() => {
    setPage(1);
  }, [searchTitle, selectedCategory]);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        // In the fetchBooks function, modify the axios request params
        const response = await axios.get(`/api/books`, {
          params: {
            page,
            pageSize,
            sortBy,
            searchTitle, // Convert to lowercase
            category: selectedCategory,
          },
        });
        setBooks(response.data.results);
        setTotalCount(response.data.totalCount);
      } catch (error) {
        console.error("Error fetching books:", error);
      }
    };

    fetchBooks();
  }, [page, pageSize, sortBy, searchTitle, selectedCategory]);

  useEffect(() => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]");
    setCartCount(
      cart.reduce((sum: number, item: CartItem) => sum + item.quantity, 0)
    );
  }, [showCart]);

  const addToCart = (book: Book) => {
    const cart: CartItem[] = JSON.parse(sessionStorage.getItem("cart") || "[]");
    const existingItem = cart.find((item) => item.bookID === book.bookID);

    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      cart.push({ ...book, quantity: 1 });
    }

    sessionStorage.setItem("cart", JSON.stringify(cart));
    setCartCount(cart.reduce((sum, item) => sum + item.quantity, 0));
  };

  const handleShowCart = () => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]");
    setCartItems(cart);
    setShowCart(true);
  };

  return (
    <Container className="mt-4">
      <Row className="mb-3 align-items-center">
        <Col md={4}>
          <Form.Control
            type="text"
            placeholder="Search by title..."
            value={searchTitle}
            onChange={(e) => setSearchTitle(e.target.value)}
          />
        </Col>

        <Col md={3}>
          <FormSelect
            value={selectedCategory}
            onChange={(e) => setSelectedCategory(e.target.value)}
          >
            <option value="">All Categories</option>
            <option value="Biography">Biography</option>
            <option value="Self-Help">Self-Help</option>
            <option value="Fiction">Fiction</option>
            <option value="Non-Fiction">Non-Fiction</option>
            <option value="Technology">Technology</option>
          </FormSelect>
        </Col>

        <Col md={3}>
          <FormSelect
            value={sortBy}
            onChange={(e) => setSortBy(e.target.value)}
          >
            <option value="title">Sort by Title</option>
            <option value="author">Sort by Author</option>
            <option value="price">Sort by Price</option>
            <option value="pages">Sort by Pages</option>
            <option value="category">Sort by Category</option>
          </FormSelect>
        </Col>

        <Col md={2} className="text-end">
          <Button variant="primary" onClick={handleShowCart}>
            Cart{" "}
            <Badge bg="light" text="dark">
              {cartCount}
            </Badge>
          </Button>
        </Col>
      </Row>

      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Title</th>
            <th>Author</th>
            <th>Publisher</th>
            <th>ISBN</th>
            <th>Classification</th>
            <th>Category</th>
            <th>Pages</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {books.map((book) => (
            <tr key={book.bookID}>
              <td>{book.title}</td>
              <td>{book.author}</td>
              <td>{book.publisher}</td>
              <td>{book.isbn}</td>
              <td>{book.classification}</td>
              <td>{book.category}</td>
              <td>{book.pageCount}</td>
              <td>${book.price.toFixed(2)}</td>
              <td>
                <Button
                  variant="success"
                  size="sm"
                  onClick={() => addToCart(book)}
                >
                  Add to Cart
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Row className="justify-content-between align-items-center">
        <Col md={3}>
          <FormSelect
            style={{ width: "150px" }}
            value={pageSize}
            onChange={(e) => {
              setPage(1);
              setPageSize(Number(e.target.value));
            }}
          >
            <option value="5">5 per page</option>
            <option value="10">10 per page</option>
            <option value="20">20 per page</option>
          </FormSelect>
        </Col>

        <Col md={9} className="d-flex justify-content-end">
          <Pagination>
            {Array.from({ length: Math.ceil(totalCount / pageSize) }).map(
              (_, index) => (
                <Pagination.Item
                  key={index + 1}
                  active={index + 1 === page}
                  onClick={() => setPage(index + 1)}
                >
                  {index + 1}
                </Pagination.Item>
              )
            )}
          </Pagination>
        </Col>
      </Row>

      <Modal show={showCart} onHide={() => setShowCart(false)} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Shopping Cart</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Title</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Subtotal</th>
              </tr>
            </thead>
            <tbody>
              {cartItems.map((item) => (
                <tr key={item.bookID}>
                  <td>{item.title}</td>
                  <td>${item.price.toFixed(2)}</td>
                  <td>{item.quantity}</td>
                  <td>${(item.price * item.quantity).toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </Table>
          <h4 className="text-end">
            Total: $
            {cartItems
              .reduce((sum, item) => sum + item.price * item.quantity, 0)
              .toFixed(2)}
          </h4>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowCart(false)}>
            Continue Shopping
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
}
