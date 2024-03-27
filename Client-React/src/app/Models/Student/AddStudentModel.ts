import RegisterModel from "@/app/Models/Auth/RegisterModel.ts";

export default interface AddStudentModel extends RegisterModel {
    code: string
}