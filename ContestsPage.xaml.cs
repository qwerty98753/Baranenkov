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
    public partial class ContestsPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public ContestsPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;
            UpdateDataGrid();
        }

        private void SortMembersComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var members = DataGrid.SelectedItems.Cast<Contest>().ToList();

            switch (SortMembersComboBox.SelectedItem.ToString())
            {
                case "Name":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Name);
                    break;
                case "Description":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Description);
                    break;
            }
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var search = SearchTextBox.Text.ToLower();

            if (MainPage.IsNullOrEmpty(search))
            {
                UpdateDataGrid();
            }
            else
            {
                DataGrid.ItemsSource = Database.Contests.Where(x => x.Name.ToLower() == search).ToList();
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Contest currentContest)
            {
                var page = new ContestEditPage(Database, currentContest);
                page.SaveButton.Click += UpdateDataGrid;
                page.SaveButton.Click += BackButtonClick;
                Navigation.Navigate(page);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new ContestEditPage(Database);
            page.SaveButton.Click += UpdateDataGrid;
            page.SaveButton.Click += BackButtonClick;
            Navigation.Navigate(page);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var toRemove = DataGrid.SelectedItems.Cast<Contest>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {toRemove.Count()} записей?", "Удалить?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // https://www.infinetsoft.com/Post/Unable-to-update-the-EntitySet-table-because-it-has-DefiningQuery-and-no-DeleteFunction-element-exists-in-the-ModificationFunctionMapping/3060#.Ypho-dVBzcs
                Database.Contests.RemoveRange(toRemove);
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
            DataGrid.ItemsSource = Database.Contests.ToList();
        }
    }
}
