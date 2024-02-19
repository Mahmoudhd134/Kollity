import "./App.css";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import SignInPage from "./pages/SignInPage";
import AuthProvider from "./components/shared/AuthProvider";
import ProfilePage from "./pages/ProfilePage";
import { Toaster } from "@/components/ui/toaster";
import ProfileDataPage from "./pages/ProfileDataPage";
import ChangePasswordPage from "./pages/ChangePasswordPage";
import CoursesPage from "./pages/CoursesPage";
import StatsPage from "./pages/StatsPage";
import Overlay from "./components/shared/Overlay";

function App() {
  return (
    <>
      <main className="relative">
        <AuthProvider>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="sign-in" element={<SignInPage />} />
            <Route path="profile" element={<ProfilePage />}>
              <Route path="profile-data" element={<ProfileDataPage />} />
              <Route path="change-password" element={<ChangePasswordPage />} />
              <Route path="courses" element={<CoursesPage />} />
              <Route path="statistics" element={<StatsPage />} />
            </Route>
          </Routes>
        </AuthProvider>
        <Overlay />
      </main>
      <Toaster />
    </>
  );
}

export default App;
