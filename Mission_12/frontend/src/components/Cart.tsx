import { useEffect, useState } from "react";
import { Button, Modal, Table } from "react-bootstrap";
import { Book } from "./BookList";

interface CartItem extends Book {
  quantity: number;
}

interface CartProps {
  show: boolean;
  handleClose: () => void;
  onCartChange?: () => void;
}

export default function Cart({ show, handleClose, onCartChange }: CartProps) {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  useEffect(() => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]");
    setCartItems(cart);
  }, [show]);

  const handleQuantityChange = (itemId: number, newQuantity: number) => {
    const updatedCart = cartItems
      .map((item) => {
        if (item.bookID === itemId) {
          return { ...item, quantity: Math.max(newQuantity, 0) };
        }
        return item;
      })
      .filter((item) => item.quantity > 0);

    sessionStorage.setItem("cart", JSON.stringify(updatedCart));
    setCartItems(updatedCart);
    onCartChange?.();
  };

  const handleRemove = (itemId: number) => {
    const updatedCart = cartItems.filter((item) => item.bookID !== itemId);
    sessionStorage.setItem("cart", JSON.stringify(updatedCart));
    setCartItems(updatedCart);
    onCartChange?.();
  };

  const total = cartItems.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0
  );

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Shopping Cart</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {cartItems.length === 0 ? (
          <p className="text-center">Your cart is empty</p>
        ) : (
          <>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Title</th>
                  <th>Price</th>
                  <th>Quantity</th>
                  <th>Subtotal</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {cartItems.map((item) => (
                  <tr key={item.bookID}>
                    <td>{item.title}</td>
                    <td>${item.price.toFixed(2)}</td>
                    <td>
                      <div className="d-flex align-items-center gap-2">
                        <Button
                          variant="outline-secondary"
                          size="sm"
                          onClick={() =>
                            handleQuantityChange(item.bookID, item.quantity - 1)
                          }
                        >
                          -
                        </Button>
                        <span>{item.quantity}</span>
                        <Button
                          variant="outline-secondary"
                          size="sm"
                          onClick={() =>
                            handleQuantityChange(item.bookID, item.quantity + 1)
                          }
                        >
                          +
                        </Button>
                      </div>
                    </td>
                    <td>${(item.price * item.quantity).toFixed(2)}</td>
                    <td>
                      <Button
                        variant="danger"
                        size="sm"
                        onClick={() => handleRemove(item.bookID)}
                      >
                        Remove
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <h4 className="text-end">Total: ${total.toFixed(2)}</h4>
          </>
        )}
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Continue Shopping
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
