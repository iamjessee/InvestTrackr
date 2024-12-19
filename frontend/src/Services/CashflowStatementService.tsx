import axios from "axios";
import { CompanyCashFlow } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/cashflow";

export const getCashFlow = async (query: string) => {
  try {
    const data = await axios.get<CompanyCashFlow>(api + `?query=${query}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};
