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

        public MeteoItemPage(Entry e)
        {
            //meteoItemViewModel = new MeteoItemViewModel(e);
            //BindingContext = meteoItemViewModel;

            BindingContext = new MeteoItemViewModel(e);
            
            InitializeComponent();
            
        }

        private async Task<Entry> GetWeather(String location)
        {

            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + openWKey);

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];
            var entry = new Entry();
            Debug.WriteLine("Weather: " + weather);
            //aggiornare la ui secondo i dati ricevuti
            entry.CurrentTemperature = (double)JObject.Parse(content)["main"]["temp"];
            entry.MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"];
            entry.MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"];

            return entry;
        }

        private async Task<Entry> GetWeatherFromPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var lat = position.Latitude;
            var lon = position.Longitude;
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=" + openWKey);
            var entry = new Entry();
            var weather = (string)JObject.Parse(content)["weather"][0]["main"];
            entry.CurrentTemperature = (double)JObject.Parse(content)["main"]["temp"];
            entry.MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"];
            entry.MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"];

            return entry;
            //aggiornare la ui secondo i dati ricevuti
            //Weather.Text = weather;

        }
    }
}