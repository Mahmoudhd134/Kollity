import {useParams} from "react-router";

const StudentProfile = () => {
    const {id} = useParams<{ id: string }>()
    if(!id)
        return <h3>id not present</h3>
    
    return (
        <div>

        </div>
    );
};

export default StudentProfile;