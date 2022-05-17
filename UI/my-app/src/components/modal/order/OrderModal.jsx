import React from "react";
import Modal from "components/modal/Modal";

const OrderModal = ({ shops, setShopId, totalPrice, createOrder, isActive, setActive }) => {

	const handleAddrChange = e => setShopId(e.target.value);

	return (
		<Modal isActive={isActive} setActive={setActive}>
			<div className="modal-body text-transform-none">
				<form name="form" className="input-group">
					<div className="container">
						<div className="form-group required">
							<label>Select address: </label>
							<select
								className="form-control"
								onChange={e => handleAddrChange(e)}
								type="text"
								name="address"
								required>
								{shops.map(shop => (
									<option key={shop.id} value={shop.id}>{shop.address}</option>
								))}
							</select>
							<div className="invalid-feedback">address is required.</div>
						</div>
						<div className="form-group d-flex justify-content-end">
							<span>Subtotal: {totalPrice}</span>
						</div>
						<button
							onClick={createOrder}
							className="btn btn-outline-dark w-100 mt-3 py-2">
							Submit
						</button>
					</div>
				</form>
			</div>
		</Modal>
	);
};

export default OrderModal;
