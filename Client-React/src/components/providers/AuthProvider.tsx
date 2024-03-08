import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import api from "../../features/api/api";
import { login } from "../../features/slices/authSlice";
import Loader from "../shared/Loader";
import { useLocation, useNavigate } from "react-router-dom";

export default function AuthProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  const refreshAuth = async () => {
    try {
      setIsLoading(true);
      const res = await api.post("/Auth/refresh-token");

      const data = res.data;

      if (res.status === 200) {
        dispatch(login(data));

        if (location.pathname === "/login") {
          navigate("/profile");
        }
      } else {
        throw new Error(data?.message || "global error");
      }
    } catch (error) {
      if (location.pathname.includes("/profile")) {
        navigate("/");
      }
    } finally {
      setTimeout(() => {
        document.querySelector("span.auth-loader")?.classList.add("opacity-0");

        setTimeout(() => {
          setIsLoading(false);
        }, 600);
      }, 200);
    }
  };

  useEffect(() => {
    refreshAuth();
  }, []);

  return (
    <>
      {isLoading && (
        <span className="auth-loader h-screen w-screen fixed left-0 top-0 z-[99999] bg-slate-100 grid place-items-center transition-opacity ease-out duration-500">
          <Loader />
        </span>
      )}
      {children}
    </>
  );
}
