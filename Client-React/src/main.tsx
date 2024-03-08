import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import "./i18n.ts";
import { BrowserRouter } from "react-router-dom";
import LanguageProvider from "./components/providers/LanguageProvider.tsx";
import { Provider } from "react-redux";
import { store } from "./features/store/store.ts";
import AuthProvider from "./components/providers/AuthProvider.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <>
    <Provider store={store}>
      <BrowserRouter>
        <LanguageProvider>
          <AuthProvider>
            <App />
          </AuthProvider>
        </LanguageProvider>
      </BrowserRouter>
    </Provider>
  </>
);
