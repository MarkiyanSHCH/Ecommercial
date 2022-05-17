import './Loader.css';
import logo from '../../../assets/gif/spinner.gif'

import React from 'react';

const Loader = () => {
  return (
    <div className="spinner-wrapper">
      <div className="loadable-content"></div>
      <div className="spinner">
        <div className="text-center">
          <img src={logo} />
        </div>
      </div>
    </div>
  );
};

export default Loader;
