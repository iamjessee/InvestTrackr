import axios from "axios";
import { CompanyKeyMetrics } from "../company";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5261/api/company/keymetrics";

export const getKeyMetrics = async (symbol: string) => {
  try {
    const data = await axios.get<CompanyKeyMetrics>(api + `?query=${symbol}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};
