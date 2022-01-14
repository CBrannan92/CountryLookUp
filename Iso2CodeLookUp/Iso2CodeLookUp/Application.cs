using Iso2CodeLookUp.Logic;
using Iso2CodeLookUp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Iso2CodeLookUp
{
    public class Application
    {
        private IHttpClientFactory _httpFactory { get; set; }
        public Application(
            IHttpClientFactory httpFactory)
        {
           
            _httpFactory = httpFactory;
        }

        public async Task Run()
        {
            string iso2Code = "";
            Console.WriteLine("Please enter iso2code");
            iso2Code = Console.ReadLine();

            CountryLogic countryLogic = new CountryLogic(_httpFactory);

            CountryInfo countryInfo = await countryLogic.GetCountryInformationByIso2Code(iso2Code);
            if(countryInfo != null)
            {
                Console.WriteLine($"Country: {countryInfo.CountryName}");
                Console.WriteLine($"Region: {countryInfo.Region}");
                Console.WriteLine($"Capital City: { countryInfo.CapitalCity}");
                Console.WriteLine($"Longitude: { countryInfo.Longitude}");
                Console.WriteLine($"Latitude: { countryInfo.Latitude}");
            }
            else
            {
                Console.WriteLine("Please Enter A Valid Iso2Code");
            }
        }
    }
}
