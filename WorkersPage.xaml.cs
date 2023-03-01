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
    public partial class WorkersPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public WorkersPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;

            UpdateDataGrid();
        }

        private void SortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Worker = DataGrid.ItemsSource.Cast<Worker>().ToList();

            switch (SortComboBox.SelectedItem.ToString())
            {
                case "Surname":
                    DataGrid.ItemsSource = Worker.OrderBy(x => x.Surname);
                    break;
                case "Name":
                    DataGrid.ItemsSource = Worker.OrderBy(x => x.Name);
                    break;
                case "Patronymic":
                    DataGrid.ItemsSource = Worker.OrderBy(x => x.Patronymic);
                    break;
                case "Workplase":
                    DataGrid.ItemsSource = Worker.OrderBy(x => x.Workplace);
                    break;
            }
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var search = SearchTextBox.Text.ToLower();

            DataGrid.ItemsSource = Database.Workers.Where(x => x.Name.ToLower() == search).ToList();
            if (MainPage.IsNullOrEmpty(search)) UpdateDataGrid();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Worker currentWorker)
            {
                var page = new WorkersEditPage(Database, currentWorker);
                page.SaveButton.Click += UpdateDataGrid;
                page.SaveButton.Click += BackButtonClick;
                Navigation.Navigate(page);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new WorkersEditPage(Database);
            page.SaveButton.Click += UpdateDataGrid;
            page.SaveButton.Click += BackButtonClick;
            Navigation.Navigate(page);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var toRemove = DataGrid.SelectedItems.Cast<Worker>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {toRemove.Count()} записей?", "Удалить?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Database.Workers.RemoveRange(toRemove);
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
            DataGrid.ItemsSource = Database.Workers.ToList();

            var first = Database.Workers.ToList().FirstOrDefault();
            if (first != null)
            {
                SortComboBox.ItemsSource = first.GetType().GetProperties().ToList().Select(x => x.Name).Where(x => x != "Id" && x != "Workers");
            }
        }
    }
}

