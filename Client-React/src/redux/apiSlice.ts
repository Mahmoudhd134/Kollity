import { logOut } from "@/redux/authSlice";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { RootState } from "./store";

export const apiSlice = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_URL,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState)?.auth?.token;

      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
      }

      return headers;
    },
  }),
  endpoints: (builder) => ({
    // Auth
    logOut: builder.mutation({
      query: () => ({
        url: "Auth/logout",
        method: "DELETE",
      }),
      onQueryStarted: (_, { dispatch }) => {
        dispatch(logOut());
      },
    }),
    // Student
    getStudentProfileData: builder.query({
      query: ({ id }: { id: string }) => `Student/${id}`,
    }),
    getDoctorProfileData: builder.query({
      query: ({ id }: { id: string }) => `Doctor/${id}`,
    }),
  }),
});

export const {
  useLogOutMutation,
  useLazyGetStudentProfileDataQuery,
  useLazyGetDoctorProfileDataQuery,
} = apiSlice;
export default apiSlice;
