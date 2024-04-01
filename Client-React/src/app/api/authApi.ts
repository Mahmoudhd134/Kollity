import {baseApi} from "@/app/api/baseApi.ts";
import {TokenModel} from "@/app/feutures/auth/tokenModel.ts";
import LoginModel from "models/Auth/LoginModel.ts";

export const authApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation<TokenModel, LoginModel>({
            query: (cred) => ({
                url: '/auth/login',
                method: 'Post',
                body: cred
            }),
        }),
        logout: builder.mutation<void, void>({
            query: () => ({
                url: '/auth/logout',
                method: 'delete'
            })
        })
    })
})

export const {
    useLoginMutation,
    useLogoutMutation,
} = authApi