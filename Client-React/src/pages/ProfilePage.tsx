import { Button } from "@/components/ui/button";
import { navItems } from "@/constants";
import { selectAuth } from "@/redux/authSlice";
import { useDispatch, useSelector } from "react-redux";
import { Link, Outlet, useLocation, useNavigate } from "react-router-dom";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Skeleton } from "@/components/ui/skeleton";
import blob from "@/assets/svg/blob_1.svg";
import { useEffect, MouseEvent } from "react";
import { LogOut } from "lucide-react";
import { logOut } from "@/redux/authSlice";
import api from "@/api/api";

export default function ProfilePage() {
  const authData = useSelector(selectAuth);
  const location = useLocation();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const pathname = location.pathname;

  useEffect(() => {
    if (location.pathname === "/profile") {
      navigate("/profile/profile-data");
    }
  }, []);
  let usernameAndAvatar =
    authData.profileImage && authData.userName ? (
      <>
        <Avatar className="w-[150px] h-[150px]">
          <AvatarImage src="https://github.com/shadcn.png" />
          <AvatarFallback className="w-full h-full bg-slate-100"></AvatarFallback>
        </Avatar>
        <p className="text-sm">{authData.userName}</p>
      </>
    ) : (
      <>
        <Skeleton className="w-[150px] h-[150px] rounded-full" />
        <Skeleton className="w-20 h-5" />
      </>
    );

  const handleLogOut = async (e: MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    const overlay = document.getElementById("overlay");

    overlay?.classList.add("active");

    try {
      await api.delete("/Auth/logout");
    } catch (error) {
      dispatch(logOut());
    } finally {
      navigate("/");
      document.getElementById("overlay")?.classList.remove("active");
    }
  };

  return (
    <div className="flex flex-row">
      <aside className="w-[300px] h-screen border-l bg-zinc-800 text-white overflow-hidden relative">
        <nav className="p-3">
          <div className="flex flex-col items-center gap-3 mb-8">
            {usernameAndAvatar}
          </div>

          <ul>
            {navItems.map((item, index) => (
              <li key={index} className="mb-2">
                <Link to={item.to} className="block w-full">
                  <Button
                    variant={`${pathname === item.to ? "secondary" : "ghost"}`}
                    className="w-full flex justify-start gap-3"
                  >
                    {<item.icon />} {item.title}
                  </Button>
                </Link>
              </li>
            ))}

            <li>
              <Button
                className="w-full flex justify-start gap-3"
                variant={"destructive"}
                onClick={handleLogOut}
              >
                <LogOut /> تسجيل الخروج
              </Button>
            </li>
          </ul>
        </nav>
        <div className="z-10 bottom-0 left-12 opacity-5 w-[350px] h-[350px]">
          <img src={blob} alt="Blob" />
        </div>
      </aside>
      <section className="px-6 py-8 w-full">
        <Outlet context={{ authData }} />
      </section>
    </div>
  );
}
