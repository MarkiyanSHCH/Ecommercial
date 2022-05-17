import React, { useEffect, useState } from "react";
import "./CartModalStyle.css";
import { CART_ITEMS_KEY } from "services/CartService";
import Modal from "components/modal/Modal";

const CartModal = ({ product, isActive, setActive }) => {
	const [quantity, setQuantity] = useState(1);
	const [note, setNote] = useState("");

	let cartItems = [];

	useEffect(() => {
		cartItems = JSON.parse(localStorage.getItem(CART_ITEMS_KEY) || "[]");
		let item = cartItems.find(x => x.productId === product?.id);
		setNote(item?.note ?? "");
		setQuantity(item?.quantity ?? 1);
	}, []);

	const addItemCart = e => {
		e.preventDefault();
		cartItems = JSON.parse(localStorage.getItem(CART_ITEMS_KEY) || "[]");

		setQuantity(cartItems.find(x => x.productId === product?.id)?.quantity ?? 0);

		cartItems = cartItems.filter(x => x.productId !== product?.id);
		cartItems.push({
			productId: product?.id,
			quantity: quantity,
			note: note,
		});

		localStorage.setItem(CART_ITEMS_KEY, JSON.stringify(cartItems));
		setActive();
	};

	const increment = e => {
		e.preventDefault();
		setQuantity(quantity + 1);
	};

	const decrement = e => {
		e.preventDefault();
		if (quantity > 1) setQuantity(quantity - 1);
	};

	return (
		<Modal isActive={isActive} setActive={setActive}>
			<div className="modal-body text-transform-none">
				<form name="form" className="input-group">
					<div className="container">
						<div className="form-group">
							<img
								src={product?.photoFileName}
								className="image image-max-width"
							/>
							<span> {product?.name}</span>
						</div>
						<div className="form-group required">
							<label>Note</label>
							<div className="d-flex justify-content-center">
								<textarea
									type="text"
									className="form-control"
									value={note}
									onChange={e => setNote(e.target.value)}></textarea>
							</div>
						</div>
						<div className="form-group">
							<label>Quantity</label>
							<div className="d-flex justify-content-center">
								<button
									type="button"
									className="btn"
									onClick={e => decrement(e)}>
									<i className="bi bi-dash-square"></i>
								</button>
								<input
									type="number"
									className="form-control"
									value={quantity}
									onChange={e => setQuantity(e.target.value)}
									disabled
								/>
								<button
									type="button"
									className="btn"
									onClick={e => increment(e)}>
									<i className="bi bi-plus-square"></i>
								</button>
							</div>
							<div className="invalid-feedback">
								<div>Quantity is invalid.</div>
								<div>Quantity is required.</div>
							</div>
						</div>
						<button
							className="btn btn-outline-dark w-100 mt-3 py-2"
							onClick={e => addItemCart(e)}>
							Add
						</button>
					</div>
				</form>
			</div>
		</Modal>
	);
};

export default CartModal;
