import i18n from "i18next";
import { initReactI18next } from "react-i18next";

const resources = {
  en: {
    translation: {
      "Welcome to FCAI platform": "Welcome to FCAI platform",
      "Select Language": "Select Language",
      "Login to the platform": "Login to the platform",
      Login: "Login",
      Username: "Username",
      Password: "Password",
      "Return to homepage": "Return to homepage",
      "Please fill in all fields": "Please fill in all fields",
      "global error": "Some error occurred",
      "The username is wrong": "The username is wrong",
      "The password is wrong": "The password is wrong",
      "Welcome back!": "Welcome back!",
      "Go to profile": "Go to profile",
      "The session is expired, signin again":
        "The session is expired, signin again",
      // Nav items
      Home: "Home",
      Doctors: "Doctors",
      Courses: "Courses",
      Students: "Students",
      Settings: "Settings",
      // Profile nav items
      "My Profile": "My Profile",
      "Change Password": "Change Password",
      "My Courses": "My Courses",
      Logout: "Logout",
    },
  },
  ar: {
    translation: {
      "Welcome to FCAI platform": "أهلا بك في منصة كلية حاسبات ومعلومات",
      "Select Language": "اختر اللغة",
      "Login to the platform": "تسجيل الدخول إلى المنصة",
      Login: "تسجيل الدخول",
      Username: "اسم المستخدم",
      Password: "كلمة المرور",
      "Return to homepage": "العودة إلى الصفحة الرئيسية",
      "Please fill in all fields": "يرجى ملئ جميع الحقول",
      "global error": "حدث خطأ ما",
      "The username is wrong": "اسم المستخدم خاطئ",
      "The password is wrong": "كلمة المرور خاطئة",
      "Welcome back!": "مرحبا بعودتك!",
      "Go to profile": "الذهاب إلى الصفحة الشخصية",
      "The session is expired, signin again":
        "انتهت الجلسة، قم بتسجيل الدخول مرة أخرى",
      // Nav items
      Home: "الرئيسية",
      Doctors: "هيئة التدريس",
      Courses: "الدورات",
      Students: "الطلاب",
      Settings: "الإعدادات",
      // Profile nav items
      "My Profile": "صفحتي",
      "Change Password": "تغيير كلمة المرور",
      "My Courses": "دوراتي",
      Logout: "تسجيل الخروج",
    },
  },
};

i18n.use(initReactI18next).init({
  resources,
  lng: "en",

  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
