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

        private void GenderFilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid.ItemsSource = Database.Members.Where(x => x.Gender1.Name == GenderFilterComboBox.SelectedItem.ToString()).ToList();
        }

        private void SortMembersComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var members = DataGrid.SelectedItems.Cast<Member>().ToList();

            switch (SortMembersComboBox.SelectedItem.ToString())
            {
                case "Name":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Name);
                    break;
                case "Surname":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Surname);
                    break;
                case "Patronymic":
                    DataGrid.ItemsSource = members.OrderBy(x => x.Patronymic);
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
                DataGrid.ItemsSource = Database.Members.Where(x => x.Name.ToLower() == search).ToList();
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Member currentMember)
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
            var toRemove = DataGrid.SelectedItems.Cast<Member>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {toRemove.Count()} записей?", "Удалить?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Database.Members.RemoveRange(toRemove);
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
            DataGrid.ItemsSource = Database.Members.ToList();
            GenderFilterComboBox.ItemsSource = Database.Genders.ToList().GroupBy(x => x.Name).Select(x => x.Key);

            var first = Database.Members.ToList().FirstOrDefault();
            if (first != null)
            {
                SortMembersComboBox.ItemsSource = first.GetType().GetProperties().ToList().Select(x => x.Name).Where(x => x != "Id" && x != "Gender" && x != "Gender1");
            }
        }
    }
}
