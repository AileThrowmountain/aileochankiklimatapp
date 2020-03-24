using AnkiOchAilesKlimatAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AnkiOchAilesKlimatAPP.Repositories.ObserverRepository;

namespace AnkiOchAilesKlimatAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Observer selectedObserver;
        Category selectedCategory;
        Category selectedSubCategory;
        Country selectedCountry;
        Observation selectedObservation;
        InformationDisplay selectedInformationDisplay;




        public MainWindow()
        {
            InitializeComponent();
            listBoxObservers.ItemsSource = null;
            listBoxObservers.ItemsSource = GetObservers();
            comboBoxCountry.ItemsSource = GetCountries();
            comboBoxCategory.ItemsSource = GetMainCategories(GetCategories());

        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            var observer = new Observer
            {
                FirstName = textBxRegisterFirstname.Text,
                LastName = textBxRegisterLastname.Text

            };
            AddObserver(observer);
            UpdateObserverList();


        }
        #region MAINCATEGORYMETOD
        public List<Category> GetMainCategories(IEnumerable<Category> categories)
        {
            List<Category> mainCategories = new List<Category>();

            foreach (var item in categories)
            {
                if (item.BaseCategoryId == 0)
                {
                    mainCategories.Add(item);
                }
            }
            return mainCategories;
        }
        #endregion

        #region SUBCATEGORY
        public List<Category> GetSubCategories(IEnumerable<Category> categories, int mainCategory)
        {
            List<Category> subCategories = new List<Category>();

            foreach (var item in categories)
            {
                if (item.BaseCategoryId == mainCategory)
                {
                    subCategories.Add(item);
                }
            }
            return subCategories;
        }
        #endregion

        #region GETCOUNTRYAREA

        public List<Area> GetCountryArea(IEnumerable<Area> areas, int countryId)
        {
            List<Area> getArea = new List<Area>();

            foreach (var item in areas)
            {
                if (item.CountryId == countryId)
                {
                    getArea.Add(item);
                }
            }
            return getArea;
        }

        #endregion

        #region DELETEOBSERVER

        //public void ObserverDeletePossible(IEnumerable<Observer> observers, int observerId)
        //{
        //    foreach (var observer in observers)
        //    {
        //        if (observer.Id == observerId)
        //        {
        //            MessageBox.Show($"Denna observatören har en eller flera registrerade observationer. Kan ej raderas.")
        //        }
        //        else
        //        {
        //        }
        //    }
        //
        //}




        #endregion


        #region GETOBSERVATIONDATES
        public List<Observation> GetObservationDates(IEnumerable<Observation> observations, int observerId)
        {
            List<Observation> observationDates = new List<Observation>();

            foreach (var observation in observations)
            {
                if (observation.ObserverId == observerId)
                {
                    observationDates.Add(observation);
                }
            }
            return observationDates;
        }


        #endregion
        public void UpdateObserverList() //uppdaterar Listboxen med observatörer
        {
            listBoxObservers.ItemsSource = null;
            listBoxObservers.ItemsSource = GetObservers();
        }
        public void UpdateObservationList() //uppdaterar Listboxen med observatörer
        {
            selectedObserver = listBoxObservers.SelectedItem as Observer;
            listBoxObservation.ItemsSource = null;
            listBoxObservation.ItemsSource = GetObservationDates(GetObservations(), selectedObserver.Id);
        }


        private void buttonDeleteObserver_Click(object sender, RoutedEventArgs e)
        {

            selectedObserver = listBoxObservers.SelectedItem as Observer;
            DeleteObserver(selectedObserver.Id);
            UpdateObserverList();

        }

        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = comboBoxCategory.SelectedItem as Category;
            comboBoxSubCategory.ItemsSource = null;
            comboBoxSubCategory.ItemsSource = GetSubCategories(GetCategories(), selectedCategory.Id);
        }

        private void comboBoxSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = comboBoxCategory.SelectedItem as Category;
            selectedSubCategory = comboBoxSubCategory.SelectedItem as Category;
            if(selectedSubCategory != null)
            {
                comboBoxType.ItemsSource = GetSubCategories(GetCategories(), selectedSubCategory.Id);
            
             //KRASHAR NÄR MAN VÄLJER EN ANNAN HUVUDKATEGORI


            //ändra label beroende på vad man väljer
            if (selectedSubCategory.Id == 4 && selectedSubCategory.Id == 6 && selectedSubCategory.Id == 7)
            {
                labelValues.Content = "Antal";
            }
           else if (selectedSubCategory.Id == 5)
            {
                labelValues.Content = "Lufttemperatur";
            }
           else if (selectedSubCategory.Id == 12 && selectedSubCategory.Id == 13)
            {
                labelValues.Content = "Meter";
            }
            else if (selectedSubCategory.Id == 14)
            {
                labelValues.Content = "Cm";
            }
            }
        }

        private void comboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCountry = comboBoxCountry.SelectedItem as Country;
            comboBoxArea.ItemsSource = GetCountryArea(GetAreas(), selectedCountry.Id);
        }

        private void buttonRegisterObservation_Click(object sender, RoutedEventArgs e)
        {
            selectedObserver = listBoxObservers.SelectedItem as Observer;
            var subCategorin = comboBoxSubCategory.SelectedItem as Category;
            var typeCategorin = comboBoxType.SelectedItem as Category;
            var chosenArea = comboBoxArea.SelectedItem as Area;

            Category theChosenOneCategory;

            if (typeCategorin == null)
            {
                theChosenOneCategory = subCategorin;
            }
            else
            {
                theChosenOneCategory = typeCategorin;
            }

            if (textBxLatitude.Text == "" || textBxLongitude.Text == "")
            {
                textBxLatitude.Text = "0";
                textBxLongitude.Text = "0";
            }
            if (textBxValues.Text == "")
            {
                textBxValues.Text = "0";
            }

            var geoLoc = new Geolocation 
            {
                Latitude = float.Parse(textBxLatitude.Text),
                Longitude = float.Parse(textBxLongitude.Text),
                AreaId = chosenArea.Id
            };

            var geolocationId = AddGeolocation(geoLoc);
            // Create new geolocation

            var newObservation = new Observation() //tjorvar fixa imorgon
            {

                Date = checkBoxToday.IsChecked.GetValueOrDefault() ? DateTime.Now : observationDatePicker.SelectedDate.GetValueOrDefault(), //om checkboxen är checkad så är det dagens datum, annars det som vaäljs
                ObserverId = selectedObserver.Id,
                GeolocationId = geolocationId
            };

            var observationId = AddObservation(newObservation);

            var value = new Measurement
            {
                Value = double.Parse(textBxValues.Text),
                ObservationId = observationId,
                CategoryId = theChosenOneCategory.Id

            };
            AddMeasurement(value);
            UpdateObservationList();

            //UpdateObserverationList(); // har ingen sån metod än

        }




        private void listBoxObservers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateObservationList();
        }

        private void listBoxObservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedObservation = listBoxObservation.SelectedItem as Observation;
            listBoxMeasurements.ItemsSource = null;
            listBoxMeasurements.ItemsSource = GetInformation(selectedObservation.Id);
        }

        private void listBoxMeasurements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInformation();
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {

            UpdateMeasurement(selectedInformationDisplay.Measurement_id, double.Parse(textBxValues.Text));
            UpdateInformation();
        }

       public void UpdateInformation() 
        {
            selectedInformationDisplay = listBoxMeasurements.SelectedItem as InformationDisplay;
            string info = $"Land: {selectedInformationDisplay.CountryName}\nOmråde: {selectedInformationDisplay.AreaName}\nLatitud: {selectedInformationDisplay.Latitude}\nLongitud: {selectedInformationDisplay.Longitude}\nKategori: {selectedInformationDisplay.Category}\nMätvärde: {selectedInformationDisplay.Type} {selectedInformationDisplay.Value} {selectedInformationDisplay.Abbrevation}";
            textBoxInformation.Text = info;
            textBoxInformation.Text = textBoxInformation.Text.Replace("\n", Environment.NewLine);

        }


    }


}

