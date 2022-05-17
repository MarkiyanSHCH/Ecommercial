import React from 'react';
import { NavLink } from 'react-router-dom';

const ProductList = ({ product }) => {
  return (
    <div className="col-12 col-md-4 col-lg-3 my-3">
      <NavLink
        to={`/product/${product.id}`}
        style={{ textDecoration: 'none', color: 'inherit' }}>
        <div className="card-sl rounded-3 d-flex flex-column h-100">
          <div className="d-flex justify-content-center pt-2">
            <img
              className="card-image image-max-width rounded-top"
              src={product.photoFileName}
            />
          </div>
          <div className="font-size-lg fw-bold p-2">{product.name}</div>
          <div className="font-size-sm text-muted p-2">${product.price}</div>
          <div className="mt-auto d-flex justify-content-center p-2">
            Detail
          </div>
        </div>
      </NavLink>
    </div>
  );
};

export default ProductList;
