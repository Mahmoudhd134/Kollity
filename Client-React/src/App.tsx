import { Routes, Route } from "react-router-dom";
import WelcomePage from "./pages/WelcomePage";
import LoginPage from "./pages/LoginPage";
import { Toaster } from "react-hot-toast";
import ProfilePage from "./pages/ProfilePage";
import CoursesPage from "./pages/CoursesPage";

function App() {
  return (
    <main>
      <Routes>
        <Route path="/" element={<WelcomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="profile" element={<ProfilePage />}>
          <Route path="courses" element={<CoursesPage />} />
        </Route>
      </Routes>
      <Toaster />
    </main>
  );
}

export default App;
