import {BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError} from "@reduxjs/toolkit/query/react";
import {RootState} from "../store.ts";
import {logout, setCredentials} from "../feutures/auth/authSlice.ts";
import useRefreshToken from "../hooks/userRefreshToken.ts";

export const authorizedSend = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_BASE_URL,
    credentials: 'include',
    prepareHeaders: (headers, {getState}) => {
        const token = (getState() as RootState).auth.token
        if (token != null)
            headers.append('authorization', `Bearer ${token}`)
    }
})

const baseQuery: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    const auth = (api.getState() as RootState).auth
    if (auth.expiryInMilliSecond != undefined) {
        const now = Math.floor(Date.now() / 1000)
        const exp = Math.floor(auth.expiryInMilliSecond / 1000)
        if (now >= exp) {
            try {
                const refreshToken = useRefreshToken()
                const data = await refreshToken()
                api.dispatch(setCredentials(data))
            } catch (e) {
                api.dispatch(logout())
            }
        }
    }
    return authorizedSend(args, api, extraOptions);
}
export const baseApi = createApi({
    reducerPath: 'api',
    baseQuery,
    tagTypes: [],
    endpoints: () => ({}),
    keepUnusedDataFor: 0
})