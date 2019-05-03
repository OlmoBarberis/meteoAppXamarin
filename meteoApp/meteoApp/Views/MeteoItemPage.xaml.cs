using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Plugin.Geolocator;

namespace meteoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeteoItemPage : ContentPage
    {
        private string openWKey = "cc3c629069bce6858e29dedf0f73213a";
        private Entry entry;
        private double lat;
        private double lon;
        public MeteoItemPage(Entry e)
        {
            entry = e;
            BindingContext = new MeteoItemViewModel(e);
            InitializeComponent();
            Debug.WriteLine("Loaded: " + BindingContext.ToString());
            if (e.Name.Equals("Current Location"))
            {
                _ = GetWeatherFromPosition();
            }
            else
            {
                _ = GetWeather(e.Name);
            }
        }

        private async Task<String> GetLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            lat = position.Latitude;
            lon = position.Longitude;
            if (lat != 0 && lon != 0)
                return "found";
            return "not found";
        }

        private async Task GetWeather(String location)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + openWKey);

            //var weather = (string)JObject.Parse(content)["weather"][0]["main"]["temp"];

            Debug.WriteLine("Weather: " + weather);
            Debug.WriteLine("Place: " + entry.Name);

            //aggiornare la ui secondo i dati ricevuti
            entry.CurrentTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp"];
            entry.MaxTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp_max"];
            entry.MinTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp_min"];
        }

        private async Task GetWeatherFromPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            lat = position.Latitude;
            lon = position.Longitude;
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=" + openWKey);

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];

            Debug.WriteLine("Weather: " + weather);
            Debug.WriteLine("Latitude: " + lat);
            Debug.WriteLine("Longitude: " + lon);

            //aggiornare la ui secondo i dati ricevuti
            //Weather.Text = weather;
        }
    }
}