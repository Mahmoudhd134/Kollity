import {Route, Routes, useLocation, useNavigate} from "react-router-dom";
import RootLayout from "./components/Layout/RootLayout.tsx";
import {useAppDispatch} from "./app/hooks/reduxHooks.ts";
import {useEffect} from "react";
import useRefreshToken from "./app/hooks/userRefreshToken.ts";
import {baseApi} from "./app/api/baseApi.ts";
import {logout, setCredentials} from "./app/feutures/auth/authSlice.ts";

function AppRoutes() {
    const stayLogin = JSON.parse(localStorage.getItem('stayLogin') ?? 'false')
    const dispatch = useAppDispatch()
    const loc = useLocation()
    const navigator = useNavigate()

    useEffect(() => {
        if (stayLogin) {
            (async () => {
                const refresh = useRefreshToken()
                const data = await refresh()
                dispatch(baseApi.util?.resetApiState())
                if (data) {
                    dispatch(setCredentials(data))
                    navigator(loc.pathname)
                } else {
                    dispatch(logout())
                }
            })()
        }
    }, [])

    return <Routes>
        <Route path={'/'} element={<RootLayout/>}>
            <Route index element={<h3>Home</h3>}/>
            <Route path={'login'} element={<h3>Login page</h3>}/>
        </Route>
    </Routes>

}

export default AppRoutes