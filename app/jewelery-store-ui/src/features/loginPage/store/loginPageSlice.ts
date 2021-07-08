import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { LoginPageState, ILoginCheckState } from './loginPageStore.types'

const initialState : LoginPageState = {
    isLoginButtonEnabled: false,
    loginCheck: {
      isPending: false,
      isAccessGranted: false,
    }
};

const loginPageSlice = createSlice({
    name: '$features/loginPage',
    initialState,
    reducers: {
      updateLoginCheckState: (state: LoginPageState, action: PayloadAction<ILoginCheckState>) => {
        state.loginCheck = { ...state.loginCheck, ...action.payload };
      },
      resetLoginCheckState: (state:LoginPageState) => {
        state.loginCheck = initialState.loginCheck;
      },
    },
});

export const { updateLoginCheckState, resetLoginCheckState } = loginPageSlice.actions;

export default loginPageSlice.reducer;