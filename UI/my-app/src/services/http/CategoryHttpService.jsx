import axios from 'axios';

export default function CategoryHttpService() {
  const getCategories = () =>
   axios.get(`${process.env.REACT_APP_API_URL}category`);

  return { getCategories };
}
