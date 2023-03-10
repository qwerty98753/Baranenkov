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

namespace Program
{
    public partial class MainWindowForADMIN : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public MainWindowForADMIN(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;
        }

        private void ClientsButtonClick(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(new SuppliersPage(Navigation, Database));
        }



        private void WorkersButtonClick(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(new WorkersPage(Navigation, Database));
        }

        public static bool IsNullOrEmpty(String s)
        {
            return s.Length <= 0 || String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

