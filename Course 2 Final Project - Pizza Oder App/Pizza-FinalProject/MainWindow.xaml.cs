using Pizza_FinalProject.Domain;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using static Pizza_FinalProject.Domain.Pizza;
using static Pizza_FinalProject.Domain.Payment;
using static Pizza_FinalProject.Domain.Order;

namespace Pizza_FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Item> cart;
        //defining list for partial Totals
        List<double> PTlist = new List<double>();
        double total, orderAmount, subTotal, tips, gst, qst, deliveryFee;
        DeliverMethodEnum delivery;
        public MainWindow()
        {
            InitializeComponent();
            // insert values into pizza table, if data doesn't exist
            // NOT execute if pizza table are already with data
            List<Pizza> pizzaTable = Global.ctx.Pizzas.ToList();
            if (pizzaTable.Count == 0)
            {
                Pizza p = new Pizza();
                p.InsertPizzaTable();
            }
            // load data for combo box options
            DataContext = new ViewModel();
            cart = new List<Item>();
            lvOrderDetail.ItemsSource = cart;
        }

        // *************************1st TAB: Pizza Menu begin**********************************
        private void BtnChe_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidChe()) { return; }
            Pizza pizzaToCart = new Pizza();
            if ((PizzaSizeEnum)cmbCheSize.SelectedItem == PizzaSizeEnum.Small)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Cheese where p.Size == PizzaSizeEnum.Small select p).FirstOrDefault();
            }
            else if ((PizzaSizeEnum)cmbCheSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Cheese where p.Size == PizzaSizeEnum.Medium select p).FirstOrDefault();
            }
            else
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Cheese where p.Size == PizzaSizeEnum.Large select p).FirstOrDefault();
            }
            int qty = (int)cmbCheQty.SelectedItem;
            double pt = CalculateParcialTotal(qty, pizzaToCart.price);
            Item itemToAdd = new Item { Pizza = pizzaToCart, Quantity = qty, PT = pt };
            cart.Add(itemToAdd);
            PTlist.Add(pt);
            RefreshCart();
        }
        private void CmbCheSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PizzaSizeEnum)cmbCheSize.SelectedItem == PizzaSizeEnum.Small)
            {
                lblChePrice.Content = "$9.00";
            }
            else if ((PizzaSizeEnum)cmbCheSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                lblChePrice.Content = "$11.00";
            }
            else
            {
                lblChePrice.Content = "$13.00";
            }
        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidAll()) { return; }
            Pizza pizzaToCart = new Pizza();
            if ((PizzaSizeEnum)cmbAllSize.SelectedItem == PizzaSizeEnum.Small)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.All_Dressed where p.Size == PizzaSizeEnum.Small select p).FirstOrDefault();
            }
            else if ((PizzaSizeEnum)cmbAllSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.All_Dressed where p.Size == PizzaSizeEnum.Medium select p).FirstOrDefault();
            }
            else
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.All_Dressed where p.Size == PizzaSizeEnum.Large select p).FirstOrDefault();
            }
            int qty = (int)cmbAllQty.SelectedItem;
            double pt = CalculateParcialTotal(qty, pizzaToCart.price);
            Item itemToAdd = new Item { Pizza = pizzaToCart, Quantity = qty, PT = pt };
            cart.Add(itemToAdd);
            PTlist.Add(pt);
            RefreshCart();
        }

        private void CmbAllSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PizzaSizeEnum)cmbAllSize.SelectedItem == PizzaSizeEnum.Small)
            {
                lblAllPrice.Content = "$9.00";
            }
            else if ((PizzaSizeEnum)cmbAllSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                lblAllPrice.Content = "$11.00";
            }
            else
            {
                lblAllPrice.Content = "$13.00";
            }
        }

        private void BtnHaw_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidHaw()) { return; }
            Pizza pizzaToCart = new Pizza();
            if ((PizzaSizeEnum)cmbHawSize.SelectedItem == PizzaSizeEnum.Small)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Hawaiian where p.Size == PizzaSizeEnum.Small select p).FirstOrDefault();
            }
            else if ((PizzaSizeEnum)cmbHawSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Hawaiian where p.Size == PizzaSizeEnum.Medium select p).FirstOrDefault();
            }
            else
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Hawaiian where p.Size == PizzaSizeEnum.Large select p).FirstOrDefault();

            }
            int qty = (int)cmbHawQty.SelectedItem;
            double pt = CalculateParcialTotal(qty, pizzaToCart.price);
            Item itemToAdd = new Item { Pizza = pizzaToCart, Quantity = qty, PT = pt };
            cart.Add(itemToAdd);
            PTlist.Add(pt);
            RefreshCart();
        }
        private void CmbHawSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PizzaSizeEnum)cmbHawSize.SelectedItem == PizzaSizeEnum.Small)
            {
                lblHawPrice.Content = "$9.00";
            }
            else if ((PizzaSizeEnum)cmbHawSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                lblHawPrice.Content = "$11.00";
            }
            else
            {
                lblHawPrice.Content = "$13.00";
            }
        }

        private void BtnSal_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidSal()) { return; }
            Pizza pizzaToCart = new Pizza();
            if ((PizzaSizeEnum)cmbSalSize.SelectedItem == PizzaSizeEnum.Small)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Salami where p.Size == PizzaSizeEnum.Small select p).FirstOrDefault();
            }
            else if ((PizzaSizeEnum)cmbSalSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Salami where p.Size == PizzaSizeEnum.Medium select p).FirstOrDefault();

            }
            else
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Salami where p.Size == PizzaSizeEnum.Large select p).FirstOrDefault();

            }
            int qty = (int)cmbSalQty.SelectedItem;
            double pt = CalculateParcialTotal(qty, pizzaToCart.price);
            Item itemToAdd = new Item { Pizza = pizzaToCart, Quantity = qty, PT = pt };
            cart.Add(itemToAdd);
            PTlist.Add(pt);
            RefreshCart();
        }
        private void CmbSalSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PizzaSizeEnum)cmbSalSize.SelectedItem == PizzaSizeEnum.Small)
            {
                lblSalPrice.Content = "$9.00";
            }
            else if ((PizzaSizeEnum)cmbSalSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                lblSalPrice.Content = "$11.00";
            }
            else
            {
                lblSalPrice.Content = "$13.00";
            }
        }

        private void BtnVeg_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidVeg()) { return; }
            Pizza pizzaToCart = new Pizza();
            if ((PizzaSizeEnum)cmbVegSize.SelectedItem == PizzaSizeEnum.Small)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Vegetarian where p.Size == PizzaSizeEnum.Small select p).FirstOrDefault();
            }
            else if ((PizzaSizeEnum)cmbVegSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Vegetarian where p.Size == PizzaSizeEnum.Medium select p).FirstOrDefault();

            }
            else
            {
                pizzaToCart = (from p in Global.ctx.Pizzas where p.PizzaName == PizzaNameEnum.Vegetarian where p.Size == PizzaSizeEnum.Large select p).FirstOrDefault();

            }
            int qty = (int)cmbVegQty.SelectedItem;
            double pt = CalculateParcialTotal(qty, pizzaToCart.price);
            Item itemToAdd = new Item { Pizza = pizzaToCart, Quantity = qty, PT = pt };
            cart.Add(itemToAdd);
            PTlist.Add(pt);
            RefreshCart();
        }

        private void CmbVegSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PizzaSizeEnum)cmbVegSize.SelectedItem == PizzaSizeEnum.Small)
            {
                lblVegPrice.Content = "$9.00";
            }
            else if ((PizzaSizeEnum)cmbVegSize.SelectedItem == PizzaSizeEnum.Medium)
            {
                lblVegPrice.Content = "$11.00";
            }
            else
            {
                lblVegPrice.Content = "$13.00";
            }
        }

        // *************************1st TAB: Pizza Menu end**********************************



        // *************************2nd TAB: Check out begin**********************************
        private void LvOrderDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvOrderDetail.SelectedIndex != -1)
            {
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                cmbItemQty.IsEnabled = true;
                cmbItemSize.IsEnabled = true;
                Item selectedItem = (Item)lvOrderDetail.SelectedItem;
                showSelectedItem.Content = selectedItem.Pizza.PizzaName;
                cmbItemQty.SelectedItem = selectedItem.Quantity;
                cmbItemSize.SelectedItem = selectedItem.Pizza.Size;
            }
            RefreshCart();
        }

        private void RefreshCart()
        {
            lvOrderDetail.Items.Refresh();
            showTotal.Content = CalculateTotal();
            showDeliveryFee.Content = deliveryFee;
            showSubtotal.Content = subTotal;
            showGST.Content = gst;
            showQST.Content = qst;

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int currIndex = lvOrderDetail.SelectedIndex;
            Item currItem = (Item)lvOrderDetail.SelectedItem;
            if (currItem == null) { return; }
            if (MessageBoxResult.Yes != MessageBox.Show("Do you want to delete this item?\n",
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            { return; }
            try
            {
                cart.Remove(currItem);
                PTlist.RemoveAt(currIndex);
                RefreshCart();
                btnDelete.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                cmbItemQty.IsEnabled = false;
                cmbItemQty.SelectedIndex = -1;
                cmbItemSize.IsEnabled = false;
                cmbItemSize.SelectedIndex = -1;
                showSelectedItem.Content = "(No selection)";
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValidUpdateQty()) { return; }

            int currIndex = lvOrderDetail.SelectedIndex;
            Item currItem = (Item)lvOrderDetail.SelectedItem;
            if (currItem == null) { return; }
            try
            {
                PizzaSizeEnum size = (PizzaSizeEnum)cmbItemSize.SelectedItem;
                if (currItem.Pizza.Size != size)
                {
                    currItem.Pizza = (from p in Global.ctx.Pizzas where p.PizzaName == currItem.Pizza.PizzaName where p.Size == size select p).FirstOrDefault();
                }
                int qty = (int)cmbItemQty.SelectedItem;
                currItem.Quantity = qty;
                double pt = CalculateParcialTotal(qty, currItem.Pizza.price);
                currItem.PT = pt;
                PTlist.RemoveAt(currIndex);
                PTlist.Insert(currIndex, pt);
                RefreshCart();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private double GetDeliveryFee()
        {
            if ((DeliverMethodEnum)cmbDeliveryMethod.SelectedItem == DeliverMethodEnum.Delivery)
            {
                return 5.00;
            }
            else { return 0.00; }
        }
        private void CmbDeliveryMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            delivery = (DeliverMethodEnum)cmbDeliveryMethod.SelectedItem;
            if (delivery == DeliverMethodEnum.Delivery)
            {
                showDeliveryFee.Content = "5.00";
            }
            RefreshCart();
        }

        private void BtnAddTips_Click(object sender, RoutedEventArgs e)
        {
            if (IsTipValid())
            {
                tips = Math.Round(tips, 2);
                txtTip.Text = tips.ToString();
                RefreshCart();
            }
        }


        private void BtnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("The cart is empty. Please choose pizza.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client clientToOrder = GetClientInfo();
            if (clientToOrder != null)
            {
                CreateOrder(clientToOrder);
            }
        }


        private void CreateOrder(Client c)
        {
            Order newOrder = new Order();
            newOrder.Client = c;
            List<Item> orderList = new List<Item>();
            if (cart != null)
            {
                foreach (var itemincart in cart)
                {
                    Item i = new Item() { Pizza = itemincart.Pizza, Quantity = itemincart.Quantity, Order = newOrder };
                    orderList.Add(i);
                }
            }
            newOrder.ItemsInOrder = orderList;
            newOrder.Tip = tips;
            newOrder.GST = gst;
            newOrder.QST = qst;
            newOrder.Total = total;
            newOrder.DeliverMethod = delivery;
            newOrder.OrderDate = DateTime.Now;
            newOrder.Status = StatusEnum.Confirmed;
            Global.ctx.Orders.Add(newOrder);
            try
            {
                Global.ctx.SaveChanges();
                MessageBox.Show("Your order is received! Thank you for shopping!");
                cart.Clear();
                RefreshCart();
                ResetForm();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private Client GetClientInfo()
        {
            // check if client exists or not. if not, get client info. if yes, update
            if (IsClientInfoValid() != true)
            {
                return null;
            }
            string name = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            Payment p = new Payment();
            if (rbtnCash.IsChecked == true)
            {
                p.PaymentMethod = PaymentMethodEnum.Cash;
            }
            if (rbtnCredit.IsChecked == true || rbtnDebit.IsChecked == true)
            {
                p.CardHolderName = txtCardName.Text;
                p.CardNumber = txtCardNumber.Text;
                string expStr = txtExp.Text;
                p.Expiration = DateTime.ParseExact(expStr, "MMyy", CultureInfo.InvariantCulture, DateTimeStyles.None).AddMonths(1).AddDays(-1);

                if (rbtnCredit.IsChecked == true)
                {
                    p.PaymentMethod = PaymentMethodEnum.Credit_card;
                }
                else
                {
                    p.PaymentMethod = PaymentMethodEnum.Debit_card;
                }
            }

            // check if client exist or not
            Client existingC = (from client in Global.ctx.Clients where client.Phone == phone select client).FirstOrDefault<Client>();
            if (existingC == null)
            {
                Client c = new Client() { Name = name, Address = address, Phone = phone, Payment = p };
                Global.ctx.Clients.Add(c);
                Global.ctx.SaveChanges();
                return c;
            }
            else
            {
                existingC.Name = name;
                existingC.Address = address;
                existingC.Payment.PaymentMethod = p.PaymentMethod;
                existingC.Payment.CardHolderName = p.CardHolderName;
                existingC.Payment.CardNumber = p.CardNumber;
                existingC.Payment.Expiration = p.Expiration;
                Global.ctx.SaveChanges();
                return existingC;
            }
        }

        private void ResetForm()
        {
            lvOrderDetail.SelectedIndex = -1;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            showDeliveryFee.Content = 0.00;
            cmbDeliveryMethod.SelectedIndex = 0;
            showGST.Content = 0.00;
            showQST.Content = 0.00;
            showSubtotal.Content = 0.00;
            showTotal.Content = 0.00;
            txtTip.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            rbtnCredit.IsChecked = false;
            rbtnDebit.IsChecked = false;
            rbtnCash.IsChecked = false;
            txtCardName.Text = "";
            txtCardNumber.Text = "";
            txtExp.Text = "";
        }
        // *************************2nd TAB: Check out end**********************************


        // **************************3rd TAB: Order tracking begin*********************************
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Regex rxPhone = new Regex(@"^[\s*0-9]{10}$");
            if (!rxPhone.IsMatch(txtByPhone.Text))
            {
                MessageBox.Show("Phone number must be 10 digits without white spaces or dash.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    string searchPhoneStr = txtByPhone.Text;
                    List<Order> foundOrders = (from o in Global.ctx.Orders where o.Client.Phone == txtByPhone.Text select o).OrderByDescending(o => o.OrderDate).ToList<Order>();
                    if (foundOrders.Count == 0)
                    {
                        lblSearchResult.Content = "Sorry. There is no order record matching your phone number: " + searchPhoneStr;
                        txtByPhone.Text = "";
                    }
                    else
                    {
                        lblSearchResult.Content = "Your order history with phone number: " + searchPhoneStr
                                                + "\n\n Order ID                     Date                   Total Amount              Status";
                        lvTrackOrder.ItemsSource = foundOrders;
                        txtByPhone.Text = "";
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return;
                }
            }
        }
        // **************************3rd TAB: Order tracking end*********************************


        // **************************Calculation methods*********************************
        public double CalculateParcialTotal(int Quantity, double price)
        {
            double PT = Quantity * price;
            return PT;
        }

        public double CalculateTotal()
        {
            gst = CalculateGST();
            qst = CalculateQST();
            subTotal = CalculateSubTotal();
            total = subTotal + gst + qst + tips;
            return Math.Round(total, 2);
        }

        public double CalculateSubTotal()
        {
            orderAmount = OrderAmount();
            deliveryFee = GetDeliveryFee();
            subTotal = orderAmount + deliveryFee;
            return subTotal;
        }

        public double OrderAmount()
        {
            orderAmount = 0;
            foreach (double element in PTlist)
            {
                orderAmount += element;
            }
            return orderAmount;
        }

        public double CalculateGST()
        {
            return Math.Round(OrderAmount() * 0.05, 2);
        }

        public double CalculateQST()
        {
            return Math.Round(OrderAmount() * 0.14975, 2);
        }


        // **************************Validation methods*********************************
        public bool IsFieldsValidChe()
        {
            if ((int)cmbCheQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool IsFieldsValidAll()
        {
            if ((int)cmbAllQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool IsFieldsValidHaw()
        {
            if ((int)cmbHawQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool IsFieldsValidSal()
        {
            if ((int)cmbSalQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool IsFieldsValidVeg()
        {
            if ((int)cmbVegQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool IsFieldsValidUpdateQty()
        {
            if ((int)cmbItemQty.SelectedItem == 0)
            {
                MessageBox.Show("You must choose a Quantity", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private bool IsClientInfoValid()
        {
            Regex rxName = new Regex(@"^[\s\S]{2,100}$");
            if (!rxName.IsMatch(txtName.Text))
            {
                MessageBox.Show("Name must be characters between 2 and 100", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            Regex rxAddr = new Regex(@"^[\s\S]{10,100}$");
            if (!rxAddr.IsMatch(txtAddress.Text))
            {
                MessageBox.Show("Address must be characters between 10 and 100", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            Regex rxPhone = new Regex(@"^[\s*0-9]{10}$");
            if (!rxPhone.IsMatch(txtPhone.Text))
            {
                MessageBox.Show("Phone number must be 10 digits without white spaces or dash.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (rbtnCash.IsChecked != true && rbtnCredit.IsChecked != true && rbtnDebit.IsChecked != true)
            {
                MessageBox.Show("Please choose a payment method", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (rbtnCredit.IsChecked == true || rbtnDebit.IsChecked == true)
            {
                if (!rxName.IsMatch(txtCardName.Text))
                {
                    MessageBox.Show("Card holder's name must be characters between 2 and 100", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                Regex rxCard = new Regex(@"^[\s*0-9]{16}$");
                if (!rxCard.IsMatch(txtCardNumber.Text))
                {
                    MessageBox.Show("Card number must 16 digits", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                string expStr = txtExp.Text;
                DateTime dt;
                if (!DateTime.TryParseExact(expStr, "MMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    MessageBox.Show("Invalid expiration value", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else if (DateTime.Compare(dt, DateTime.Now) < 0)
                {
                    MessageBox.Show("Card expired", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }

        private bool IsTipValid()
        {
            if (!double.TryParse(txtTip.Text, out tips))
            {
                MessageBox.Show("Please enter a number for tip.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (tips < 0)
            {
                MessageBox.Show("The tips cannot be negative.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
