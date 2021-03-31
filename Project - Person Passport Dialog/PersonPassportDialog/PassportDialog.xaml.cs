using Microsoft.Win32;
using PersonPassportDialog.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PassportDialog.xaml
    /// </summary>
    public partial class PassportDialog : Window
    {
        Person currentP;
        Passport currentPassport;
        byte[] currentPic;
        public PassportDialog(Person p)
        {
            InitializeComponent();
            if (p != null)
            {
                currentP = p;
                showName.Content = p.Name;
                if (p.Passport != null)
                {
                    currentPassport = p.Passport;
                    txtPassportNo.Text = p.Passport.PassportNo;
                    currentPic = p.Passport.Picture;
                    imageViewer.Source = Utils.ByteArrayToBitmapImage(p.Passport.Picture);
                    btnAddPP.Content = "Update";
                }

            }

        }

        private void BtnAddPP_Click(object sender, RoutedEventArgs e)
        {

            if (IsFieldsValid() != true)
            {
                return;
            }

            if (currentPassport != null)
            {
                try
                {
                    currentPassport.PassportNo = txtPassportNo.Text;
                    currentPassport.Picture = currentPic;
                    Global.ctx.SaveChanges();
                    MessageBox.Show("Passport updated!");
                }
                catch (SystemException exc)
                {
                    // !!!catch exception if duplicated passport number entered
                    MessageBox.Show(exc.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //MessageBox.Show("Passport number has to be unique. The value you entered has already existed in database", "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                try
                {
                    Passport pp = new Passport()
                    {
                        Person = currentP,
                        PassportNo = txtPassportNo.Text,
                        Picture = currentPic
                    };
                    Global.ctx.passports.Add(pp);
                    Global.ctx.SaveChanges();
                    btnAddPP.Content = "Update";
                    MessageBox.Show("Passport saved!");

                }
                catch (SystemException ex)
                {
                    MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
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
                    currentPic = File.ReadAllBytes(dlg.FileName);
                    tbImage.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currentPic); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (Exception exc) when (exc is SystemException || exc is IOException)
                {
                    MessageBox.Show(exc.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }


            }
        }


        private bool IsFieldsValid()
        {
            Regex rx = new Regex(@"^[a-zA-Z]{2}[0-9]{7}$");
            if (!rx.IsMatch(txtPassportNo.Text))
            {
                MessageBox.Show("Passport number must be like AB1234567", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (currentPic == null)
            {
                MessageBox.Show("Please choose a picture", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }



        private void BtnCancelPP_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
