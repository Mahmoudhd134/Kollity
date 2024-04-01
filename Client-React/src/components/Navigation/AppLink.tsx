import {Link, LinkProps, useLocation} from "react-router-dom";

const AppLink = (props: LinkProps) => {
    const loc = useLocation()
    return <Link
        {...props}
        state={{
            from: loc
        }}
    >{props.children}</Link>
};

export default AppLink;