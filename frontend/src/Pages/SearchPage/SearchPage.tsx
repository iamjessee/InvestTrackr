import React, { useState, ChangeEvent, SyntheticEvent } from "react";
import { CompanySearch } from "../../company";
import { searchAPI } from "../../Services/CompanySearchService";
import Search from "../../Components/Search/Search";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import CardList from "../../Components/CardList/CardList";
import {
  portfolioAddAPI,
  portfolioDeleteAPI,
  portfolioGetAPI,
} from "../../Services/PortfolioService";
import { toast } from "react-toastify";
import { useAuth } from "../../Context/useAuth";

interface Props {}

const SearchPage = (props: Props) => {
  const { portfolioValues, setPortfolioValues } = useAuth();
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onPortfolioCreate = async (e: any) => {
    e.preventDefault();
    try {
      const response = await portfolioAddAPI(e.currentTarget[0].value);
      if (response?.status === 204) {
        toast.success("Stock added to portfolio!");
        const updatedPortfolio = await portfolioGetAPI();
        if (updatedPortfolio?.data) {
          setPortfolioValues(updatedPortfolio.data);
        }
      }
    } catch (error) {
      toast.warning("Could not add stock to portfolio!");
      console.error("Error adding to portfolio:", error);
    }
  };

  const onPortfolioDelete = async (e: any) => {
    e.preventDefault();
    try {
      const response = await portfolioDeleteAPI(e.currentTarget[0].value);
      if (response?.status === 200) {
        toast.success("Stock deleted from portfolio!");
        const updatedPortfolio = await portfolioGetAPI();
        if (updatedPortfolio?.data) {
          setPortfolioValues(updatedPortfolio.data);
        }
      }
    } catch (error) {
      toast.warning("Could not add stock to portfolio!");
      console.error("Error removing from portfolio:", error);
    }
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    setServerError(null);
    const result = await searchAPI(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };
  return (
    <>
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio
        portfolioValues={portfolioValues!}
        onPortfolioDelete={onPortfolioDelete}
      />
      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
      {serverError && <div>Unable to connect to API</div>}
    </>
  );
};

export default SearchPage;
