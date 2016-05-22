using AuthCe.Domain.Entities;
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
using AuthCe.Domain;

namespace AuthCe.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            ulong id;
            string shop = "";
            string currency;

            //walidacja danych
            try
            {
                amount = double.Parse(AmountTestBox.Text);
            }
            catch
            {
                AmountTestBox.Text = "To powinna być wartość";
                AmountTestBox.Background = Brushes.OrangeRed;
                return;
            }

            try
            {
                id = ulong.Parse(CardIdTextBox.Text);
            }
            catch
            {
                CardIdTextBox.Text = "To powinna być wartość";
                CardIdTextBox.Background = Brushes.OrangeRed;
                return;
            }
            
            try
            {
                shop = ((ListBoxItem)ListWithShopsListBox.SelectedValue).Content.ToString();
            }
            catch
            {
                MessageBox.Show("Wybierz miejsce zakupów");
            }


            currency = CurrencyComboBox.Text;

            var content = string.Format("Amount: {0} id: {1}, shop: {2}, currency: {3}", amount, id, shop, currency);
            MessageBox.Show(content.ToString());


            AuthorizationCentre authCe = new AuthorizationCentre();
            authCe.RealizeRequest(amount, shop, id, currency);
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

    }
}
