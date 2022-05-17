import React, { useEffect, useState } from "react";
import CategoryHttpService from "services/http/CategoryHttpService";
import NavbarButton from "components/core/Header/NavbarButton";

const categoryService = CategoryHttpService();

const Navbar = () => {
	const [categories, setCategories] = useState([]);

	useEffect(() => {
		categoryService
			.getCategories()
			.then(response => setCategories(response.data.categories));
	}, []);

	return (
		<nav className="navbar navbar-expand-lg navbar-dark bg-dark mb-4">
			<div className="container-xl">
				<button
					className="navbar-toggler"
					type="button"
					data-bs-toggle="collapse"
					data-bs-target="#collapseTarget">
					<span className="navbar-toggler-icon"></span>
				</button>
				<div className="collapse navbar-collapse" id="collapseTarget">
					<ul className="navbar-nav me-auto mb-2 mb-lg-0">
						<NavbarButton
							url={`/product`}
							name={"All"}
							style={`nav-item nav-link`}
						/>
						{categories.map(category => (
							<li key={category.id} className="nav-item">
								<NavbarButton
									url={`/category/${category.id}`}
									name={category.name}
									style={`nav-item nav-link`}
								/>
							</li>
						))}
					</ul>
					<ul className="navbar-nav mr-right">
						<li className="nav-item">
							<NavbarButton
								url={`/cart`}
								name={
									<>
										<i className="bi bi-cart4"></i> Cart
									</>
								}
								style={`nav-item nav-link`}
							/>
						</li>
						{true && (
							<li className="nav-item">
								<NavbarButton
									url={`/auth/login`}
									name={
										<>
											<i className="bi bi-box-arrow-in-right"></i>{" "}
											Login
										</>
									}
									style={`nav-item nav-link`}
								/>
							</li>
						)}
					</ul>
					{false && (
						<ul className="navbar-nav mr-right">
							<li className="nav-item dropdown">
								<a
									className="nav-link dropdown-toggle"
									data-bs-toggle="dropdown">
									<i className="bi bi-person-lines-fill"></i>
								</a>
								<div
									className="dropdown-menu dropdown-menu-end"
									id="dropdown-account">
									<NavbarButton
										url={`/account/account-info`}
										name={
											<>
												<i className="bi bi-person-circle"></i>{" "}
												Account
											</>
										}
										style={`dropdown-item`}
									/>
									<NavbarButton
									url={`/account/order`}
									name={
										<>
											<i className="bi bi-card-list"></i> Order
										</>
									}
									style={`dropdown-item`}
									/>
									<NavbarButton
										url={''}
										name={
											<>
													<i className="bi bi-door-open"></i> Logout
											</>
										}
										style={`dropdown-item`}
									/>
								</div>
							</li>
						</ul>
					)}
				</div>
			</div>
		</nav>
	);
};

export default Navbar;
