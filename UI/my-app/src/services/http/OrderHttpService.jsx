import axios from "axios";

export default function OrderHttpService() {
	const getOrders = () => axios.get(`${process.env.REACT_APP_API_URL}orders`);

	const getOrderLines = orderId =>
		axios.get(`${process.env.REACT_APP_API_URL}orders/${orderId}/lines`);

	const postOrder = request =>
		axios.post(`${process.env.REACT_APP_API_URL}orders`, {
			shopId: request.shopId,
			totalPrice: request.totalPrice,
			orderLines: request.orderLines,
		});

	return {
		getOrders,
		getOrderLines,
		postOrder,
	};
}
