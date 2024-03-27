import Navbar from "../Navigation/Navbar.tsx";
import {Outlet} from "react-router-dom";
import {useState} from "react";
import {useAppSelector} from "@/app/hooks/reduxHooks.ts";

const RootLayout = () => {
    const isDarkModeActivated = useAppSelector(s => s.userPreference.isDarkModeActive)
    return (<div className={isDarkModeActivated ? 'dark' : ''}>
        <Navbar/>
        <Outlet/>
    </div>);
};

export default RootLayout;