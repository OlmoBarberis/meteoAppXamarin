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

        public MeteoItemPage(Entry e)
        {
            //meteoItemViewModel = new MeteoItemViewModel(e);
            //BindingContext = meteoItemViewModel;
            
            InitializeComponent();
            
        }

        


       
    }
}