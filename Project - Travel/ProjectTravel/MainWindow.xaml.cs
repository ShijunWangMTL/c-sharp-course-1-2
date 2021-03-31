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

namespace ProjectTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string DataFileName = @"..\..\trips.txt";

        List<Trip> trips = new List<Trip>();

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile();

        }

        // ----------- methods for loading and saving/exporting data --------------
        private void LoadDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                string[] tripsTxt = File.ReadAllLines(DataFileName);

                foreach (string tripline in tripsTxt)
                {
                    try
                    {
                        Trip t = new Trip(tripline);
                        trips.Add(t);
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }

                lvTravel.ItemsSource = trips;

            }
        }

        private void BtnSaveSelectedExport_Click(object sender, RoutedEventArgs e)
        {
            var tripsSelected = lvTravel.SelectedItems;
            if (tripsSelected.Count == 0)
            {
                MessageBox.Show("Please choose the data you want to export.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "trips files (*.trips)|*.trips|All files (*.*)|*.*";
            saveFileDialog.Title = "Save all the trips in a file";

            if (saveFileDialog.ShowDialog() == true)
            {

                string allData = "";

                foreach (Trip t in tripsSelected)
                {
                    allData += t.ToString() + "\n";
                }
                File.WriteAllText(saveFileDialog.FileName, allData);
                ResetView();
            }
            else
            {
                return;
            }

        }


        // ----------- methods for buttons: add, delete, update --------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtDestination.Text.Equals(""))
            {
                MessageBox.Show("Please enter the destination of the trip.");
                return;
            }

            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("Please enter the name of the travller.");
                return;
            }

            if (txtPassport.Text.Equals(""))
            {
                MessageBox.Show("Please enter the passport number of the travller.");
                return;
            }

            DateTime? dateDepart;
            dateDepart = dpDepart.SelectedDate;
            if (dateDepart == null)
            {
                MessageBox.Show("Please choose a departure date.");
                return;
            }

            DateTime? dateReturn;
            dateReturn = dpReturn.SelectedDate;
            if (dateReturn == null)
            {
                MessageBox.Show("Please choose a return date.");
                return;
            }

            try
            {
                Trip tAdd = new Trip(txtDestination.Text, txtName.Text, txtPassport.Text, dateDepart.Value.ToString("yyyy-MM-dd"), dateReturn.Value.ToString("yyyy-MM-dd"));
                trips.Add(tAdd);
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }


            ResetView();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravel.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a trip.");
                return;
            }

            Trip tDelete = (Trip)lvTravel.SelectedItem;
            if (tDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete this trip?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    trips.Remove(tDelete);
                }
            }

            ResetView();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravel.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a trip.");
                return;
            }

            Trip tUpdate = (Trip)lvTravel.SelectedItem;

            if (txtDestination.Text.Equals(""))
            {
                MessageBox.Show("Please enter the destination of the trip.");
                return;
            }
            try
            {
                tUpdate.Destination = txtDestination.Text;
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }

            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("Please enter the name of the travller.");
                return;
            }
            try
            {
                tUpdate.Name = txtName.Text;
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }

            if (txtPassport.Text.Equals(""))
            {
                MessageBox.Show("Please enter the passport number of the travller.");
                return;
            }
            try
            {
                tUpdate.Passport = txtPassport.Text;
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }

            DateTime? selectedDate;
            selectedDate = dpDepart.SelectedDate;
            if (selectedDate == null)
            {
                MessageBox.Show("Please choose a departure date.");
                return;
            }
            try
            {
                tUpdate.Departure = selectedDate.Value.ToString("yyyy-MM-dd");
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }

            selectedDate = dpReturn.SelectedDate;
            if (selectedDate == null)
            {
                MessageBox.Show("Please choose a return date.");
                return;
            }
            try
            {
                tUpdate.ReturnDate = selectedDate.Value.ToString("yyyy-MM-dd");
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message);
            }

            ResetView();
        }



        // ----------- methods for select and reset --------------
        private void lvTravel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;

            var selectedTrip = lvTravel.SelectedItem;
            if (selectedTrip is Trip)
            {
                Trip t = (Trip)selectedTrip;
                txtDestination.Text = t.Destination;
                txtName.Text = t.Name;
                txtPassport.Text = t.Passport;
                dpDepart.SelectedDate = DateTime.Parse(t.Departure);
                dpReturn.SelectedDate = DateTime.Parse(t.ReturnDate);
            }

        }

        private void ResetView()
        {
            lvTravel.Items.Refresh();

            // clear the GUI form content
            txtDestination.Clear();
            txtName.Clear();
            txtPassport.Clear();
            dpDepart.SelectedDate = null;
            dpReturn.SelectedDate = null;

            // clear the selection
            lvTravel.SelectedIndex = -1;

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // save data back into the original txt file
            try
            {
                using (StreamWriter writer = new StreamWriter(DataFileName))
                {
                    foreach (Trip t in trips)
                    {
                        writer.WriteLine(t.ToString());
                    }
                }
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message); ;
            }
        }
    }
}
