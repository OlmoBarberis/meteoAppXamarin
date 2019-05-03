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

namespace meteoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeteoListPage : ContentPage
    {
        MeteoListViewModel meteoListViewModel = new MeteoListViewModel();
        public MeteoListPage()
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
            Debug.WriteLine("OnItemAdded");

            //          /!\ FUNZIONA SOLO SU ANDROID!  /!\ 

            ShowPrompt(this);
        }
        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage(e.SelectedItem as Entry));
            }
        }
        
        private async Task ShowPrompt(MeteoListPage instance)
        {
            Debug.WriteLine("Show Prompt");
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