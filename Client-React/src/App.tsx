import "./App.css";

import { Routes, Route } from "react-router-dom";
import ColorsPalletePage from "./pages/ColorsPalletePage";

function App() {
  return (
    <>
      <Routes>
        <Route path="colors" element={<ColorsPalletePage />} />
      </Routes>
    </>
  );
}

export default App;
