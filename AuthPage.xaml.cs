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
    public partial class AuthPage : Page
    {
        private readonly NavigationService Navigation;
        private readonly Database Database;

        public AuthPage(NavigationService navigation, Database database)
        {
            InitializeComponent();
            Navigation = navigation;
            Database = database;

            LoginComboBox.ItemsSource = Database.Users.ToList();
        }

        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(new RegPage(Navigation, Database));
        }

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            var user = LoginComboBox.SelectedItem as User;
            var password = PasswordBox.Password;

            if (user == null)
            {
                MessageBox.Show("Выберите пользователя");
            }  
            else if (MainPage.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
            }
            else if (Database.Users.Where(u => u.Name.Equals("Кладовщик") && user.Name.Equals("Кладовщик") && u.Password.Equals(password)).Count() > 0)
            {
                Navigation.Navigate(new MainPage(Navigation, Database));
                MessageBox.Show("Вы авторизовались как кладовщик!");
            }
            else if (Database.Users.Where(u => u.Name.Equals("Админ") && user.Name.Equals("Админ") && u.Password.Equals(password)).Count() > 0)
            {
                Navigation.Navigate(new MainWindowForADMIN(Navigation, Database));
                MessageBox.Show("Вы авторизовались как админ!");
            }
            else
            {
                MessageBox.Show("Неверный пароль");
            }
        }
    }
}
