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
        public MainWindow()
        {
            InitializeComponent();
            listBoxObservers.ItemsSource = null;
            listBoxObservers.ItemsSource = GetObservers();
            comboBoxCountry.ItemsSource = GetCountries();

            //ska komma upp  när man valt land // comboBoxArea.ItemsSource = GetAreas();
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
    }
}
