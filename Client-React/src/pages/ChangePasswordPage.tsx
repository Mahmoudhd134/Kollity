import { IAuth, logOut } from "@/redux/authSlice";
import { useNavigate, useOutletContext } from "react-router-dom";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useRef } from "react";
import { useToast } from "@/components/ui/use-toast";
import api from "@/api/api";
import { useDispatch } from "react-redux";

export default function ChangePasswordPage() {
  const oldPasswordRef = useRef<HTMLInputElement>(null);
  const newPasswordRef = useRef<HTMLInputElement>(null);
  const confirmNewPasswordRef = useRef<HTMLInputElement>(null);
  const passedData: { authData: IAuth } = useOutletContext();
  const authData = passedData.authData;
  const { toast } = useToast();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleChangePassword = async () => {
    const oldPassword = oldPasswordRef.current?.value;
    const newPassword = newPasswordRef.current?.value;
    const confirmNewPassword = confirmNewPasswordRef.current?.value;

    if (!oldPassword || !newPassword || !confirmNewPassword) {
      toast({
        description: "الرجاء ملء جميع الحقول",
      });
      return;
    }

    if (newPassword !== confirmNewPassword) {
      toast({
        title: "خطأ",
        description: "الباسورد الجديد غير متطابق",
        variant: "destructive",
      });
      return;
    }

    try {
      if (authData.token === undefined) {
        dispatch(logOut());
        navigate("/");
        return toast({
          description: "الرجاء تسجيل الدخول",
        });
      }

      const res = await api.patch(
        "/Identity/change-password",
        {
          oldPassword,
          newPassword,
        },
        {
          headers: {
            Authorization: `Bearer ${authData.token}`,
          },
          withCredentials: true,
        }
      );

      if (res.status === 200) {
        toast({
          description: "تم تغيير الباسورد بنجاح",
        });

        navigate("/profile/profile-data");
      }
    } catch (error) {
      toast({
        title: "خطأ",
        description: "حدث خطأ أثناء تغيير الباسورد",
        variant: "destructive",
      });
    }
  };

  return (
    <div>
      <h3 className="profile-page-h3-title">تغيير الباسورد</h3>
      <div
        className="flex flex-col gap-5 w-[350px] mx-auto h-full justify-center"
        style={{
          height: "50vh",
        }}
      >
        <Input
          type="password"
          placeholder="الباسورد القديم"
          ref={oldPasswordRef}
        />
        <Input
          type="password"
          placeholder="الباسورد الجديد"
          ref={newPasswordRef}
        />
        <Input
          type="password"
          placeholder="تأكيد الباسورد الجديد"
          ref={confirmNewPasswordRef}
        />

        <Button onClick={handleChangePassword}>تغيير الباسورد</Button>
      </div>
    </div>
  );
}
