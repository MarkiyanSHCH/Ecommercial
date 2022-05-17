import React, { useCallback, useEffect, useState } from "react";
import CartHttpService from "services/http/CartHttpService";
import OrderHttpService from "services/http/OrderHttpService";
import ShopHttpService from "services/http/ShopHttpService";
import CartLine from "components/cart/CartLine";
import { CART_ITEMS_KEY } from "services/CartService";
import "styles/ProductStyle.css";
import { NavLink } from "react-router-dom";
import OrderModal from "components/modal/order/OrderModal";

const cartHttpService = CartHttpService();
const orderHttpService = OrderHttpService();
const shopHttpService = ShopHttpService();

const Cart = () => {
	const [totalPrice, setTotalPrice] = useState(0);
	const [cartItems, setCartItems] = useState([]);
	const [products, setProducts] = useState([]);
	const [shops, setShops] = useState([]);
	const [showModal, setShowModal] = useState(false);
	const [shopId, setShopId] = useState(1);

	// TODO: Think about how do it better
	useEffect(() => {
		let tmp = 0;
		products.forEach(x => {
			setTotalPrice(
				(tmp +=
					(cartItems.find(y => y.productId === x.id)?.quantity ?? 1) * x.price)
			);
		});
	}, [showModal]);

	useEffect(() => {
		setCartItems(() => {
			const items = JSON.parse(window.localStorage.getItem(CART_ITEMS_KEY) ?? "[]");
			Promise.all([
				cartHttpService(items.map(item => item.productId)),
				shopHttpService.getShops(),
			])
				.then(response => {
					setProducts(response[0].data.products);
					setShops(response[1].data.shops);
				})
				.catch(() => {});
			return items;
		});
	}, []);

	const createOrder = useCallback((e) => {
		e.preventDefault();
		orderHttpService
			.postOrder({
				shopId: shopId,
				totalPrice: totalPrice,
				orderLines: cartItems,
			})
			.then(() => {
				setCartItems([]);
				localStorage.removeItem(CART_ITEMS_KEY);
				setShowModal(false);
			});
	});

	const deleteItem = useCallback(item =>
		setCartItems(cartItems => {
			const items = cartItems.filter(elem => elem.productId !== item.productId);
			window.localStorage.setItem(CART_ITEMS_KEY, JSON.stringify(items));
			return items;
		})
	);

	const openModal = useCallback(() => setShowModal(prev => !prev));

	return (
		<>
			<div className="container py-5">
				<h3 className="mb-4">Shopping Bag ({cartItems.length})</h3>
				{cartItems.length > 0 && products.length > 0 && (
					<div className="font-size-sm">
						<div className="row border-bottom border-dark d-none d-md-flex fw-bold">
							<div className="col-4">Name</div>
							<div className="col-5">Note</div>
							<div className="col-1">
								<i className="bi bi-basket"></i>
							</div>
							<div className="col-1">
								<i className="bi bi-wallet"></i>
							</div>
							<div className="col-1">
								<i className="bi bi-x-lg"></i>
							</div>
						</div>
						{cartItems.map(item => (
							<CartLine
								key={item.productId}
								products={products}
								item={item}
								deleteItem={deleteItem}></CartLine>
						))}
						<div className="d-flex justify-content-end mt-2">
							<button
								className="btn btn-outline-dark px-4 py-2"
								onClick={openModal}>
								Order
							</button>
						</div>
					</div>
				)}

				{cartItems.length === 0 && (
					<div className="row">
						<div className="col-12 col-md-4">
							<h5>Your shopping bag is empty.</h5>
							<NavLink to={`/product`}>
								<button className="btn btn-outline-dark mt-2 px-4 py-2">
									Continue Browsing
								</button>
							</NavLink>
						</div>
					</div>
				)}
			</div>
			<OrderModal
				shops={shops}
				setShopId={setShopId}
				totalPrice={totalPrice}
				createOrder={createOrder}
				isActive={showModal}
				setActive={openModal}
			/>
		</>
	);
};

export default Cart;
