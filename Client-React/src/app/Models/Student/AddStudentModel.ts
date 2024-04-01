import RegisterModel from "models/Auth/RegisterModel.ts";

export default interface AddStudentModel extends RegisterModel {
    code: string
}