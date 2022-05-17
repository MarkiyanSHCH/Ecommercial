import axios from 'axios';
import qs from 'qs';

export default function CartHttpService() {
  const getCartItem = (productIds) => {
    const queryParams = qs.stringify(
      { productIds },
      {
        encode: false,
        allowDots: true,
      }
    );
    return axios.get(`${process.env.REACT_APP_API_URL}cart?${queryParams}`);
  };

  return getCartItem;
}
