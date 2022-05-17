import React from 'react';
import { NavLink } from 'react-router-dom';

const NavbarButton = ({ url, name, style }) => {
  return (
    <NavLink
      className={style}
      to={url}>
      {name}
    </NavLink>
  );
};

export default NavbarButton;
