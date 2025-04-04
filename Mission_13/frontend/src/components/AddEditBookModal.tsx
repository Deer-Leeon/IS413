import React, { useState, useEffect } from "react";
import { Form, Modal, Button } from "react-bootstrap";
import { Book } from "./BookList";

interface Props {
  show: boolean;
  book?: Book;
  onClose: () => void;
  onSubmit: (book: Book) => void;
}

export default function AddEditBookModal({
  show,
  book,
  onClose,
  onSubmit,
}: Props) {
  const [formData, setFormData] = useState<Book>(
    book || {
      bookID: 0,
      title: "",
      author: "",
      publisher: "",
      isbn: "",
      classification: "",
      category: "",
      pageCount: 0,
      price: 0,
    }
  );

  return (
    <Modal show={show} onHide={onClose}>
      <Modal.Header closeButton>
        <Modal.Title>{book ? "Edit Book" : "Add New Book"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          {/* Add form fields for all book properties */}
          <Form.Group className="mb-3">
            <Form.Label>Title</Form.Label>
            <Form.Control
              value={formData.title}
              onChange={(e) =>
                setFormData({ ...formData, title: e.target.value })
              }
            />
          </Form.Group>
          {/* Repeat for other fields */}
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Close
        </Button>
        <Button variant="primary" onClick={() => onSubmit(formData)}>
          Save Changes
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
