import AppError from "models/AppError.ts";

const FormError = ({error}: { error: AppError }) => {
    return (
        <h3>
            <p className={"text-center text-red-500 text-2xl bg-blue-200 p-2"}>{error.message}</p>
        </h3>
    );
};

export default FormError;