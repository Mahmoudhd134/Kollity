import authSlice, { IAuth } from "@/redux/authSlice";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { useOutletContext } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Upload } from "lucide-react";

export default function ProfileDataPage() {
  const passedData: { authData: IAuth } = useOutletContext();
  const authData = passedData.authData;

  const parentDivClasses = "bg-slate-50 rounded-md p-8";
  const headerClasses = "text-slate-600 text-base font-semibold";
  const paragraphClasses = "text-lg font-bold";

  return (
    <div>
      <h3 className="profile-page-h3-title">بيانات المستخدم</h3>
      <div className="flex gap-4">
        <div className={"flex flex-col gap-4 flex-1 " + parentDivClasses}>
          <div>
            <h4 className={headerClasses}>الاسم: </h4>
            <p className={paragraphClasses}>
              {/**fullNameInArabic */} مصطفى أسامة محمد
            </p>
          </div>
          <div>
            <h4 className={headerClasses}>اسم المستخدم: </h4>
            <p className={paragraphClasses}>@{authData.userName}</p>
          </div>
          <div>
            <h4 className={headerClasses}>الكود: </h4>
            <p>200375</p>
          </div>
          <div>
            <h4 className={headerClasses}>البريد الالكتروني</h4>
            <p className={paragraphClasses}>{authData.email}</p>
          </div>
          <div>
            <h4 className={headerClasses}>المستوى: </h4>
            <p className={paragraphClasses}>Grade 4</p>
          </div>
          <div>
            <h4 className={headerClasses}>القسم: </h4>
            <p className={paragraphClasses}>CS</p>
          </div>
        </div>
        <form
          method=""
          className={"flex flex-col gap-6 items-center " + parentDivClasses}
        >
          <Avatar className="w-[150px] h-[150px]">
            <AvatarImage src="https://github.com/shadcn.png" />
            <AvatarFallback className="w-full h-full bg-slate-100"></AvatarFallback>
          </Avatar>
          <h4>@{authData.userName}</h4>

          <Button variant={"outline"}>
            <Upload /> تغيير صورة الملف
          </Button>
        </form>
      </div>
    </div>
  );
}
