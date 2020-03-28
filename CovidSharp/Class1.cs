﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CovidSharp
{
    public class CoronavirusData
    {
        //Initiates client
        public static IRestClient client = new RestClient("https://coronavirus-tracker-api.herokuapp.com/");

        //Sends a GET request to the API
        public static IRestRequest  request = new RestRequest("v2/latest", Method.GET);

        //Fetches the response from the API
        public IRestResponse response = client.Execute(request);

        public string LatestConfirmed()
        {
            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);
 
            //Stores the 'latest' node in the result variable
            var LatestData = output["latest"];

            //The 'confirmed' sub node of the 'latest' node  is fetched and converted into a string
            var LatestConfirmedData = LatestData["confirmed"].ToString();
            return LatestConfirmedData;
        }

        public string LatestRecovered()
        {
            //Deserializes the reponse
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the result variable
            var LatestData = output["latest"];

            //The 'recovered' sub node of the 'latest' node  is fetched and converted into a string
            var LatestRecoveredData = LatestData["recovered"].ToString();
            return LatestRecoveredData;
        }

        public string LatestDeaths()
        {
            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the result variable
            var LatestData = output["latest"];

            //The 'death' sub node of the 'latest' node  is fetched and converted into a string
            var LatestDeathsData = LatestData["deaths"].ToString();
            return LatestDeathsData;
        }
        
         public string GetCountryList(string source = "jhu")
        {
            string country_list = GetCountryData("country", source);
            return country_list;
        }

        public string GetPopulationList(string source = "jhu")
        {
            string population_list = GetCountryData("country_population", source);
            return population_list;
        }

        public string GetProvinceList(string source = "jhu")
        {
            string province_list = GetCountryData("province", source);
            return province_list;
        }

        public string GetCountyList(string source = "jhu")
        {
            string county_list = GetCountryData("county", source);
            return county_list;
        }

        //General method to get country data.
        private static string GetCountryData(string data_type, string source = "jhu")
        {
            string data_list = null;
            var data_set = new SortedSet<string>();
            try
            {
                //Sends a GET request to the API
                var request = new RestRequest("v2/locations?source=" + source, Method.GET);

                //Fetches the response from the API
                var response = client.Execute(request);

                //Deserializes the response
                JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

                //Stores the 'locations' node
                var locations = output["locations"];
                JArray loc_arr = (JArray)locations;

                //Loop untill all countries and add them in HashSet to remove duplicates.
                for (int index = 0; index < loc_arr.Count; index++)
                {
                    string country_data = locations[index][data_type].ToString();
                    data_set.Add(country_data);
                }

                data_list = string.Join("\n", data_set);
            }
            catch (NullReferenceException ex)
            {
                data_list = "Data not available try changing source";
            }
            return data_list;
        }
        
        
        public string FromCountryCodeConfirmed(string country_code)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country_code=" + country_code, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'confirmed' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryConfirmedData = LatestData["confirmed"].ToString();
            return CountryConfirmedData;
        }

        public string FromCountryCodeRecovered(string country_code)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country_code=" + country_code, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'recovered' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryRecoveredData = LatestData["recovered"].ToString();
            return CountryRecoveredData;
        }

        public string FromCountryCodeDeaths(string country_code)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country_code=" + country_code, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'Deaths' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryDeathsData = LatestData["deaths"].ToString();
            return CountryDeathsData;
        }

        public string FromCountryCodePopulation(string country_code)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country_code=" + country_code, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //The 'country_population' sub node of the 'id' sub node  is fetched and converted into a string
            var CountryCodePopulation = TheID["country_population"].ToString();
            return CountryCodePopulation;
        }

        public string FromCountryNameConfirmed(string country_name)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country=" + country_name, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'confirmed' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryConfirmedData = LatestData["confirmed"].ToString();
            return CountryConfirmedData;
        }

        public string FromCountryNameRecovered(string country_name)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country=" + country_name, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'recovered' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryRecoveredData = LatestData["recovered"].ToString();
            return CountryRecoveredData;
        }

        public string FromCountryNameDeaths(string country_name)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country=" + country_name, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'Deaths' sub node of the 'latest' sub node  is fetched and converted into a string
            var CountryDeathsData = LatestData["deaths"].ToString();
            return CountryDeathsData;
        }

        public string FromCountryNamePopulation(string country_name)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?country=" + country_name, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //The 'country_population' sub node of the 'id' sub node  is fetched and converted into a string
            var CountryPopulationData = TheID["country_population"].ToString();
            return CountryPopulationData;
        }



        public string FromIDConfirmed(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'confirmed' sub node of the 'latest' sub node  is fetched and converted into a string
            var IDConfirmedData = LatestData["confirmed"].ToString();
            return IDConfirmedData;
        }

         public string FromIDRecovered(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'recovered' sub node of the 'latest' sub node  is fetched and converted into a string
            var IDRecovoredData = LatestData["recovered"].ToString();
            return IDRecovoredData;
        }

        public string FromIDDeaths(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'latest' node in the LatestData variable
            var LatestData = output["latest"];

            //The 'deaths' sub node of the 'latest' sub node  is fetched and converted into a string
            var IDDeathsData = LatestData["deaths"].ToString();
            return IDDeathsData;
        }

        public string FromIDLongitude(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //Stores the 'Coordinates' node in the LatestData variable
            var Coordinates = TheID["coordinates"];

            //The 'longitude' sub node of the 'id' sub node  is fetched and converted into a string
            var IDLongitudeData = Coordinates["longitude"].ToString();
            return IDLongitudeData;
        }

        public string FromIDLatitude(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //Stores the 'Coordinates' node in the LatestData variable
            var Coordinates = TheID["coordinates"];

            //The 'latitude' sub node of the 'id' sub node  is fetched and converted into a string
            var IDLatitudeData = Coordinates["latitude"].ToString();
            return IDLatitudeData;
        }

        public string FromIDCountry(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //The 'country' sub node of the 'id' sub node  is fetched and converted into a string
            var IDCountryName = TheID["country"].ToString();
            return IDCountryName;
        }

        public string FromIDProvince(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //The 'province' sub node of the 'id' sub node  is fetched and converted into a string
            var IDProvinceName = TheID["province"].ToString();
            return IDProvinceName;
        }

        public string FromIDPopulation(string ID)
        {
            //Sends a GET request to the API
            var request = new RestRequest("v2/locations?id=" + ID, Method.GET);

            //Fetches the response from the API
            var response = client.Execute(request);

            //Deserializes the response
            JObject output = (JObject)JsonConvert.DeserializeObject(response.Content);

            //Stores the 'locations' node in the location variable
            var Location = output["locations"];

            //Stores the 'id' sub node in the TheID variable
            var TheID = Location[0];

            //The 'country_population' sub node of the 'id' sub node  is fetched and converted into a string
            var IDPopulationData = TheID["country_population"].ToString();
            return IDPopulationData;
        }

    }
}
