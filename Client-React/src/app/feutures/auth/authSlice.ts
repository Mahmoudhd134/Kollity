import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {TokenModel} from "@/app/feutures/auth/tokenModel.ts";

const initialState: Partial<TokenModel> = {}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action: PayloadAction<TokenModel>) => {
            const {email, expiry, profileImage, token, userName, roles, id} = action.payload
            state.roles = roles
            state.token = token
            state.id = id
            state.userName = userName
            state.email = email
            state.expiry = expiry
            state.expiryInMilliSecond = new Date(expiry).getTime()
            state.profileImage = profileImage
        },
        logout: (state) => {
            localStorage.removeItem('stayLogin')
            state.roles = undefined
            state.token = undefined
            state.id = undefined
            state.userName = undefined
            state.email = undefined
            state.expiry = undefined
            state.expiryInMilliSecond = undefined
            state.profileImage = undefined
        }
    }
})

export const authReducer = authSlice.reducer
export const {logout, setCredentials} = authSlice.actions