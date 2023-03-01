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
    public partial class ContestEditPage : Page
    {
        private readonly Database Database;
        private readonly Contest CurrentContest;
        private readonly bool ShouldAdd;

        public ContestEditPage(Database database, Contest currentContest)
        {
            InitializeComponent();
            Database = database;
            CurrentContest = currentContest;
            DataContext = CurrentContest;
            ShouldAdd = false;
        }

        public ContestEditPage(Database database) : this(database, new Contest())
        {
            ShouldAdd = true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainPage.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (MainPage.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Введите описание");
                return;
            }

            if (ShouldAdd) Database.Contests.Add(CurrentContest);
            Database.SaveChanges();
        }
    }
}
