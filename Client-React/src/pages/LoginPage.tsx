import { Button } from "../components/ui/button";
import { Input } from "../components/ui/input";
import { useTranslation } from "react-i18next";
import { MdOutlineLogin } from "react-icons/md";
import { IoIosReturnLeft } from "react-icons/io";
import blobSvgSrc1 from "../assets/svg/blob_1.svg";
import blobSvgSrc2 from "../assets/svg/blob_2.svg";
import toast from "react-hot-toast";
import { useDispatch } from "react-redux";

import logoImgSrc from "../assets/images/logo.png";
import { Link, useNavigate } from "react-router-dom";
import { ChangeEvent, FormEvent, useRef } from "react";
import api from "../features/api/api";
import ApiError from "../lib/ApiError";
import { IAuth, login } from "../features/slices/authSlice";

export default function LoginPage() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const usernameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);
  const defaultWidth = "w-[300px]";

  // Handle login
  const handleLogin = async (e: FormEvent) => {
    e.preventDefault();

    const username = usernameRef.current?.value;
    const password = passwordRef.current?.value;

    if (!username || !password) {
      return toast.error(t("Please fill in all fields"));
    }

    try {
      const res = await api.post("/Auth/login", { username, password });
      const data = res.data;

      if (res.status === 200) {
        let responseData = data as IAuth;

        dispatch(login(responseData));

        navigate("/profile");
      } else {
        throw new Error(data?.message || t("global error"));
      }
    } catch (error: any) {
      toast.error(t(ApiError.generateError(error)));
    }
  };

  // Handle text direction
  const handleTextDir = (e: ChangeEvent<HTMLInputElement>) => {
    const { currentTarget } = e;

    if (currentTarget.value.length > 0) {
      currentTarget.setAttribute("dir", "ltr");
    } else {
      currentTarget.removeAttribute("dir");
    }
  };

  // Render
  return (
    <section className="login-section grid place-content-center gap-4 relative">
      <div className="flex flex-col items-center gap-3 mb-3">
        <img src={logoImgSrc} alt="FCAI Logo" className="w-36 h-36 mx-auto" />
        <h1 className="text-2xl">{t("Login to the platform")}</h1>
      </div>

      <form
        action=""
        className={"flex flex-col gap-4 " + defaultWidth}
        onSubmit={handleLogin}
      >
        <Input
          placeholder={t("Username")}
          ref={usernameRef}
          onChange={handleTextDir}
        />
        <Input
          placeholder={t("Password")}
          ref={passwordRef}
          onChange={handleTextDir}
          type="password"
        />

        <Button
          className={"btn-primary text-lg flex items-center gap-3 "}
          type="submit"
        >
          <MdOutlineLogin />
          {t("Login")}
        </Button>

        <Button
          className="text-lg flex items-center gap-3 text-primary-100 hover:text-primary-200"
          variant={"secondary"}
          asChild
        >
          <Link to={"/"}>
            <IoIosReturnLeft />
            {t("Return to homepage")}
          </Link>
        </Button>
      </form>

      <img
        src={blobSvgSrc1}
        alt="Blob 1"
        className="absolute top-0 right-0 w-1/4 opacity-30"
      />
      <img
        src={blobSvgSrc2}
        alt="Blob 2"
        className="absolute bottom-0 left-0 w-1/4 opacity-60"
      />
    </section>
  );
}
