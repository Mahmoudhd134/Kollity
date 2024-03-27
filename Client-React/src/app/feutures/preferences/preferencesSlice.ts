import {createSlice} from "@reduxjs/toolkit";
import {UserPreferenceModel} from "@/app/feutures/preferences/userPreferenceModel.ts";

const initialState: UserPreferenceModel = {
    isDarkModeActive: false
}
const preferencesSlice = createSlice({
    name: 'preferencesSlice',
    initialState,
    reducers: {
        switchDarkMode: (state) => {
            state.isDarkModeActive = !state.isDarkModeActive
        }
    }
})

export const preferenceReducer = preferencesSlice.reducer
export const {
    switchDarkMode
} = preferencesSlice.actions
