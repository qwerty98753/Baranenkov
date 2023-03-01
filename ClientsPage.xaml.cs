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
    public partial class MembersPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public MembersPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;

            UpdateDataGrid();
        }

        private void GenderComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var members = DataGrid.ItemsSource.Cast<UsedMaterial>().ToList();

            switch (SortComboBox.SelectedItem.ToString())
            {
                case "WorkerId":
                    DataGrid.ItemsSource = members.OrderBy(x => x.WorkerId);
                    break;
                case "MaterialId":
                    DataGrid.ItemsSource = members.OrderBy(x => x.MaterialId);
                    break;
                case "Count":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Count);
                    break;
                case "Date":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Date);
                    break;
            }
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var search = SearchTextBox.Text.ToLower();

            DataGrid.ItemsSource = Database.UsedMaterials.Where(x => x.Worker.Name.ToLower() == search).ToList();
            if (MainPage.IsNullOrEmpty(search)) UpdateDataGrid();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is UsedMaterial currentMember)
            {
                var page = new MembersEditPage(Database, currentMember);
                page.SaveButton.Click += UpdateDataGrid;
                page.SaveButton.Click += BackButtonClick;
                Navigation.Navigate(page);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new MembersEditPage(Database);
            page.SaveButton.Click += UpdateDataGrid;
            page.SaveButton.Click += BackButtonClick;
            Navigation.Navigate(page);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var toRemove = DataGrid.SelectedItems.Cast<UsedMaterial>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {toRemove.Count()} записей?", "Удалить?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Database.UsedMaterials.RemoveRange(toRemove);
                Database.SaveChanges();
                UpdateDataGrid();

                MessageBox.Show("Записи удалены");
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
            DataGrid.ItemsSource = Database.UsedMaterials.ToList();

            var first = Database.UsedMaterials.ToList().FirstOrDefault();
            if (first != null)
            {
                SortComboBox.ItemsSource = first.GetType().GetProperties().ToList().Select(x => x.Name).Where(x => x != "Id");
            }
        }
    }
}
