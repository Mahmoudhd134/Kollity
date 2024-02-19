import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import blobSrc_1 from "@/assets/svg/blob_1.svg";
import blobSrc_2 from "@/assets/svg/blob_2.svg";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import api from "@/api/api";
import { useDispatch } from "react-redux";
import { IAuth, signIn } from "@/redux/authSlice";
import Logo from "@/components/ui/Logo";
import { useToast } from "@/components/ui/use-toast";

export default function SignInPage() {
  const usernameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { toast } = useToast();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const username = usernameRef.current?.value;
    const password = passwordRef.current?.value;

    if (!username || !password) {
      toast({
        description: "الرجاء ملء جميع الحقول",
      });
      return;
    }

    try {
      const res = await api.post("/Auth/login", {
        UserName: username,
        Password: password,
      });

      const data: IAuth = res.data;

      console.log(data);
    } catch (error) {
      toast({
        title: "خطأ",
        description: "حدث خطأ أثناء تسجيل الدخول",
        variant: "destructive",
      });
    }

    // navigate("/profile");
  };

  return (
    <section className="grid place-content-center w-full h-screen relative">
      <div className="absolute -top-10 md:-top-32 -left-52  z-[-1] hidden md:block">
        <img src={blobSrc_1} className="sm:300px md:w-[400px]" />
      </div>

      <div className="absolute -bottom-36 -right-36 z-[-1] hidden md:block">
        <img src={blobSrc_2} className="w-[300px]" />
      </div>

      <form
        className="h-full flex flex-col items-center justify-center gap-5 w-[350px] max-w-full"
        onSubmit={handleSubmit}
      >
        <Logo styles="w-[120px]" />

        <h1 className="text-center w-full mb-20">تسجيل الدخول</h1>
        <Input
          type="text"
          placeholder="اسم المستخدم"
          className="w-full"
          ref={usernameRef}
        />
        <Input
          type="password"
          placeholder="الرقم السري"
          className="w-full"
          ref={passwordRef}
        />
        <Button className="w-full">تسجيل الدخول</Button>
      </form>
    </section>
  );
}
