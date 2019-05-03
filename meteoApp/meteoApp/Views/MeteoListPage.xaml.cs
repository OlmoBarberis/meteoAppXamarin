using Acr.UserDialogs;
using Plugin.Geolocator;
using System;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace meteoApp.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeteoListPage : ContentPage
    {
        private string openWKey = "cc3c629069bce6858e29dedf0f73213a";
        MeteoListViewModel meteoListViewModel = new MeteoListViewModel();

		public MeteoListPage ()
		{
			InitializeComponent();

            GetLocation();
            BindingContext = meteoListViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemAdded(object sender, EventArgs e)
        {
            ShowPrompt(this);            
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage(e.SelectedItem as Entry)
                {
                    BindingContext = new MeteoItemViewModel(e.SelectedItem as Entry)
                });
            }
        }

        async void GetLocation()
        {
            Entry e = new Entry();
            e = GetWeatherFromPosition().Result;
            e.ID = 0;
            e.Name = "Current Location";
            meteoListViewModel.Entries[0] = e;
        }

        private async Task ShowPrompt(MeteoListPage instance)
        {
            var pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Name,
                OkText = "Create",
                Title = "New Entry",
            });
            // esempio: creo una nuova Entry partendo dal testo e la aggiungo al ViewModel
            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                var entry = GetWeather(pResult.Text).Result;
                entry.ID = (int)(meteoListViewModel.Entries.LongCount());
                entry.Name = pResult.Text;
                
               
                meteoListViewModel.Entries.Add(entry);

                App.Database.SaveEntryAsync(entry);
            }
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