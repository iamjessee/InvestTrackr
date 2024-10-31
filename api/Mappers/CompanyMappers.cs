using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Company;

namespace api.Mappers
{
    public static class CompanyMappers {

        // Maps FMPStock data to CompanyProfileDto
        public static CompanyProfileDto ToCompanyProfileDto(this FMPStock fmpStock)
        {
            return new CompanyProfileDto
            {
                Symbol = fmpStock.symbol,
                Price = fmpStock.price,
                Beta = fmpStock.beta,
                VolAvg = fmpStock.volAvg,
                MktCap = fmpStock.mktCap,
                LastDiv = fmpStock.lastDiv,
                Range = fmpStock.range,
                Changes = fmpStock.changes,
                CompanyName = fmpStock.companyName,
                Currency = fmpStock.currency,
                Cik = fmpStock.cik,
                Isin = fmpStock.isin,
                Exchange = fmpStock.exchange,
                ExchangeShortName = fmpStock.exchangeShortName,
                Industry = fmpStock.industry,
                Website = fmpStock.website,
                Description = fmpStock.description,
                Ceo = fmpStock.ceo,
                Sector = fmpStock.sector,
                Counter = fmpStock.country,
                FullTimeEmployees = fmpStock.fullTimeEmployees,
                Phone = fmpStock.phone,
                Address = fmpStock.address,
                City = fmpStock.city,
                State = fmpStock.state,
                Zip = fmpStock.zip,
                DcfDiff = fmpStock.dcfDiff,
                Dcf = fmpStock.dcf,
                Image = fmpStock.image,
                IpoDate = fmpStock.ipoDate,
                DefaultImage = fmpStock.defaultImage,
                IsEtf = fmpStock.isEtf,
                IsActivelyTrading = fmpStock.isActivelyTrading,
                IsAdr = fmpStock.isAdr,
                IsFund = fmpStock.isFund
            };
        }
    }
}
