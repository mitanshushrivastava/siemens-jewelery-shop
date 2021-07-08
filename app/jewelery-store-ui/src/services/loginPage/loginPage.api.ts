import axios from "axios";
import { ILoginResponse } from "./loginPage.contracts";

const checkUserLogin = async (userName: string, password: string): Promise<ILoginResponse> => {
    const response = await axios.post<ILoginResponse>("http://localhost:5000/UserAuthentication/authenticate", {
        UserName: userName,
        Password: password
    });

    console.log(response.data);

    return response.data;
}

export const LoginPageApi = {
    checkUserLogin,
};