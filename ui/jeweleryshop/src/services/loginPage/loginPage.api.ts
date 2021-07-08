import axios from "axios";
import { ILoginResponse } from "./loginPage.contracts";
import { LoginUrl } from "../endpoints";

const checkUserLogin = async (userName: string, password: string): Promise<ILoginResponse> => {
    const response = await axios.post<ILoginResponse>(LoginUrl, {
        UserName: userName,
        Password: password
    });

    console.log(response.data);

    return response.data;
}

export const LoginPageApi = {
    checkUserLogin,
};