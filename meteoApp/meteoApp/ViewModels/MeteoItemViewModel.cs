using Xamarin.Forms;

namespace meteoApp
{
    public class MeteoItemViewModel : BaseViewModel
    {
        Entry _entry;
        public Entry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public MeteoItemViewModel(Entry entry)
        {
            _entry = entry;
        }
    }
}