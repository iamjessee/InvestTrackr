import React, { useEffect, useState } from "react";
import { CompanyProfile } from "../../company";
import { useParams } from "react-router-dom";
import { getCompanyProfile } from "../../Services/CompanyProfileService";
import Sidebar from "../../Components/Sidebar/Sidebar";
import CompanyDashboard from "../../Components/CompanyDashboard/CompanyDashboard";
import Tile from "../../Components/Tile/Tile";
import Spinner from "../../Components/Spinners/Spinner";
import CompFinder from "../../Components/CompFinder/CompFinder";
import TenKFinder from "../../Components/TenKFinder/TenKFinder";

interface Props {}

const CompanyPage = (props: Props) => {
  let { ticker } = useParams<string>();

  const [company, setCompany] = useState<CompanyProfile | null>(null); // Allow null to indicate loading or no data
  const [error, setError] = useState<string | null>(null); // State to hold error message
  const [isLoading, setIsLoading] = useState(true);

  const fetchCompanyData = async () => {
    setIsLoading(true);
    try {
      if (ticker) {
        const result = await getCompanyProfile(ticker);
        if (result.error) {
          setError(result.error);
          setCompany(null);
        } else {
          setCompany(result.data);
          setError(null);
        }
      }
    } catch (err) {
      setError("Failed to fetch company data");
      setCompany(null);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    // Initial fetch
    fetchCompanyData();

    // Set up interval for periodic refresh (every 5 minutes)
    const intervalId = setInterval(() => {
      fetchCompanyData();
    }, 5 * 60 * 1000); // 5 minutes in milliseconds

    // Cleanup interval on component unmount
    return () => clearInterval(intervalId);
  }, [ticker]); // Only re-run if ticker chang
  //   const getProfileInit = async () => {
  //     if (ticker) {
  //       const result = await getCompanyProfile(ticker!); // Call the service
  //       if (result.error) {
  //         setError(result.error); // Set error if there's an issue
  //         setCompany(null); // Clear company data
  //       } else {
  //         setCompany(result.data); // Set company data if successful
  //         setError(null); // Clear any existing error
  //       }
  //     }
  //   };
  //   getProfileInit();
  // });

  return (
    <>
      {error ? (
        <p className="error-message">{error}</p> // Display error message if exists
      ) : (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          {isLoading && !company ? (
            <Spinner />
          ) : (
            company && (
              <CompanyDashboard ticker={ticker!}>
                <Tile title="Company Name" subTitle={company.companyName} />
                <Tile title="Price" subTitle={"$" + company.price.toString()} />
                <Tile title="DCF" subTitle={"$" + company.dcf.toString()} />
                <Tile title="Sector" subTitle={company.sector} />
                <CompFinder ticker={company.symbol} />
                <TenKFinder ticker={company.symbol} />
                <p className="bg-white shadow rounded text-medium font-medium text-gray-900 p-3 mt-1 m-4">
                  {company.description}
                </p>
              </CompanyDashboard>
            )
          )}
        </div>
      )}
    </>
  );
};

export default CompanyPage;
