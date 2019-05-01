using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace meteoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeteoItemPage : ContentPage
	{
        private string openWKey = "b038aafc4ea3e367b4fb1fed9126b444";
        private Entry entry;

		public MeteoItemPage (Entry entry)
        { 
            InitializeComponent();

            this.entry = entry;

            GetWeather(entry.Name);
            //GetWeather(42.02, 8.91);
		}

        private async Task GetWeather(String location)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid="+ openWKey);

            //var weather = (string)JObject.Parse(content)["weather"][0]["main"]["temp"];

            //aggiornare la ui secondo i dati ricevuti
            entry.CurrentTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp"];
            entry.MaxTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp_max"];
            entry.MinTemperature = (double)JObject.Parse(content)["weather"][0]["main"]["temp_min"];
        }

        private async Task GetWeather(double lat, double lon)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=" + openWKey);

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];

            //aggiornare la ui secondo i dati ricevuti
            //Weather.Text = weather;
        }
    }
}