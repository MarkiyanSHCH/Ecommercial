import React from "react";

const AuthInput = ({placeholder, pattern, setMethod, type="text"}) => {
  return (
		<div className="d-flex justify-content-center pt-4 w-50">
			<input
				type={type}
				placeholder={placeholder}
				className="border-1 rounded p-3 w-100"
				pattern={pattern}
				onChange={(e) => setMethod(e.target.value)}
				required
			/>
		</div>
	);
};

export default AuthInput;