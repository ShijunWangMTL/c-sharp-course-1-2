using CarsOwnersEF.Domain;
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

namespace CarsOwnersEF
{
    /// <summary>
    /// Interaction logic for CarsDialog.xaml
    /// </summary>
    public partial class CarsDialog : Window
    {
      //  List<Car> carlist;
        CarOwner currentOwner;

        public CarsDialog(CarOwner carOwner)
        {

            InitializeComponent();
            // load the car list from database
            currentOwner = carOwner;
            // carlist = (from c in Global.ctx.cars where c.Id == carOwner.Id select c).ToList<Car>();
            lvGridCar.ItemsSource = currentOwner.CarsInGarage.ToList();

            if (carOwner != null)
            {
                lblOwnerNameResult.Content = carOwner.Name;
                txtMakeModel.Text = "";
            }
        }

        private void LvGridCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdateCar.IsEnabled = true;
            btnDeleteCar.IsEnabled = true;

            var selectedCar = lvGridCar.SelectedItem;
            if (selectedCar is Car)
            {
                Car c = (Car)selectedCar;
                txtMakeModel.Text = c.MakeModel;
                lblCarIdResult.Content = c.CarId;
            }
        }
        private void ResetForm()
        {
            lvGridCar.ItemsSource = currentOwner.CarsInGarage.ToList();
            txtMakeModel.Text = "";
            lblCarIdResult.Content = "";
            lvGridCar.SelectedIndex = -1;
            btnUpdateCar.IsEnabled = false;
            btnDeleteCar.IsEnabled = false;
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDialogFieldsValid()) { return; }

            try
            {
                Car c = new Car()
                {
                    Id = currentOwner.Id,
                    MakeModel = txtMakeModel.Text
                };
                Global.ctx.cars.Add(c);
                Global.ctx.SaveChanges();
                // carlist.Add(c); 
                ResetForm();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void BtnUpdateCar_Click(object sender, RoutedEventArgs e)
        {

            if (lvGridCar.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a car.");
                return;
            }

            Car carToUpdate = (Car)lvGridCar.SelectedItem;
            if (carToUpdate != null)
            {
                if (!IsDialogFieldsValid()) { return; }
                try
                {
                    carToUpdate.MakeModel = txtMakeModel.Text;
                    /*Global.ctx.cars.Add(carToUpdate);
                    Global.ctx.Entry(carToUpdate).State = System.Data.Entity.EntityState.Modified;*/
                    Global.ctx.SaveChanges();

                    ResetForm();
                }               
                catch (SystemException ex)
                {
                    MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

        }

        private void BtnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (lvGridCar.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a car.");
                return;
            }

            Car carToDelete = (Car)lvGridCar.SelectedItem;
            if (carToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete this car?",
                                                        "Confirmation",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Global.ctx.cars.Remove(carToDelete);
                    Global.ctx.SaveChanges();
                  //  carlist.Remove(carToDelete);
                }
            }
            ResetForm();
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
           // dialog will be close after clicking DONE

        }


        private bool IsDialogFieldsValid()
        {
            if (txtMakeModel.Text.Length < 1)
            {
                MessageBox.Show("Please, fill in Make & Model", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true; 
        }


    }
}
