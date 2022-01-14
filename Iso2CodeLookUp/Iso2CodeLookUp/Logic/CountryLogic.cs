using Iso2CodeLookUp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Iso2CodeLookUp.Logic
{
    public class CountryLogic
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public CountryLogic(IHttpClientFactory httpClientFactory)
        {

            this.HttpClientFactory = httpClientFactory;
        }
        public async Task<CountryInfo> GetCountryInformationByIso2Code(string iso2Code)
        {

            var path = $"https://api.worldbank.org/v2/country/br?format=json";
            HttpClient client = HttpClientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var json = await response.Content.ReadAsStringAsync();
                    JArray jArray = JArray.Parse(json);

                    var information = JsonConvert.DeserializeObject<Information>(jArray[0].ToString());
                    var countries = JsonConvert.DeserializeObject<List<Country>>(jArray[1].ToString());

                    Country selectedCountry = countries.FirstOrDefault(x => x.Iso2Code.ToUpper() == iso2Code.ToUpper());
                    if (selectedCountry != null)
                    {
                        CountryInfo countryInfo = new CountryInfo()
                        {
                            CountryName = selectedCountry.Name,
                            Region = selectedCountry.Region.Value,
                            CapitalCity = selectedCountry.CapitalCity,
                            Longitude = selectedCountry.Longitude,
                            Latitude = selectedCountry.Latitude
                        };

                        return countryInfo;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }

            }
            else
            {
                return null;
            }


        }
    }
}
