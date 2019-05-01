using Acr.UserDialogs;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace meteoApp.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeteoListPage : ContentPage
	{
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
                    BindingContext = e.SelectedItem as Entry
                });
            }
        }

        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Debug.WriteLine("Position Status: {0}", position.Timestamp);
            Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            Debug.WriteLine("Position Longitude: {0}", position.Longitude);
            Entry e = new Entry();
            e.ID = 0;
            e.Name = position.Timestamp.DateTime.ToString();
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
                var newEntry = new Entry
                {
                    ID = (int)(meteoListViewModel.Entries.LongCount()),
                    Name = pResult.Text
                };
                meteoListViewModel.Entries.Add(newEntry);
                App.Database.SaveEntryAsync(newEntry);
            }
        }
    }
}