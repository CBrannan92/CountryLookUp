using System;
using System.Collections.Generic;
using System.Text;

namespace Iso2CodeLookUp.Models
{
    public class Country
    {
        public string Id { get; set; }
        public string Iso2Code { get; set; }
        public string Name { get; set; }
        public string CapitalCity { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public AdminRegion AdminRegion { get; set; }

        public IncomeLevel IncomeLevel { get; set; }
        public LendingType LendingType { get; set; }
        public Region Region { get; set; }

    }
}
