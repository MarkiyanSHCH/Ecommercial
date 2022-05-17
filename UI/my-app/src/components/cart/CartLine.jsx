import React from 'react';

const CartLine = ({ products, item, deleteItem }) => {
  const getProductById = (productId) =>
    products.find((x) => x.id === productId);

  const del = () => deleteItem(item);
  
  return (
    <div className="row border-bottom border-dark py-2">
      <div className="col-12 col-md-4">
        <span className="d-md-none fw-bold">Name: </span>
        <span>{getProductById(item.productId).name}</span>
      </div>
      <div className="col-12 col-md-5">
        <span className="d-md-none fw-bold">Note: </span>
        <span>{item?.note}</span>
      </div>
      <div className="col-12 col-md-1">
        <span className="d-md-none fw-bold">Quantity: </span>
        <span>{item.quantity}</span>
      </div>
      <div className="col-12 col-md-1 d-flex justify-content-between mb-2">
        <span>
          <span className="d-md-none fw-bold">Price: </span>
          <span>${item.quantity * getProductById(item.productId).price}</span>
        </span>
        <button className="btn btn-outline-dark d-md-none">Delete</button>
      </div>
      <div className="col-12 col-md-1">
        <button className="btn d-none d-md-block" onClick={del}>
          <i className="bi bi-trash"></i>
        </button>
      </div>
    </div>
  );
};

export default CartLine;
