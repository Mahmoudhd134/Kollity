import {SerializedError} from "@reduxjs/toolkit"
import {AxiosError} from "axios";
import {FetchBaseQueryError} from "@reduxjs/toolkit/query/react";
import AppError from "models/AppError.ts";

const getAppError = (error: FetchBaseQueryError | SerializedError | AxiosError | undefined) => {
    // if (typeof error == typeof AxiosError)
    //     return error?.response.data as AppError
    return error == undefined ? error : (error as FetchBaseQueryError).data as AppError
}

export default getAppError