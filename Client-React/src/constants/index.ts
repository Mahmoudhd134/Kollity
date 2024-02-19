import { UserCog, LockKeyhole, Layers, BarChart3 } from "lucide-react";

export const protectedRoutes = [
  "/profile",
  "/profile/profile-data",
  "/profile/change-password",
];

export const navItems = [
  {
    title: "بيانات المستخدم",
    to: "/profile/profile-data",
    icon: UserCog,
  },
  {
    title: "تغيير الباسورد",
    to: "/profile/change-password",
    icon: LockKeyhole,
  },
  {
    title: "الكورسات",
    to: "/profile/courses",
    icon: Layers,
  },
  {
    title: "أحصائيات",
    to: "/profile/statistics",
    icon: BarChart3,
  },
];

export type Role = "Assistant" | "Doctor" | "Student";
export const roles: Role[] = ["Assistant", "Doctor", "Student"];
