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
    public partial class MembersEditPage : Page
    {
        private readonly Database Database;
        private readonly Member Member;
        private readonly bool ShouldAdd;

        public MembersEditPage(Database database, Member currentMember)
        {
            InitializeComponent();
            Database = database;
            Member = currentMember;
            DataContext = Member;
            ShouldAdd = false;

            GenderComboBox.ItemsSource = Database.Genders.ToList();
        }

        public MembersEditPage(Database database) : this(database, new Member())
        {
            ShouldAdd = true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainPage.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (MainPage.IsNullOrEmpty(SurnameTextBox.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if (MainPage.IsNullOrEmpty(PatronymicTextBlock.Text))
            {
                MessageBox.Show("Введите отчество");
                return;
            }
            if (GenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Введите пол");
                return;
            }

            if (ShouldAdd) Database.Members.Add(Member);
            Database.SaveChanges();
        }
    }
}
