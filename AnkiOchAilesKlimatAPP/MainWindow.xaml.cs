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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            //var observer = GetObserver(1);
            //var observers = GetObservers();
           
            //var observer = new Observer
            //{
            //    FirstName = "Olle",
            //    LastName = "Andersson"

            //};
            //AddObserver(observer);
            //DeleteObserver(1);
            var observation = new Observation
            {
                Date = DateTime.Now,
                ObserverId = 1,
                GeolocationId = 1

            };
            AddObservation(observation);
        }
    }
}
