import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import AuthInterceptor from 'interceptor/auth-interceptor';

AuthInterceptor();

ReactDOM.render(<App />, document.getElementById('root'));
