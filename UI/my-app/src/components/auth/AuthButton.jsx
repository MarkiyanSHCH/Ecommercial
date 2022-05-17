import React from "react";

const AuthButton = ({name, submit}) => {
  return (
		<div className="py-2 my-2 w-50">
			<button type="submit" className="btn btn-outline-dark w-100 mt-3 p-3" onClick={submit}>
				{name}
			</button>
		</div>
	);
};


export default AuthButton;
