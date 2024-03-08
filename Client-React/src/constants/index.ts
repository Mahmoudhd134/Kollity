// Nav items
import { FaHome } from "react-icons/fa";
import { PiBooksFill } from "react-icons/pi";
import { FaChalkboardTeacher } from "react-icons/fa";
import { PiStudentFill } from "react-icons/pi";
import { IoSettings } from "react-icons/io5";

export const navItems = [
  {
    title: "Home",
    path: "/profile",
    icon: FaHome,
  },
  {
    title: "Courses",
    path: "/profile/courses",
    icon: PiBooksFill,
  },
  {
    title: "Doctors",
    path: "/profile/doctors",
    icon: FaChalkboardTeacher,
  },
  {
    title: "Students",
    path: "/profile/students",
    icon: PiStudentFill,
  },
  {
    title: "Settings",
    path: "/profile/settings",
    icon: IoSettings,
  },
];

// Profile nav items
import { IoIosInformationCircleOutline } from "react-icons/io";
import { IoIosUnlock } from "react-icons/io";
import { MdOutlineLibraryBooks } from "react-icons/md";

export const profileNavItems = [
  {
    title: "My Profile",
    path: "/profile",
    icon: IoIosInformationCircleOutline,
  },
  {
    title: "Change Password",
    path: "/profile/change-password",
    icon: IoIosUnlock,
  },
  {
    title: "My Courses",
    path: "/profile/my-courses",
    icon: MdOutlineLibraryBooks,
  },
];
