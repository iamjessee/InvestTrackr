import axios from "axios";
import { CompanySearch } from "../company";

export interface SearchResponse {
  data: CompanySearch[];
}

const api = "http://localhost:5261/api/company/search";

export const searchAPI = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(api + `?query=${query}`);
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.message);
      return error.message;
    } else {
      console.log("unexpected error: ", error);
      return "An unexpected error has occurred.";
    }
  }
};
