﻿using System;
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
                Name = "Entry " + 0,
                MinTemperature = 20.0,
                MaxTemperature = 25.0,
                CurrentTemperature = 23.0
            };

            Entries.Add(e);
        }
    }
}