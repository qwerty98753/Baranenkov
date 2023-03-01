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
    public partial class WorkersEditPage : Page
    {
        private readonly Database Database;
        private readonly Worker CurrentContest;
        private readonly bool ShouldAdd;

        public WorkersEditPage(Database database, Worker currentContest)
        {
            InitializeComponent();
            Database = database;
            CurrentContest = currentContest;
            DataContext = CurrentContest;
            ShouldAdd = false;
        }

        public WorkersEditPage(Database database) : this(database, new Worker())
        {
            ShouldAdd = true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainPage.IsNullOrEmpty(SurnameTextBox.Text))
            {
                MessageBox.Show("Введите Фамилию");
                return;
            }
            if (MainPage.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите Имя");
                return;
            }
            if (MainPage.IsNullOrEmpty(PatronymicTextBox.Text))
            {
                MessageBox.Show("Введите Отчество");
                return;
            }
            if (MainPage.IsNullOrEmpty(WorkplaceTextBox.Text))
            {
                MessageBox.Show("Введите Рабочее место");
                return;
            }

            if (ShouldAdd) Database.Workers.Add(CurrentContest);
            Database.SaveChanges();
        }
    }
}
