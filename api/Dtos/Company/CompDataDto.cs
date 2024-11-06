using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompDataDto
    {
        public string symbol { get; set; }
        public List<string> PeerList { get; set; }
    }
}