import React, { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ProductHttpService from "services/http/ProductHttpService";
import CartModal from "components/modal/cart/CartModal";

const productService = ProductHttpService();

const ProductDetail = () => {
	const params = useParams();
	const [product, setProduct] = useState();
	const [showModal, setShowModal] = useState(false);

	const openModal = useCallback(() => {
		setShowModal(prev => !prev);
	});

	useEffect(() => {
		productService.getProductById(params.id).then(response => {
			setProduct(response.data);
		});
	}, []);

	return (
		<>
			{product && (
				<div className="container my-5">
					<div className="card overflow-hidden border-0">
						<div className="row">
							<div className="col-12 col-md-6 border-end">
								<div className="d-flex flex-column">
									<img src={product.photoFileName} />
								</div>
							</div>
							<div className="col-12 col-md-6">
								<div className="p-3 position-relative">
									<div className="d-flex justify-content-between align-items-center">
										<h3>{product.name}</h3>
									</div>
									<div className="mt-2 pr-3 content">
										<p>{product.description}</p>
									</div>
									<h3>$ {product.price}</h3>
									{product.properties.length > 0 && (
										<div className="mt-4">
											<span className="fw-bold">
												Characteristics :{" "}
											</span>
											{product.properties.map(property => (
												<div className="mt-1 text-center">
													<span>
														{property.name} : {property.value}
													</span>
												</div>
											))}
										</div>
									)}
									<div className="text-center mt-5">
										<button
											className="btn btn-outline-dark px-5 py-3"
											onClick={openModal}>
											Buy Now
										</button>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			)}
			<CartModal product={product} isActive={showModal} setActive={openModal} />
		</>
	);
};

export default ProductDetail;
