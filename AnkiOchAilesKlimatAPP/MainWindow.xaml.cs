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

        #region TYPECATEGORY

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

        private void buttonRegisterObservation_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
