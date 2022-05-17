import axios from 'axios';

export default function ProductHttpService() {
  const getProducts = () =>
    axios.get(`${process.env.REACT_APP_API_URL}product`);

  const getProductById = (id) =>
    axios.get(`${process.env.REACT_APP_API_URL}product/${id}`);

  const getProductByCategory = (id) =>
    axios.get(`${process.env.REACT_APP_API_URL}product/category/${id}`);

  return {
    getProducts,
    getProductById,
    getProductByCategory,
  };
}
