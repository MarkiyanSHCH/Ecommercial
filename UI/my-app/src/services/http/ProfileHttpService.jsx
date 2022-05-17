import axios from 'axios';

export default function ProfileHttpService() {
  const getProfile = () => axios.get(`${process.env.REACT_APP_API_URL}profile`);

  const updateProfile = (name) =>
    axios.patch({
      url: `${process.env.REACT_APP_API_URL}profile/edit/name`,
      data: name,
    });

  const getProductByCategory = (changePassword) =>
    axios.patch(
      `${process.env.REACT_APP_API_URL}profile/edit/password`,
      changePassword
    );

  return {
    getProfile,
    updateProfile,
    getProductByCategory,
  };
}
