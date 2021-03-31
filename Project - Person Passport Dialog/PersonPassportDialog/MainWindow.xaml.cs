using PersonPassportDialog.Domain;
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

namespace PersonPassportDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                GetPersonFromDb();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }




        }

        private void GetPersonFromDb()
        {
            lvPerson.ItemsSource = Global.ctx.persons.ToList();
        }

        private void MenuAddPerson_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson = new AddPerson();
            bool? result = addPerson.ShowDialog();
            if (result == true)
            {
                GetPersonFromDb();
            }
            

        }

        private void LvPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvPerson.SelectedIndex == -1)
            {
                return;
            }
            Person selectedP = (Person)lvPerson.SelectedItem;
            PassportDialog ppDialog = new PassportDialog(selectedP);
            ppDialog.ShowDialog();
            GetPersonFromDb();
        }
    }
}
