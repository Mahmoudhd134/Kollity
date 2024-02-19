import api from "@/api/api";
import { ReactNode, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Loader2 } from "lucide-react";
import { IAuth, signIn } from "@/redux/authSlice";
import { useLocation, useNavigate } from "react-router-dom";
import { protectedRoutes } from "@/constants";

export default function AuthProvider({ children }: { children: ReactNode }) {
  const [isLoading, setIsLoading] = useState(true);
  const [readyToRemoveLoader, setReadyToRemoveLoader] = useState(false);
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();

  const checkAuth = async () => {
    let counter: NodeJS.Timeout;
    const isProtectedRoute = protectedRoutes.includes(location.pathname);

    console.log("isProtectedRoute", location.pathname);

    try {
      setIsLoading(true);
      const res = await api.post("/Auth/refresh-token");
      const data: IAuth = res.data;

      if (data?.token) {
        if (location.pathname === "/sign-in") {
          navigate("/profile");
        }

        dispatch(signIn(data));
      } else {
        throw new Error("No token");
      }
    } catch (error) {
      if (isProtectedRoute) {
        navigate("/");
      }
    } finally {
      setIsLoading(false);
      counter = setTimeout(() => {
        setReadyToRemoveLoader(true);
      }, 300);
    }

    return () => {
      clearTimeout(counter);
    };
  };

  useEffect(() => {
    checkAuth();
  }, []);

  let loader = readyToRemoveLoader ? null : (
    <div
      className={`fixed left-0 top-0 h-screen w-screen grid place-items-center transition-opacity duration-300 bg-slate-100 z-50 ${
        isLoading ? "opacity-100" : "opacity-0"
      }`}
    >
      <Loader2 className="animate-spin" size={40} />
    </div>
  );

  return (
    <>
      {loader}
      {children}
    </>
  );
}
