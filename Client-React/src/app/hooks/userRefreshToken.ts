import axios from "axios";

import {TokenModel} from "@/app/feutures/auth/tokenModel.ts";

const useRefreshToken = () => {
    return async () => {
        try {
            const refreshToken = await axios.post<TokenModel>(
                `${import.meta.env.VITE_BASE_URL}/auth/refresh-token`, null,{
                    withCredentials: true
                })
            return refreshToken.data;
        } catch (e) {
            throw e
        }
    }
}

export default useRefreshToken