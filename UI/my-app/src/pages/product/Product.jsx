import React, { useEffect, useState } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import ProductHttpService from 'services/http/ProductHttpService';
import ProductList from 'components/products/ProductList';
import 'styles/ProductStyle.css';

const productService = ProductHttpService();

const Product = () => {
  const params = useParams();
  const [products, setProducts] = useState([]);
  let location = useLocation();

  useEffect(() => {
    if (window.location.pathname === '/product')
      productService
        .getProducts()
        .then((response) => setProducts(response.data.products));
    else
      productService
        .getProductByCategory(params.id)
        .then((response) => setProducts(response.data.products));
  }, [location]);

  return (
    <div className="container">
      <div className="row">
        {products.map((product) => (
          <ProductList key={product.id} product={product}></ProductList>
        ))}
      </div>
    </div>
  );
};

export default Product;
