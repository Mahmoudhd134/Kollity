import { LANGUAGE_COOKIE } from "../../constants/cookies";
import React, { useEffect } from "react";
import { useCookies } from "react-cookie";
import { useTranslation } from "react-i18next";

export default function LanguageProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [cookies] = useCookies([LANGUAGE_COOKIE]);
  const { i18n } = useTranslation();

  useEffect(() => {
    const cookieLang = cookies[LANGUAGE_COOKIE];

    if (cookieLang === "ar") {
      document.body.dir = "rtl";

      i18n.changeLanguage(cookieLang);
    } else i18n.changeLanguage("en");
  }, []);

  return children;
}
