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
    public partial class MaterialEditPage : Page
    {
        private readonly Database Database;
        private readonly Material CurrentContest;
        private readonly bool ShouldAdd;

        public MaterialEditPage(Database database, Material currentContest)
        {
            InitializeComponent();
            Database = database;
            CurrentContest = currentContest;
            DataContext = CurrentContest;
            ShouldAdd = false;
        }

        public MaterialEditPage(Database database) : this(database, new Material())
        {
            ShouldAdd = true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainPage.IsNullOrEmpty(NakladnayaNumberIDTextBox.Text))
            {
                MessageBox.Show("Введите номер накладной");
                return;
            }
            if (MainPage.IsNullOrEmpty(DateNumberTextBox.Text))
            {
                MessageBox.Show("Укажите дату");
                return;
            }
            if (MainPage.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (MainPage.IsNullOrEmpty(CountTextBox.Text))
            {
                MessageBox.Show("Введите количество");
                return;
            }
            if (MainPage.IsNullOrEmpty(PriceTextBox.Text))
            {
                MessageBox.Show("Введите цену");
                return;
            }

            CurrentContest.Sum = CurrentContest.Price * CurrentContest.Count;

            if (ShouldAdd) Database.Materials.Add(CurrentContest);
            Database.SaveChanges();
        }
    }
}
