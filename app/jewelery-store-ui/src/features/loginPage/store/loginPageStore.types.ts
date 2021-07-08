import { ILoginResponse } from '../../../services/loginPage/loginPage.contracts';

export type ILoginCheckState = {
    isPending: boolean;
    isAccessGranted: boolean;
    message?: string;
    actualResponse?: ILoginResponse;
}

export type LoginPageState = {
    userName?: string;
    password?: string;
    isLoginButtonEnabled: boolean;
    loginCheck: ILoginCheckState;
}