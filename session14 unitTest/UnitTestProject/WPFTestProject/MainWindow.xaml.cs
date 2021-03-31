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
using WPFTestProject.Domain;
using WPFTestProject.Service;

namespace WPFTestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICustomerService cService;
        CustomerBusinessService businessService;
        public MainWindow()
        {
            cService = new CustomerService();
            businessService = new CustomerBusinessService(cService);
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            //You call the database Container (ctx)
            //and populate a list and pass this list as an itemsource
            //it is better to put the responsiblity of calling database
            //fetching the data to another class
            //and UI only uses data

            lvCustomer.ItemsSource = cService.GetCustomers();
            //lvCustomer.Items.Refresh();


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtName.Text;
            try
            {
                Customer resCust = cService.AddCustomer(new Customer { Name = customerName });
                LoadData();
            }
            catch (SystemException exc)
            {
                MessageBox.Show("An error happened" + exc.Message);
            }
        }

        private void btnCount_Click(object sender, RoutedEventArgs e)
        {
            int count = businessService.GetCountOfNames(txtSearchCount.Text);
            string msg = $"The number of {txtSearchCount.Text} is {count}";
            MessageBox.Show(msg);

        }
    }
}
