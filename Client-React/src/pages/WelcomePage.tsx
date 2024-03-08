import universityImgSrc from "../assets/images/university.jpg";
import waveSvgSrc from "../assets/svg/wave.svg";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../components/ui/select";
import { useTranslation } from "react-i18next";
import logoImgSrc from "../assets/images/logo.png";
import { MdOutlineLogin } from "react-icons/md";

import { useCookies } from "react-cookie";
import { LANGUAGE_COOKIE } from "../constants/cookies";
import { Button } from "../components/ui/button";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectAuth } from "../features/slices/authSlice";
import { FaUserAlt } from "react-icons/fa";

export default function WelcomePage() {
  const { t, i18n } = useTranslation();
  const [_, setCookie] = useCookies([LANGUAGE_COOKIE]);
  const authData = useSelector(selectAuth);

  const changeLanguage = (lang: string) => {
    setCookie(LANGUAGE_COOKIE, lang, { path: "/" });
    location.reload();
  };

  let content;

  if (authData.isAuth) {
    content = (
      <Button
        className="flex items-center gap-3 mx-auto text-lg btn-primary"
        asChild
      >
        <Link to="/profile">
          <FaUserAlt /> {t("Go to profile")}
        </Link>
      </Button>
    );
  } else {
    content = (
      <Button
        className="flex items-center gap-3 mx-auto text-lg btn-primary"
        asChild
      >
        <Link to="login">
          <MdOutlineLogin /> {t("Login")}
        </Link>
      </Button>
    );
  }

  return (
    <>
      <section className="welcome-section overflow-hidden relative grid place-items-center">
        <div className="absolute top-0 w-full z-30 p-5">
          <Select
            onValueChange={(value) => {
              i18n.changeLanguage(value);
              changeLanguage(value);
            }}
          >
            <SelectTrigger className="w-[180px] welcome-language-change">
              <SelectValue placeholder={t("Select Language")} />
            </SelectTrigger>
            <SelectContent>
              <SelectGroup>
                <SelectItem value="ar" className="select-item">
                  العربية
                </SelectItem>
                <SelectItem value="en" className="select-item">
                  English
                </SelectItem>
              </SelectGroup>
            </SelectContent>
          </Select>
        </div>

        <div className="flex flex-col gap-5">
          <img
            src={logoImgSrc}
            alt="FCAI logo"
            className="w-40 h-40 mx-auto mt-10"
          />
          <h1 className="text-center text-4xl font-bold text-white mt-5">
            {authData.isAuth
              ? t("Welcome back!")
              : t("Welcome to FCAI platform")}
          </h1>

          {content}
        </div>

        <img
          src={universityImgSrc}
          alt="university image"
          className="w-full h-full object-cover brightness-50 absolute left-0 top-0 -z-10"
        />

        <img
          src={waveSvgSrc}
          alt="Wave svg"
          className="w-full h-fit absolute -bottom-0 left-0 object-cover -z-10 opacity-45"
        />
      </section>
    </>
  );
}
