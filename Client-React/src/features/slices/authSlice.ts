import { createSlice } from "@reduxjs/toolkit";
import profileImgSrc from "../../assets/images/default_user.png";

export type roles = "Student" | "Assistant" | "Doctor";

export interface IAuth {
  roles: roles[];
  id: string;
  token: string;
  expiry: string;
  userName: string;
  email: string;
  profileImage: string;
  isAuth: boolean;
}

const initialState: IAuth = {
  roles: [],
  id: "",
  token: "",
  expiry: "",
  userName: "",
  email: "",
  profileImage: "",
  isAuth: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login(state, action) {
      state.roles = action.payload.roles;
      state.id = action.payload.id;
      state.token = action.payload.token;
      state.expiry = action.payload.expiry;
      state.userName = action.payload.userName;
      state.email = action.payload.email;
      state.profileImage = action.payload.profileImage || profileImgSrc;
      state.isAuth = true;
    },
    logout(state) {
      state.roles = [];
      state.id = "";
      state.token = "";
      state.expiry = "";
      state.userName = "";
      state.email = "";
      state.profileImage = "";
      state.isAuth = false;
    },
  },
});

export const selectAuth = (state: { auth: IAuth }) => state.auth;
export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
