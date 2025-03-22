import { useState, useEffect } from "react";
import {
  Table,
  Pagination,
  FormSelect,
  Container,
  Form,
} from "react-bootstrap";
import axios from "axios";

interface Book {
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

export default function BookList() {
  const [books, setBooks] = useState<Book[]>([]);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalCount, setTotalCount] = useState(0);
  const [sortBy, setSortBy] = useState("title");
  const [searchTitle, setSearchTitle] = useState("");

  // Reset page to 1 when search term changes
  useEffect(() => {
    setPage(1);
  }, [searchTitle]);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const response = await axios.get(`/api/books`, {
          params: { page, pageSize, sortBy, searchTitle },
        });
        setBooks(response.data.results);
        setTotalCount(response.data.totalCount);
      } catch (error) {
        console.error("Error fetching books:", error);
      }
    };

    fetchBooks();
  }, [page, pageSize, sortBy, searchTitle]);

  return (
    <Container className="mt-4">
      <div className="d-flex justify-content-between mb-3">
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

        <FormSelect
          style={{ width: "180px" }}
          value={sortBy}
          onChange={(e) => setSortBy(e.target.value)}
        >
          <option value="title">Sort by Title</option>
          <option value="author">Sort by Author</option>
          <option value="price">Sort by Price</option>
          <option value="pages">Sort by Pages</option>
          <option value="category">Sort by Category</option>
        </FormSelect>
      </div>

      <Form.Group className="mb-3">
        <Form.Control
          type="text"
          placeholder="Search by title..."
          value={searchTitle}
          onChange={(e) => setSearchTitle(e.target.value)}
        />
      </Form.Group>

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
            </tr>
          ))}
        </tbody>
      </Table>

      <div className="d-flex justify-content-center">
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
      </div>
    </Container>
  );
}
