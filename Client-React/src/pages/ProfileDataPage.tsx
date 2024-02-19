import { IAuth } from "@/redux/authSlice";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { useOutletContext } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Upload } from "lucide-react";
import {
  useLazyGetDoctorProfileDataQuery,
  useLazyGetStudentProfileDataQuery,
} from "@/redux/apiSlice";
import { useEffect, useState } from "react";
import { Skeleton } from "@/components/ui/skeleton";

export default function ProfileDataPage() {
  const [triggerStudent] = useLazyGetStudentProfileDataQuery();
  const [triggerDoctor] = useLazyGetDoctorProfileDataQuery();
  const passedData: { authData: IAuth } = useOutletContext();
  const [data, setData] = useState<any>({});
  const authData = passedData.authData;

  useEffect(() => {
    if (authData.id) {
      if (authData.roles.includes("Student")) {
        triggerStudent({ id: authData.id }).then((res) => {
          setData(res.data);
        });
      }
      if (
        authData.roles.includes("Doctor") ||
        authData.roles.includes("Assistant")
      ) {
        triggerDoctor({ id: authData.id }).then((res) => {
          setData(res.data);
          console.log(res.data);
        });
      }
    }
  }, [authData.id]);

  const parentDivClasses = "border border-slate-200 rounded-md p-8";
  const headerClasses = "text-slate-600 text-base font-semibold";
  const paragraphClasses = "text-lg font-bold flex flex-col gap-1";

  return (
    <div>
      <h3 className="profile-page-h3-title">بيانات المستخدم</h3>
      <div className="flex gap-4">
        <div
          className={
            "flex flex-col justify-center gap-4 flex-1 " + parentDivClasses
          }
        >
          <div>
            <h4 className={headerClasses}>الاسم: </h4>
            <>
              {data.fullNameInArabic ? (
                <p className={paragraphClasses}>{data.fullNameInArabic}</p>
              ) : (
                <Skeleton
                  className={"h-[15px] w-[150px] " + paragraphClasses}
                />
              )}
            </>
          </div>
          <div>
            <h4 className={headerClasses}>اسم المستخدم: </h4>
            <>
              {authData.userName ? (
                <p className={paragraphClasses}>@{authData.userName}</p>
              ) : (
                <Skeleton
                  className={"h-[15px] w-[130px] " + paragraphClasses}
                />
              )}
            </>
          </div>
          {/* <div>
            <h4 className={headerClasses}>الكود: </h4>
            <p>200375</p>
          </div> */}
          <div>
            <h4 className={headerClasses}>البريد الالكتروني</h4>
            <>
              {authData.email ? (
                <p className={paragraphClasses}>{authData.email}</p>
              ) : (
                <Skeleton
                  className={"h-[15px] w-[180px] " + paragraphClasses}
                />
              )}
            </>
          </div>
          {/* <div>
            <h4 className={headerClasses}>المستوى: </h4>
            <p className={paragraphClasses}>Grade 4</p>
          </div>
          <div>
            <h4 className={headerClasses}>القسم: </h4>
            <p className={paragraphClasses}>CS</p>
          </div> */}
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

          <Button variant={"outline"} className="flex items-center gap-2">
            <Upload size={20} /> تغيير صورة الملف
          </Button>
        </form>
      </div>
    </div>
  );
}
