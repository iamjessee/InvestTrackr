import axios from "axios";
import { CompanyIncomeStatement } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/incomestatement";

export const getIncomeStatement = async (query: string) => {
  try {
    const data = await axios.get<CompanyIncomeStatement>(
      api + `?query=${query}`
    );
    return data;
  } catch (error) {
    handleError(error);
  }
};
