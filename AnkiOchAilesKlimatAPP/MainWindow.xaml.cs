using AnkiOchAilesKlimatAPP.Models;
using Npgsql;
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
    public partial class MainWindow : Window //skapar variabler
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
            listBoxObservers.ItemsSource = GetObservers(); //alla klimatobservatörer ska visas direkt då vi startar applikationen
            comboBoxCountry.ItemsSource = GetCountries(); //tillkallar landen till comboboxen 
            comboBoxCategory.ItemsSource = GetMainCategories(GetCategories()); //tillkallar huvudkategori till comboboxen
            comboBoxCountry.SelectedIndex = 0;
            comboBoxCategory.SelectedIndex = 0;
            //sätter att första indexet i land- och kategori listan ska visas så att det inte råkar bli null-error om man glömmer

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
        public List<Category> GetMainCategories(IEnumerable<Category> categories) //en metod för att leta fram huvudkategorierna
        {
            List<Category> mainCategories = new List<Category>();

            foreach (var item in categories)
            {
                if (item.BaseCategoryId == 0) //huvudkategorierna är nullad på baskategoriId, så därför ska den söka efter de objekt i listan med baskategoriId noll
                {
                    mainCategories.Add(item); // lägg till
                }
            }
            return mainCategories;
        }
        #endregion

        #region SUBCATEGORY
        public List<Category> GetSubCategories(IEnumerable<Category> categories, int mainCategory) //en metod för att leta fram underkategorier från kategorier
        {
            List<Category> subCategories = new List<Category>();

            foreach (var item in categories)
            {
                if (item.BaseCategoryId == mainCategory) //om bas-id'et är kopplat till huvudkategorierna
                {
                    subCategories.Add(item); //lägg till
                }
            }
            return subCategories;
        }
        #endregion

        #region GETCOUNTRYAREA

        public List<Area> GetCountryArea(IEnumerable<Area> areas, int countryId) //en metod för att koppla varje område till rätt land
        {
            List<Area> getArea = new List<Area>();

            foreach (var item in areas)
            {
                if (item.CountryId == countryId) //om id'et på området är kopplat till samma id som landet har
                {
                    getArea.Add(item); //lägg till
                }
            }
            return getArea;
        }

        #endregion

        #region GETOBSERVATIONDATES
        public List<Observation> GetObservationDates(IEnumerable<Observation> observations, int observerId) //metod för att få fram rätt observation till rätt observatör
        {
            List<Observation> observationDates = new List<Observation>();

            foreach (var observation in observations)
            {
                if (observation.ObserverId == observerId) //om observationen i listan har samma observerId som observatören vi valt
                {
                    observationDates.Add(observation); // lägg till i listan
                }
            }
            return observationDates;
        }


        #endregion

        #region UPDATELISTS
        public void UpdateObserverList() //uppdaterar Listboxen med observatörer
        {
            listBoxObservers.ItemsSource = null;
            listBoxObservers.ItemsSource = GetObservers();
        }
        public void UpdateObservationList() //uppdaterar Listboxen med observationer
        {
            selectedObserver = listBoxObservers.SelectedItem as Observer;
            listBoxObservation.ItemsSource = null;
            if (selectedObserver != null)
            {
                listBoxObservation.ItemsSource = GetObservationDates(GetObservations(), selectedObserver.Id);
            }
           

        }

        #endregion

        #region DELETEOBSERVER

        private void buttonDeleteObserver_Click(object sender, RoutedEventArgs e)
        { //metod för att ta bort observatör men om detta inte går (pga restricted inställning) få ett felmeddelande

            try
            {
                selectedObserver = listBoxObservers.SelectedItem as Observer;
                DeleteObserver(selectedObserver.Id);
                UpdateObserverList();
            }
            catch (PostgresException error)
            {
                if (error.SqlState == "23503")
                {
                    MessageBox.Show("Observatören har en eller flera registrerade observationer. Kan ej raderas.", "Felmeddelande");
                }

            }

        }
        #endregion
        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = comboBoxCategory.SelectedItem as Category;
            comboBoxSubCategory.SelectedIndex = 0; //sätter första underkategorin i listan att visas så det inte råkar bli null-error om man glömt välja
            comboBoxSubCategory.ItemsSource = null;
            comboBoxSubCategory.ItemsSource = GetSubCategories(GetCategories(), selectedCategory.Id); //kallar in GetCategories i metoden för att det är listan den ska söka igenom
        }

        private void comboBoxSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = comboBoxCategory.SelectedItem as Category;
            selectedSubCategory = comboBoxSubCategory.SelectedItem as Category;

            // Om den valda underkategorin skiljer sig från null så sätts selectedindex till 0, alltså första i listan.
            if (selectedSubCategory != null)
            {
                comboBoxType.SelectedIndex = 0;
                comboBoxType.ItemsSource = GetSubCategories(GetCategories(), selectedSubCategory.Id);
            }
        }

        private void comboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCountry = comboBoxCountry.SelectedItem as Country;
            comboBoxArea.SelectedIndex = 0; //sätter första området i listan att visas
            comboBoxArea.ItemsSource = GetCountryArea(GetAreas(), selectedCountry.Id);  //kallar in GetAreas i metoden för att det är listan den ska söka igenom för att hitta rätt område
        }

        private void buttonRegisterObservation_Click(object sender, RoutedEventArgs e)
        {
            selectedObserver = listBoxObservers.SelectedItem as Observer; 
            var subCategorin = comboBoxSubCategory.SelectedItem as Category;
            var typeCategorin = comboBoxType.SelectedItem as Category;
            var chosenArea = comboBoxArea.SelectedItem as Area;

            Category theChosenOneCategory;
            //om comboxen med vinter/sommardräkt är tom så ska underkategorin bli den valda kategorin
            //sen ett villkor för att inte få null-error vid longitut och latitud
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
            // Skapa ny geolocation
            var geoLoc = new Geolocation
            {
                Latitude = float.Parse(textBxLatitude.Text),
                Longitude = float.Parse(textBxLongitude.Text),
                AreaId = chosenArea.Id
            };
            
            var geolocationId = AddGeolocation(geoLoc);
           

            var newObservation = new Observation()
            {
                //om checkboxen är checkad så är det dagens datum, annars det som väljs
                Date = checkBoxToday.IsChecked.GetValueOrDefault() ? DateTime.Now : observationDatePicker.SelectedDate.GetValueOrDefault(), 
                ObserverId = selectedObserver.Id,
                GeolocationId = geolocationId
            };
            // Skapa ny observation
            var observationId = AddObservation(newObservation);

            var value = new Measurement
            {
                Value = double.Parse(textBxValues.Text),
                ObservationId = observationId,
                CategoryId = theChosenOneCategory.Id
            };

            AddMeasurement(value);
            UpdateObservationList();
        }

        private void listBoxObservers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateObservationList();
        }

        private void listBoxObservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedObservation = listBoxObservation.SelectedItem as Observation;
            if (selectedObservation != null) //om den inte är null så ska den uppdatera informationen, annars skiter den i det
            {
                listBoxMeasurements.ItemsSource = GetInformation(selectedObservation.Id);
            } 
        }

        private void listBoxMeasurements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedInformationDisplay = listBoxMeasurements.SelectedItem as InformationDisplay;
            if (selectedInformationDisplay != null)
            {
                UpdateInformation(selectedInformationDisplay.Measurement_id);
            }
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        { // Ändra värde mätpunkt
            selectedInformationDisplay = listBoxMeasurements.SelectedItem as InformationDisplay;
            UpdateMeasurement(selectedInformationDisplay.Measurement_id, double.Parse(textBoxChangeValue.Text));
            UpdateInformation(selectedInformationDisplay.Measurement_id);
        }

        public void UpdateInformation(int measurement_id) // Metod uppdatera information (measurement_id) för ändring av mätpunkt.
        {
            InformationDisplay informationDisplay = GetUpdatedInformation(measurement_id);
            string info = $"Land: {informationDisplay.CountryName}\nOmråde: {informationDisplay.AreaName}\nLatitud: {informationDisplay.Latitude}\nLongitud: {informationDisplay.Longitude}\nKategori: {informationDisplay.Category}\nMätvärde: {informationDisplay.Type} {informationDisplay.Value} {informationDisplay.Abbrevation}";
            textBoxInformation.Text = info;
            textBoxInformation.Text = textBoxInformation.Text.Replace("\n", Environment.NewLine);
        }

        private void buttonAddMeasurement_Click(object sender, RoutedEventArgs e)
        {
            
            var subCategorin = comboBoxSubCategory.SelectedItem as Category;
            var typeCategorin = comboBoxType.SelectedItem as Category;

            Category theChosenOneCategory;
            //om comboxen med vinter/sommardräkt är tom så ska underkategorin bli den valda kategorin
            //sen ett villkor för att inte få null-error vid longitut och latitud
            if (typeCategorin == null)
            {
                theChosenOneCategory = subCategorin;
            }
            else
            {
                theChosenOneCategory = typeCategorin;
            }

            var value = new Measurement
            {
                Value = double.Parse(textBxValues.Text),
                ObservationId = selectedObservation.Id,
                CategoryId = theChosenOneCategory.Id
            };

            AddMeasurement(value);
            listBoxMeasurements.ItemsSource = GetInformation(selectedObservation.Id);
        }
    }

}


