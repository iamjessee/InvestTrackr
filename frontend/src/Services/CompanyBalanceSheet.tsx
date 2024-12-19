import axios from "axios";
import { CompanyBalanceSheet } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/balancesheet";

export const getBalanceSheet = async (query: string) => {
  try {
    const data = await axios.get<CompanyBalanceSheet>(api + `?query=${query}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};
