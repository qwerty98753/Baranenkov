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
    public partial class MaterialsPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public MaterialsPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;

            UpdateDataGrid();
        }

        private void SortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var members = DataGrid.ItemsSource.Cast<Material>().ToList();

            switch (SortComboBox.SelectedItem.ToString())
            {
                case "NakladnayaNumber":
                    DataGrid.ItemsSource = members.OrderBy(x => x.NakladnayaNumberID);
                    break;
                case "Date":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Date);
                    break;
                case "Name":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Name);
                    break;
                case "Count":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Count);
                    break;
                case "Price":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Price);
                    break;
                case "Sum":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Sum);
                    break;
            }
        }
     
        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var search = SearchTextBox.Text.ToLower();

            DataGrid.ItemsSource = Database.Materials.Where(x => x.Name.ToLower() == search).ToList();
            if (MainPage.IsNullOrEmpty(search)) UpdateDataGrid();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Material currentMaterial)
            {
                var page = new MaterialEditPage(Database, currentMaterial);
                page.SaveButton.Click += UpdateDataGrid;
                page.SaveButton.Click += BackButtonClick;
                Navigation.Navigate(page);
            }
        }

       

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Navigation.GoBack();
        }

        private void UpdateDataGrid(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            DataGrid.ItemsSource = Database.Materials.ToList();

            var first = Database.Materials.ToList().FirstOrDefault();
            if (first != null)
            {
                SortComboBox.ItemsSource = first.GetType().GetProperties().ToList().Select(x => x.Name).Where(x => x != "Id" && x != "Suppliers");
            }
        }
    }
}
