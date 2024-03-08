export default class ApiError {
  static generateError = (error: any) => {
    const err: string | null = error?.response?.data?.errors[0]?.description;

    if (err) {
      return err;
    } else {
      return "global error";
    }
  };
}
