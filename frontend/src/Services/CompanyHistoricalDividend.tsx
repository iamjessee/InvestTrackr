import axios from "axios";
import { CompanyHistoricalDividend } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/historicaldividend";

export const getHistoricalDividend = async (query: string) => {
  try {
    const data = await axios.get<CompanyHistoricalDividend>(
      api + `?query=${query}`
    );
    console.log(data); // debug
    return data;
  } catch (error) {
    handleError(error);
  }
};
