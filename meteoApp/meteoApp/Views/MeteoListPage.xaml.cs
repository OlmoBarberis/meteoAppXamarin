using Acr.UserDialogs;
using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace meteoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeteoListPage : ContentPage
    {
        MeteoListViewModel meteoListViewModel = new MeteoListViewModel();

		public MeteoListPage ()
		{
			InitializeComponent();
            BindingContext = meteoListViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemAdded(object sender, EventArgs e)
        {
            _ = ShowPrompt(this);            
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
                var entry = new Entry(); 

                //entry.ID = (int)(meteoListViewModel.Entries.LongCount());
                entry.Name = pResult.Text;
               
                meteoListViewModel.Entries.Add(entry);

                await App.Database.SaveEntryAsync(entry);

            }
        }

    }
}