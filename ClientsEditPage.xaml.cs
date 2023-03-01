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
        private readonly UsedMaterial Member;
        private readonly bool ShouldAdd;

        public MembersEditPage(Database database, UsedMaterial currentMember)
        {
            InitializeComponent();
            Database = database;
            Member = currentMember;
            DataContext = Member;
            ShouldAdd = false;

            WorkerIdComboBox.ItemsSource = Database.Workers.ToList();
            MaterialIdComboBox.ItemsSource = Database.Materials.ToList();
        }

        public MembersEditPage(Database database) : this(database, new UsedMaterial())
        {
            ShouldAdd = true;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (WorkerIdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите Раскройщика!");
                return;
            }
            if (MaterialIdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите Материал!");
                return;
            }
            if (MainPage.IsNullOrEmpty(CountTextBlock.Text))
            {
                MessageBox.Show("Введите Кол-во!");
                return;
            }
            if (DateTextBox.SelectedDate == null)
            {
                MessageBox.Show("Выберите Дату!");
                return;
            }

            var material = MaterialIdComboBox.SelectedItem as Material;
            material.Count -= int.Parse(CountTextBox.Text);
            material.Sum = material.Price * material.Count;

            if (ShouldAdd) Database.UsedMaterials.Add(Member);
            Database.SaveChanges();
        }
    }
}
