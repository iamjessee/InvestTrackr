import axios from "axios";
import { CompanyTenK } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/tenk";

export const getTenK = async (query: string) => {
  try {
    const data = await axios.get<CompanyTenK[]>(api + `?query=${query}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};
