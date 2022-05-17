import React, { useCallback, useState } from "react";
import { useNavigate } from "react-router-dom";
import AuthInput from "components/auth/AuthInput";
import AuthButton from "components/auth/AuthButton";
import AuthInfoText from "components/auth/AuthInfoText";
import AuthHttpService from "services/http/AuthHttpService";

const authHttpService = AuthHttpService();

const Login = () => {
	const navigate = useNavigate();

	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");

	const submitLogin = useCallback(e => {
		e.preventDefault();
		authHttpService
			.login({
				email: email,
				password: password,
			})
			.then(() => {
				navigate(-1);
			});
	});

	return (
		<div className="container mt-5 py-5">
			<div className="card">
				<div className="card-body d-flex align-items-center flex-column my-5">
					<h5 className="text-center">Login</h5>
					<AuthInfoText
						text={`Don't have an account yet? `}
						url={`/auth/registration`}
						name={`Register`}
					/>

					<AuthInput
						placeholder={`Email`}
						pattern={"[A-Za-zÀ-ÿ0-9.@]+"}
						setMethod={setEmail}
					/>
					<AuthInput
						placeholder={`Password`}
						pattern={"[A-Za-zÀ-ÿ0-9.@]+"}
						setMethod={setPassword}
						type={`password`}
					/>

					<AuthButton name={`Login`} submit={submitLogin} />
				</div>
			</div>
		</div>
	);
};

export default Login;
