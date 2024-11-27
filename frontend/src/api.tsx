import axios from "axios";
import {
  CompanyBalanceSheet,
  CompanyCashFlow,
  CompanyCompData,
  CompanyIncomeStatement,
  CompanyKeyMetrics,
  CompanyProfile,
  CompanySearch,
  CompanyTenK,
  CompanyHistoricalDividend,
  Dividend,
} from "./company";

// export const getBalanceSheet = async (query: string) => {
//   try {
//     const data = await axios.get<CompanyBalanceSheet[]>(
//       `https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?limit=20&apikey=${process.env.REACT_APP_API_KEY}`
//     );
//     return data;
//   } catch (error: any) {
//     console.log("error message: ", error.message);
//     return null;
//   }
// };

// export const getCashFlow = async (query: string) => {
//   try {
//     const data = await axios.get<CompanyCashFlow[]>(
//       `https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?limit=100&apikey=${process.env.REACT_APP_API_KEY}`
//     );
//     return data;
//   } catch (error: any) {
//     console.log("error message: ", error.message);
//     return null;
//   }
// };

export const getTenK = async (query: string) => {
  try {
    const data = await axios.get<CompanyTenK[]>(
      `https://financialmodelingprep.com/api/v3/sec_filings/${query}?type=10-K&page=0&apikey=${process.env.REACT_APP_API_KEY}`
    );
    return data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    return null;
  }
};

export const getHistoricalDividend = async (query: string) => {
  try {
    const data = await axios.get<CompanyHistoricalDividend>(
      `https://financialmodelingprep.com/api/v3/historical-price-full/stock_dividend/${query}?apikey=${process.env.REACT_APP_API_KEY}`
    );
    return data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    return null;
  }
};
