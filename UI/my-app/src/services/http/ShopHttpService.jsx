import axios from 'axios';

export default function ShopHttpService() {
  const getShops = () =>
   axios.get(`${process.env.REACT_APP_API_URL}shop`);

  const getShopById = (shopId) =>
   axios.get(`${process.env.REACT_APP_API_URL}shop`, { shopId });

  return { getShops, getShopById };
}
