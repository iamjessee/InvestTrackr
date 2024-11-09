import axios from "axios";
import { CompanyProfile } from "../company";

const api = "http://localhost:5261/api/company/companyprofile";

// Define a response type that can either be a CompanyProfile or an error message
type CompanyProfileResponse =
  | { data: CompanyProfile; error: null }
  | { data: null; error: string };

// Update the service function to return this new type
export const getCompanyProfile = async (ticker: string) => {
  try {
    const response = await axios.get<CompanyProfile>(`${api}?ticker=${ticker}`);
    // Return the company profile data with no error
    return { data: response.data, error: null };
  } catch (error: any) {
    // Handle the error and return a meaningful message
    const errorMessage = axios.isAxiosError(error)
      ? error.message
      : "An unexpected error occurred.";
    return { data: null, error: errorMessage }; // Return null for data and the error message
  }
};
