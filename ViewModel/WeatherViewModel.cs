using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApplication.Model;
using NoteApplication.ViewModel.Commands;
using NoteApplication.ViewModel.Helpers;

namespace NotesApplication.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;

        public  string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> CitiesCollection { get; set; }

        private CurrentWeather currentWeather;

        public CurrentWeather CurrentWeather
        {
            get { return currentWeather; }
            set 
            { 
                currentWeather = value; 
                OnPropertyChanged("CurrentWeather"); 
            }
        }

        private City chosenCity;

        public City ChosenCity
        {
            get { return chosenCity; }
            set 
            { 
                chosenCity = value;
                if (chosenCity != null)
                {
                    OnPropertyChanged("ChosenCity");
                    GetCurrentWeather();
                }
            }
        }

        
        private async void GetCurrentWeather()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == false)
            {
                Query = string.Empty;
                CurrentWeather = await AccuWeatherHelper.GetCurrentWeatherA(chosenCity.Key);
                CitiesCollection.Clear();
            }
        
        }
        


        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == true)
            {
                ChosenCity = new City()
                {
                    LocalizedName = "Bucharest"
                };
                CurrentWeather = new()
                {
                    WeatherText = "Sunny",
                    Temperature = new()
                    {
                        Metric = new()
                        {
                            Value = "23"
                        }
                    }
                };
            }
            SearchCommand= new(this);
            CitiesCollection = new ObservableCollection<City>();


        }
        public async void MakeQuery()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == false)
            {
                CitiesCollection.Clear();
                List<City> cities = await AccuWeatherHelper.GetCities(Query);
                foreach (var city in cities)
                {
                    CitiesCollection.Add(city);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
