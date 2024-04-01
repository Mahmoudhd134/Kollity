import RegisterModel from "models/Auth/RegisterModel";

export interface AddDoctorModel extends RegisterModel {
    nationalNumber: string;
}