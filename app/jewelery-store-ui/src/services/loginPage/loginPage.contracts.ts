export interface ILoginRequest {
    userName: string;
    password: string;
}

export interface ILoginResponse {
    code: number;
    message: string;
    data: IAuthenticationTokenResponse;
}

export type IAuthenticationTokenResponse = {
    AuthenticationToken: string;
}