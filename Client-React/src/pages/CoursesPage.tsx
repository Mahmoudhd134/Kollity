import api from "@/api/api";
import { IAuth } from "@/redux/authSlice";
import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";

const studentCourses = (id: string) =>
  import.meta.env.VITE_API_URL + `/Student/${id}/courses`;
const doctorCourses = (id: string) =>
  import.meta.env.VITE_API_URL + `/Doctor/${id}/courses`;

export default function CoursesPage() {
  const passedData: { authData: IAuth } = useOutletContext();
  const [data, setData] = useState([]);
  const authData = passedData.authData;

  const getStudentCourses = async () => {
    try {
      const res = await api.get(studentCourses(authData.id), {
        headers: {
          Authorization: `Bearer ${authData.token}`,
        },
      });
      const data = res.data;

      setData(data?.[0] || []);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (authData.roles.includes("Student")) {
      getStudentCourses();
    }

    if (
      authData.roles.includes("Doctor") ||
      authData.roles.includes("Assistant")
    ) {
      const getDoctorCourses = async () => {
        try {
          const res = await api.get(doctorCourses(authData.id), {
            headers: {
              Authorization: `Bearer ${authData.token}`,
            },
          });
          const data = res.data;

          setData(data?.[0] || []);
        } catch (error) {
          console.log(error);
        }
      };

      getDoctorCourses();
    }
  }, [authData]);

  if (data.length === 0)
    return (
      <div>
        <h3 className="profile-page-h3-title">الكورسات</h3>
        <h4>لا يوجد كورسات مسجلة حاليًا</h4>
      </div>
    );

  return <div></div>;
}
