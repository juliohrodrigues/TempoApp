using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TempoApp.Model;

namespace AppTempoAgora.Services
{
    class DataService
    {
        public static async Task<Tempo> GetPrevisaoDoTempo(string cidade)
        {
            string appId = "d86817eca8df0019a8483587f4aa9d6e";

            string queryString = "http://api,openweathermap.org/data/2.5/weather?q=" + cidade + "&units=metric" + "&appid=" + appId;
            dynamic resultado = await getDataFromService(queryString).ConfigureAwait(false);
            if (resultado["weather"] != null)
            {
                Tempo previsao = new Tempo();
                previsao.Title = (string)resultado["name"];
                previsao.Temperature = (string)resultado["main"]["temp"] + "C";
                previsao.Wind = (string)resultado["wind"]["speed"] + "mhp";
                previsao.Humidity = (string)resultado["main"]["humidity"] + "%";
                previsao.Visibility = (string)resultado["weather"][0]["main"];
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)resultado["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)resultado["sys"]["sunset"]);
                previsao.Sunrise = String.Format("{0:d/MM/yyyy HH:mm:ss", sunrise);
                previsao.Sunset = String.Format("{0:d/MM/yyyy HH:mm:ss", sunset);
                return previsao;
            }
            else
            {
                return null;
            }

            public static async Task<dynamic> getDataFromService(string queryString)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(queryString);
                dynamic data = null;
                if (response != null)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject(json);
                }
                return data;
            }

            public static async Task<dynamic> getDataFromServiceByCity(string city)
            {
                string appId = "db013655cf9032acd9998a8a0fbc0b70";

                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", city.Trim(), appId);
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(queryString);
                dynamic data = null;
                if (response != null)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject(json);
                }
                return data;
            }
        }
    }
}