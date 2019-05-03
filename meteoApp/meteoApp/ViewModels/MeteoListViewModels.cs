using System;
using System.Collections.ObjectModel;

namespace meteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Entry> _entries;

        public ObservableCollection<Entry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();

            var e = new Entry
            {
                ID = 0,
                Name = "Current Location"
            };

            Entries.Add(e);
            foreach (Entry entry in App.Database.GetEntryAsync().Result)
            {
                Entries.Add(entry);
            }

        }
    }
}