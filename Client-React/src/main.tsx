import React from 'react'
import ReactDOM from 'react-dom/client'
import AppRoutes from './AppRoutes.tsx'
import './index.css'
import {ThemeProvider} from "@material-tailwind/react";
import {Provider} from "react-redux";
import {store} from "./app/store.ts";
import {BrowserRouter, Route, Routes} from "react-router-dom";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Provider store={store}>
                <ThemeProvider>
                    <Routes>
                        <Route path={'/*'} element={<AppRoutes/>}/>
                    </Routes>
                </ThemeProvider>
            </Provider>
        </BrowserRouter>
    </React.StrictMode>,
)
