import Product from 'pages/product/Product';
import Cart from 'pages/Cart';
import ProductDetail from 'pages/product/ProductDetail';
import Login from 'pages/auth/Login';
import Registration from 'pages/auth/Registration';

export const router = [
  { path: '/product', component: <Product />, exact: true },
  { path: '/product/:id', component: <ProductDetail />, exact: true },
  { path: '/category/:id', component: <Product />, exact: true },
  { path: '/cart', component: <Cart />, exact: true },
  { path: '/auth/login', component: <Login />, exact: true},
  { path: '/auth/registration', component: <Registration />, exact: true}
];
