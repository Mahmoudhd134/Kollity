import { selectAuth } from "../../features/slices/authSlice";
import { useTranslation } from "react-i18next";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../features/slices/authSlice";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { MouseEvent } from "react";
import { navItems, profileNavItems } from "../../constants";
import { Button } from "../ui/button";
import { IoIosArrowDown } from "react-icons/io";
import { Avatar, AvatarImage } from "../../components/ui/avatar";
import { FiLogOut } from "react-icons/fi";

export default function NavItems() {
  const { t } = useTranslation();
  const location = useLocation();
  const dispatch = useDispatch();
  const authData = useSelector(selectAuth);
  const navigate = useNavigate();

  // Show extra menu on profile click
  const handleProfileClick = (e: MouseEvent<HTMLButtonElement>) => {
    const { currentTarget: target } = e;
    const arrow = target.querySelector(".arrow") as HTMLElement;
    const extraMenu = document.querySelectorAll(".extra-menu");

    if (target.getAttribute("aria-expanded") === "true") {
      target.setAttribute("aria-expanded", "false");
      arrow.classList.remove("rotate-180");

      // "translateX(-800px)"
      extraMenu.forEach((menu) => {
        (menu as HTMLElement).style.transform = "translateX(-800px)";
      });
    } else {
      target.setAttribute("aria-expanded", "true");
      arrow.classList.add("rotate-180");

      extraMenu.forEach((menu) => {
        (menu as HTMLElement).style.transform = "translateX(0px)";
      });
    }
  };

  // Handle logout
  const handleLogout = () => {
    dispatch(logout());
    navigate("/");
  };

  return (
    <nav className="h-screen fixed top-0 ltr:left-0 rtl:right-0 w-[270px] p-2 overflow-hidden bg-white shadow-xl border-x">
      <div className="relative">
        <Button
          className="flex w-full !py-7 justify-between mb-2 bg-gradient-to-br from-primary-400 to-primary-300 opacity-95 transition-opacity hover:opacity-100"
          dir="ltr"
          onClick={handleProfileClick}
        >
          <Avatar className="bg-slate-200">
            <AvatarImage src={authData?.profileImage} />
          </Avatar>

          <span className="block text-center">
            @
            {authData?.userName?.length > 14
              ? authData?.userName?.slice(0, 15) + "..."
              : authData?.userName}
          </span>

          <IoIosArrowDown size={20} className="arrow transition-transform" />
        </Button>

        <div className="absolute h-screen w-full bg-white transition-transform duration-700 ease-in-out translate-x-[800px] extra-menu">
          <ul className="flex flex-col gap-2">
            {profileNavItems.map((item, index) => (
              <li key={index}>
                <Button
                  variant={
                    location.pathname === item.path ? "secondary" : "ghost"
                  }
                  asChild
                  className={`w-full flex justify-start gap-2 py-6 text-[16px] ${
                    location.pathname === item.path && "text-primary-200"
                  }`}
                >
                  <Link to={item.path}>
                    {<item.icon size={18} />} {t(item.title)}
                  </Link>
                </Button>
              </li>
            ))}
            <i>
              <Button
                variant={"destructive"}
                className="w-full flex justify-start gap-2 py-6 text-[16px]"
                onClick={handleLogout}
              >
                <FiLogOut size={18} />
                {t("Logout")}
              </Button>
            </i>
          </ul>
        </div>
      </div>

      <ul className="flex flex-col gap-2">
        {navItems.map((item, index) => (
          <li key={index}>
            <Button
              variant={location.pathname === item.path ? "secondary" : "ghost"}
              asChild
              className={`w-full flex justify-start gap-2 py-6 text-[16px] ${
                location.pathname === item.path && "text-primary-200"
              }`}
            >
              <Link to={item.path}>
                {<item.icon size={18} />} {t(item.title)}
              </Link>
            </Button>
          </li>
        ))}
      </ul>
    </nav>
  );
}
