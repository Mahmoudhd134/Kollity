import { Role } from "@/constants";
import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
export interface IAuth {
  roles: Role[];
  token: string;
  userName: string;
  email: string;
  profileImage: string | null;
}

const initialState: IAuth & { isAuthed: boolean } = {
  roles: [],
  token: "",
  userName: "",
  email: "",
  profileImage: null,
  isAuthed: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    signIn(state, action: PayloadAction<IAuth>) {
      const { roles, token, userName, email, profileImage } = action.payload;

      state.roles = roles;
      state.token = token;
      // state.username = username;
      state.userName = userName;
      state.email = email;
      // state.profileImage = profileImage;
      state.profileImage =
        "https://pbs.twimg.com/profile_images/1593304942210478080/TUYae5z7_400x400.jpg";
      state.isAuthed = true;
    },

    logOut(state) {
      state.roles = [];
      state.token = "";
      state.userName = "";
      state.email = "";
      state.profileImage = "";
      state.isAuthed = false;
    },
  },
});

export const selectAuth = (state: { auth: IAuth }) => state.auth;
export const { signIn, logOut } = authSlice.actions;
export default authSlice.reducer;
