import {useAppDispatch, useAppSelector} from "./reduxHooks.ts";
import useRefreshToken from "./userRefreshToken.ts";
import {useEffect} from "react";
import {axiosApi} from "../api/axiosApi.ts";
import {logout, setCredentials} from "../feutures/auth/authSlice.ts";

const UseAxiosApi = () => {
    const auth = useAppSelector(s => s.auth)
    const dispatch = useAppDispatch()
    const refreshToken = useRefreshToken()

    useEffect(() => {
        const requestInterceptor = axiosApi.interceptors.request.use(
            async (req) => {
                req.headers['Authorization'] = `Bearer ${auth.token}`;
                if (auth.expiryInMilliSecond != undefined) {
                    console.log({
                        now: Date.now(),
                        expiry: auth.expiryInMilliSecond
                    })
                    const now = Math.floor(Date.now() / 1000)
                    const exp = Math.floor(auth.expiryInMilliSecond / 1000)
                    if (now >= exp) {
                        try {
                            const data = await refreshToken()
                            dispatch(setCredentials(data))
                            req.headers['Authorization'] = `Bearer ${data.token}`;
                        } catch (e) {
                            dispatch(logout())
                        }
                    }
                }
                return req;
            },
            (error) => {
                return Promise.reject(error);
            }
        );

        return () => {
            axiosApi.interceptors.request.eject(requestInterceptor);
        };

    }, [auth])


    return axiosApi
};

export default UseAxiosApi;