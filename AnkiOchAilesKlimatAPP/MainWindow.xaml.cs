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
        Category selectedType;
        Measurement measurementValue;
        Country selectedCountry;
        Geolocation geolocation;
        Area selectedArea;



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
        public void UpdateObserverList() //uppdaterar Listboxen med observatörer
        {
            listBoxObservers.ItemsSource = null;
            listBoxObservers.ItemsSource = GetObservers();
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
            comboBoxSubCategory.ItemsSource = GetSubCategories(GetCategories(), selectedCategory.Id);
        }

        private void comboBoxSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = comboBoxCategory.SelectedItem as Category;
            selectedSubCategory = comboBoxSubCategory.SelectedItem as Category;
            comboBoxType.ItemsSource = GetSubCategories(GetCategories(), selectedSubCategory.Id);
        }

        private void comboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCountry = comboBoxCountry.SelectedItem as Country;
            comboBoxArea.ItemsSource = GetCountryArea(GetAreas(), selectedCountry.Id);
        }

        private void buttonRegisterObservation_Click(object sender, RoutedEventArgs e)
        {

            var subCategorin = comboBoxSubCategory.SelectedItem as Category;
            var typeCategorin = comboBoxType.SelectedItem as Category;
            var chosenArea = comboBoxArea.SelectedItem as Area;
            Category theChosenOneCategory;

            if (typeCategorin == null)
            {
                theChosenOneCategory = typeCategorin;
            }
            else
            {
                theChosenOneCategory = subCategorin;
            }

            var geoLoc = new Geolocation //fixa imorgon måndag
            {
                Latitude = 0,//float.Parse(textBxLatitude.Text),
                Longitude = 0,//float.Parse(textBxLongitude.Text),
                AreaId = chosenArea.Id
            };

            var geolocationId = AddGeolocation(geoLoc);
            // Create new geolocation, 

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

            //UpdateObserverationList(); // har ingen sån metod än



        }

        private void checkBoxToday_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (checkBoxToday.IsChecked == true)
            {
                textBoxDate.IsEnabled = true;
            }
            else
            {
                textBoxDate.IsEnabled = false;
            }
        }
    }


}

