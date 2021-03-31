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
using System.Windows.Shapes;

namespace PersonPassportDialog
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public AddPerson()
        {
            InitializeComponent();

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValid())
            {
                return;
            }
            string name = txtName.Text;
            int age = int.Parse(txtAge.Text);

            try
            {
                Global.ctx.persons.Add(new Person() { Name = name, Age = age });
                Global.ctx.SaveChanges();
                DialogResult = true;
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsFieldsValid()
        {
            if (txtName.Text.Length < 2)
            {
                MessageBox.Show("Name must be between 2 and 100 characters", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string ageStr = txtAge.Text;
            int age;
            if (!int.TryParse(ageStr, out age))
            {
                MessageBox.Show("Age must be an integer between 0 and 200", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (age < 0 || age > 200)
            {
                MessageBox.Show("Age must be an integer between 0 and 200", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


    }

    
}

