import { Button } from "../components/ui/button";
import NavItems from "../components/shared/NavItems";
import { Sheet, SheetContent, SheetTrigger } from "../components/ui/sheet";
import { RxHamburgerMenu } from "react-icons/rx";
import { Outlet, useNavigate } from "react-router-dom";
import { selectAuth } from "../features/slices/authSlice";
import { useSelector } from "react-redux";
import { useEffect } from "react";

export default function ProfilePage() {
  const authData = useSelector(selectAuth);
  const navigate = useNavigate();

  useEffect(() => {
    if (!authData.isAuth) navigate("/login");
  }, [authData]);

  return (
    <div className="grid lg:grid-cols-[270px_1fr] h-[300vh]">
      <div className="py-2 px-5 bg-white border-b fixed w-full block lg:hidden">
        <Sheet>
          <SheetTrigger asChild>
            <Button variant={"secondary"}>
              <RxHamburgerMenu />
            </Button>
          </SheetTrigger>
          <SheetContent className="w-fit block lg:hidden">
            <NavItems />
          </SheetContent>
        </Sheet>
      </div>

      <aside className="relative hidden lg:block">
        <NavItems />
      </aside>
      <section className="h-screen">
        <Outlet />
      </section>
    </div>
  );
}
