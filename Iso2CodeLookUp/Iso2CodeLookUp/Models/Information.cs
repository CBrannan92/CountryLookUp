using System;
using System.Collections.Generic;
using System.Text;

namespace Iso2CodeLookUp.Models
{
    public class Information
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public string Per_page { get; set; }
        public int Total { get; set; }
        public List<Country> Countries {get; set;}
    }
}
