import axios from "axios";

// Create an axios instance
const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}`,
  withCredentials: true,
});

export default api;
