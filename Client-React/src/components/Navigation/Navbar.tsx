import {
    Navbar,
    Typography,
    Button,
    IconButton, Collapse, Avatar, Menu, MenuHandler, MenuList, MenuItem,
} from "@material-tailwind/react";
import {useEffect, useState} from "react";
import {useAppDispatch, useAppSelector} from "@/app/hooks/reduxHooks.ts";
import {switchDarkMode} from "@/app/feutures/preferences/preferencesSlice.ts";
import AppLink from "components/Navigation/AppLink.tsx";

export function NavbarDefault() {
    const [openNav, setOpenNav] = useState(false);

    const isDarkModeActive = useAppSelector(s => s.userPreference.isDarkModeActive)
    const {token, profileImage, roles, id} = useAppSelector(s => s.auth)
    const dispatcher = useAppDispatch()

    const type = roles ? (roles.some(r => r.toLowerCase() == 'doctor' || r.toLowerCase() == 'assistant') ? 'doctor' : 'student') : ''

    useEffect(() => {
        window.addEventListener(
            "resize",
            () => window.innerWidth >= 960 && setOpenNav(false),
        );
    }, []);

    const switchDarkOrLightMode = () => dispatcher(switchDarkMode())

    const navList = (
        <ul className="mt-2 mb-4 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
            <Typography
                as="li"
                variant="small"
                className="flex items-center gap-x-2 p-1 font-medium"
                onClick={switchDarkOrLightMode}
            >
                {isDarkModeActive ? 'Dark' : 'Light'} Mode
            </Typography>
            <AppLink to={'/courses'} className="flex items-center">
                <Typography
                    as="li"
                    variant="small"
                    className="flex items-center gap-x-2 p-1 font-medium"
                >
                    <svg
                        width="16"
                        height="15"
                        viewBox="0 0 16 15"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M5 0.5C4.73478 0.5 4.48043 0.605357 4.29289 0.792893C4.10536 0.98043 4 1.23478 4 1.5C4 1.76522 4.10536 2.01957 4.29289 2.20711C4.48043 2.39464 4.73478 2.5 5 2.5H11C11.2652 2.5 11.5196 2.39464 11.7071 2.20711C11.8946 2.01957 12 1.76522 12 1.5C12 1.23478 11.8946 0.98043 11.7071 0.792893C11.5196 0.605357 11.2652 0.5 11 0.5H5ZM2 4.5C2 4.23478 2.10536 3.98043 2.29289 3.79289C2.48043 3.60536 2.73478 3.5 3 3.5H13C13.2652 3.5 13.5196 3.60536 13.7071 3.79289C13.8946 3.98043 14 4.23478 14 4.5C14 4.76522 13.8946 5.01957 13.7071 5.20711C13.5196 5.39464 13.2652 5.5 13 5.5H3C2.73478 5.5 2.48043 5.39464 2.29289 5.20711C2.10536 5.01957 2 4.76522 2 4.5ZM0 8.5C0 7.96957 0.210714 7.46086 0.585786 7.08579C0.960859 6.71071 1.46957 6.5 2 6.5H14C14.5304 6.5 15.0391 6.71071 15.4142 7.08579C15.7893 7.46086 16 7.96957 16 8.5V12.5C16 13.0304 15.7893 13.5391 15.4142 13.9142C15.0391 14.2893 14.5304 14.5 14 14.5H2C1.46957 14.5 0.960859 14.2893 0.585786 13.9142C0.210714 13.5391 0 13.0304 0 12.5V8.5Z"
                            fill="#90A4AE"
                        />
                    </svg>

                    Courses
                </Typography>
            </AppLink>
            {token && <Menu
                as="li"
                variant="small"
                className="flex items-center gap-x-2 p-1 font-medium cursor-pointer"
            >
                <MenuHandler>
                    <Avatar
                        src={profileImage ? ("http://localhost:5196/" + profileImage) : ""} size={'sm'}
                        className={'cursor-pointer'}
                    />
                </MenuHandler>
                <MenuList>
                    <AppLink to={`/${type}/${id}/`}>
                        <MenuItem>Profile</MenuItem>
                    </AppLink>

                    <AppLink to={`/${type}/${id}/courses`}>
                        <MenuItem>Courses</MenuItem>
                    </AppLink>

                    <AppLink to={`/${type}/${id}/settings`}>
                        <MenuItem>Settings</MenuItem>
                    </AppLink>

                </MenuList>
            </Menu>}
        </ul>
    );

    return (
        <Navbar
            className="mx-auto max-w-screen-xl px-4 py-2 lg:px-8 lg:py-4 dark:bg-gray-700 dark:text-white text-black">
            <div className="container mx-auto flex items-center justify-between text-blue-gray-900 dark:text-white">
                <Typography
                    as="a"
                    href="#"
                    className="mr-4 cursor-pointer py-1.5 font-medium"
                >
                    <AppLink to={'/'}>Kollity</AppLink>
                </Typography>
                <div className="hidden lg:block">{navList}</div>
                {token == undefined && <div className="flex items-center gap-x-1">
                    <AppLink to={'/login'}>
                        <Button size="sm" className="hidden lg:inline-block">
                            Log In
                        </Button>
                    </AppLink>
                </div>}
                <IconButton
                    variant="text"
                    className="ml-auto h-6 w-6 text-inherit hover:bg-transparent focus:bg-transparent active:bg-transparent lg:hidden"
                    ripple={false}
                    onClick={() => setOpenNav(!openNav)}
                >
                    {openNav ? (
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            className="h-6 w-6"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                            strokeWidth={2}
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M6 18L18 6M6 6l12 12"
                            />
                        </svg>
                    ) : (
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            className="h-6 w-6"
                            fill="none"
                            stroke="currentColor"
                            strokeWidth={2}
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M4 6h16M4 12h16M4 18h16"
                            />
                        </svg>
                    )}
                </IconButton>
            </div>
            <Collapse open={openNav}>
                <div className="container mx-auto">
                    {navList}
                    {token == undefined && <div className="flex items-center gap-x-1">
                        <AppLink to={'/login'}>
                            <Button fullWidth size="sm" className="">
                                <span>Log In</span>
                            </Button>
                        </AppLink>
                    </div>}
                </div>
            </Collapse>
        </Navbar>
    );
}

export default NavbarDefault;