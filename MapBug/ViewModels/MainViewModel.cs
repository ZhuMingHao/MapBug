using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using MapBug.Utils;

namespace MapBug.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Things = new ObservableCollection<Thing>
            {

                new Thing("Place One", 17.4947934, 78.3996441, SelectMe),
                new Thing("Place Two", 17.3616, 78.4747, SelectMe),
                new Thing("Place Three", 17.4375, 78.4482, SelectMe),
                new Thing("Place Four", 17.3930, 78.4730, SelectMe)
            };
        }

        public ObservableCollection<Thing> Things { get; }

        private Thing _selectedThing;

        public Thing SelectedThing { get => _selectedThing; private set => SetProperty(ref _selectedThing, value); }
        private Thing previousThing;
        private void SelectMe(Thing thing)
        {
            if (previousThing != null)
            {
                previousThing.Avaiable = false;
            }

            if (thing != null)
            {
                thing.Avaiable = true;
                previousThing = thing;
            }
        }
    }

    public class Thing : BaseViewModel
    {
        public Thing(string name, double latitude, double longitude, Action<Thing> selector)
        {
            Name = name;
            Location = new Geopoint(new BasicGeoposition { Latitude = latitude, Longitude = longitude });
            SelectMeCommand = new RelayCommand(() => selector(this));
        }

        public string Name { get; set; }
        public Geopoint Location { get; set; }
        public ICommand SelectMeCommand { get; }
        private bool _avaiable;
        public bool Avaiable
        {
            get => _avaiable;

            set => SetProperty(ref _avaiable, value);

        }
    }

}
