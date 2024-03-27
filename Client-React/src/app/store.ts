import {configureStore} from '@reduxjs/toolkit'
import {baseApi} from "./api/baseApi.ts";
import {authReducer} from "./feutures/auth/authSlice.ts";
import {preferenceReducer} from "@/app/feutures/preferences/preferencesSlice.ts";

export const store = configureStore({
    reducer: {
        'auth': authReducer,
        'userPreference': preferenceReducer,
        [baseApi.reducerPath]: baseApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(baseApi.middleware),
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch