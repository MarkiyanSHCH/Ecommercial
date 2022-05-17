import React from "react";
import AuthInput from "components/auth/AuthInput";
import AuthButton from "components/auth/AuthButton";
import AuthInfoText from "components/auth/AuthInfoText";

const Registration = () => {
	return (
		<div className="container mt-5 py-5">
			<div className="card">
				<div className="card-body d-flex align-items-center flex-column my-5">
					<h5 className="text-center">Register</h5>
					<AuthInfoText
						text={`Already have an account? `}
						url={`/auth/login`}
						name={`Login`}
					/>

					<AuthInput placeholder={`Full Name`} pattern={"[A-Za-zÀ-ÿ]+"} />
					<AuthInput placeholder={`Email`} pattern={"[A-Za-zÀ-ÿ0-9.@]+"} />
					<AuthInput
						placeholder={`Phone`}
						pattern={"[0-9]+"}
						type={`number`}
					/>
					<AuthInput
						placeholder={`Password`}
						pattern={"[0-9]+"}
						type={`password`}
					/>

					<AuthButton name={`Sign up`} />
				</div>
			</div>
		</div>
	);
};

export default Registration;
