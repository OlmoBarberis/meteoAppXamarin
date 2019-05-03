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
        private Entry oldEntry;

        public MeteoItemPage(Entry e)
        {
            InitializeComponent();

            oldEntry = e;
             
            BindingContext = new MeteoItemViewModel(e);
            
            if(e.Name.Equals("Current Location"))
            {
                GetWeatherFromPosition();
            } else
            {
                GetWeather(e.Name);
            }
        }

        private async Task GetWeather(String location)
        {

            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + openWKey);

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];
            var entry = new Entry();
            Debug.WriteLine("Weather: " + weather);

            entry.Name = location;

            //Get data from response
            entry.CurrentTemperature = (double)JObject.Parse(content)["main"]["temp"];
            Debug.WriteLine("Current: " + entry.CurrentTemperature);

            entry.MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"];
            Debug.WriteLine("Max: " + entry.MaxTemperature);

            entry.MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"];
            Debug.WriteLine("Min: " + entry.MinTemperature);

            //Update View through ViewModel 
            var temp = BindingContext as MeteoItemViewModel;
            temp.Entry = entry;
        }

        private async Task GetWeatherFromPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var lat = position.Latitude;
            var lon = position.Longitude;
            var httpClient = new HttpClient();

            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=" + openWKey);
            var entry = new Entry();

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];

            //Get data from response
            entry.Name = oldEntry.Name;
            Debug.WriteLine("Name: " + entry.Name);

            entry.CurrentTemperature = (double)JObject.Parse(content)["main"]["temp"];
            Debug.WriteLine("Current: " + entry.CurrentTemperature);

            entry.MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"];
            Debug.WriteLine("Max: " + entry.MaxTemperature);

            entry.MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"];
            Debug.WriteLine("Min: " + entry.MinTemperature);

            //Update View through ViewModel 
            var temp = BindingContext as MeteoItemViewModel;
            temp.Entry = entry;
        }
    }
}