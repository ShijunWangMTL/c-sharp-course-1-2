using CarsOwnersEF.Domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CarsOwnersEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        byte[] currOwnerImage;

        public MainWindow()
        {

            InitializeComponent();
            try
            {
                Global.ctx = new CarOwnerDbContext();
                //carOwnerlist = Global.ctx.carOwners.ToList<CarOwner>();
                //Include: force eager loading - used for collection in one-to-many relationship
                List<CarOwner> carOwnerlist = Global.ctx.carOwners.Include("CarsInGarage").ToList();
                lvGridCarOwner.ItemsSource = carOwnerlist;
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }


        private void BtnAddOwner_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldValid())
            {
                return;
            }

            try
            {
                CarOwner o = new CarOwner { Name = txtName.Text, Photo = currOwnerImage };
                // o.CarsInGarage = new List<Car>(); //error without this line. NullReferenceException: 'Object reference not set to an instance of an object.'
                Global.ctx.carOwners.Add(o);
                Global.ctx.SaveChanges();
                //carOwnerlist.Add(o);
                ResetForm();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void BtnUpdateOwner_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldValid())
            {
                return;
            }

            CarOwner ownerToUpdate = (CarOwner)lvGridCarOwner.SelectedItem;

            try
            {
                ownerToUpdate.Name = txtName.Text;
                ownerToUpdate.Photo = currOwnerImage;
                /*Global.ctx.carOwners.Add(ownerToUpdate);
                Global.ctx.Entry(ownerToUpdate).State = System.Data.Entity.EntityState.Modified;*/
                Global.ctx.SaveChanges();
                // if updated information need to be displayed after clicking update button, then don't clear the form, just reset view.
                ResetForm();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a picture";
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    currOwnerImage = File.ReadAllBytes(dlg.FileName);
                    tbImage.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currOwnerImage); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }


            }
        }


        private void BtnDeleteOwner_Click(object sender, RoutedEventArgs e)
        {

            if (lvGridCarOwner.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a car owner.");
                return;
            }

            CarOwner ownerToDelete = (CarOwner)lvGridCarOwner.SelectedItem;

            // alternative solution for checking if result == MessageBoxResult.Yes
            /*if(MessageBoxResult.Yes != MessageBox.Show("Are you sure to delete this car owner?",
                                                    "Confirmation",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning))
            {
                return;
            }*/

            MessageBoxResult result = MessageBox.Show("Are you sure to delete this car owner?",
                                                    "Confirmation",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Global.ctx.carOwners.Remove(ownerToDelete);
                    Global.ctx.SaveChanges();
                    // carOwnerlist.Remove(ownerToDelete);
                    ResetForm();
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }



        private void ResetForm()
        {
            txtName.Text = "";
            lblIdResult.Content = "";
            imageViewer.Source = null;
            tbImage.Visibility = Visibility.Visible;
            lvGridCarOwner.SelectedIndex = -1;
            btnUpdateOwner.IsEnabled = false;
            btnDeleteOwner.IsEnabled = false;
            btnManage.IsEnabled = false;

            lvGridCarOwner.ItemsSource = Global.ctx.carOwners.Include("CarsInGarage").ToList();
             //lvGridCarOwner.Items.Refresh();
        }

        private void LvGridCarOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdateOwner.IsEnabled = true;
            btnDeleteOwner.IsEnabled = true;
            btnManage.IsEnabled = true;

            var co = (CarOwner)lvGridCarOwner.SelectedItem;
            // validate. when selection changed to ==> index=-1, co = null
            if (co is CarOwner)
            {
                lblIdResult.Content = co.Id;
                txtName.Text = co.Name;
                currOwnerImage = co.Photo;
                imageViewer.Source = Utils.ByteArrayToBitmapImage(co.Photo);
            }


        }

        private void BtnManage_Click(object sender, RoutedEventArgs e)
        {
            if (lvGridCarOwner.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose an owner.");
                return;
            }
            CarOwner coToManage = (CarOwner)lvGridCarOwner.SelectedItem;
            CarsDialog cd = new CarsDialog(coToManage);
            cd.Owner = this;
            bool? result = cd.ShowDialog();
            if (result == true)
            {
                lvGridCarOwner.Items.Refresh();
            }
        }

        public bool IsFieldValid()
        {
            if (txtName.Text.Length < 2)
            {
                MessageBox.Show("Name must be between 2 and 100 characters.");
                return false;
            }
            if (currOwnerImage == null)
            {
                MessageBox.Show("Please choose a picture", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

/*        private void Window_Activated(object sender, EventArgs e)
        {
            lvGridCarOwner.Items.Refresh();
        }*/
    }
}
