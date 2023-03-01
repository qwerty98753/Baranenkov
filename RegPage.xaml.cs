using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public partial class RegPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public RegPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;
        }

        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var password = PasswordBox.Password;

            if (MainPage.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (MainPage.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (password.Length < 3)
            {
                MessageBox.Show("Пароль должен содержать минимум 3 символа");
                return;
            }

            Database.Users.Add(new User() { Name = name, Password = password });
            Database.SaveChanges();

            Navigation.Navigate(new MainPage(Navigation, Database));
        }
    }
}
