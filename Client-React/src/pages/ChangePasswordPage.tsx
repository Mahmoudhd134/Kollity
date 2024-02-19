import { IAuth } from "@/redux/authSlice";
import { useOutletContext } from "react-router-dom";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useRef } from "react";
import { useToast } from "@/components/ui/use-toast";
import api from "@/api/api";

export default function ChangePasswordPage() {
  const oldPasswordRef = useRef<HTMLInputElement>(null);
  const newPasswordRef = useRef<HTMLInputElement>(null);
  const confirmNewPasswordRef = useRef<HTMLInputElement>(null);
  const passedData: { authData: IAuth } = useOutletContext();
  const { toast } = useToast();

  const handleChangePassword = async () => {
    const oldPassword = oldPasswordRef.current?.value;
    const newPassword = newPasswordRef.current?.value;
    const confirmNewPassword = confirmNewPasswordRef.current?.value;

    if (!oldPassword || !newPassword || !confirmNewPassword) {
      toast({
        title: "خطأ",
        description: "الرجاء ملء جميع الحقول",
        variant: "destructive",
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
      const res = await api.post("/Identity/change-password", {
        oldPassword,
        newPassword,
      });

      const data = res.data;

      console.log(data);
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
