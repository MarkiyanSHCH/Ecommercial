import React from "react";
import { NavLink } from "react-router-dom";

const AuthInfoText = ({ text, name, url }) => {
  return (
		<div className="text-center text-secondary fst-italic">
			{text}
			<NavLink to={url}>{name}</NavLink>
		</div>
	);
};

export default AuthInfoText;
