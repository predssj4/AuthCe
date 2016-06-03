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
using AuthCe.Domain.DataAccessLayer;
using System.IO;

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
            string reciver_shop="";
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
                reciver_shop= ((ListBoxItem)ListWithShopsListBox.SelectedValue).Content.ToString();
            }
            catch
            {
                MessageBox.Show("Wybierz miejsce zakupów");
            }

            currency = CurrencyComboBox.Text;

            //var content = string.Format("Amount: {0} id: {1}, shop: {2}, currency: {3}", amount, id, reciver_shop, currency);
            //MessageBox.Show(content.ToString());


            //realizacja zamówienia
            AuthorizationCentre authCe = new AuthorizationCentre();

            try
            {
                MessageBox.Show((authCe.RealizeRequest(amount, reciver_shop, id.ToString(), currency) ? "Tranzakcja zrealizowana pomyślnie" : "Nie udało się zrealizować tranzakcji" ));
            }
            catch(IndexOutOfRangeException exce)
            {
                MessageBox.Show("Podany numer karty jest nieprawidłowy");
            }
            catch(Exception exce)
            {
                MessageBox.Show("Nieoczekiwany błąd. {0}", exce.Message);
            }

        }


        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {

            string companyName = "";
            string companyType = "";

            try
            {
                companyName = CompanyNameTextBox.Text;
            }
            catch
            {
                CompanyNameTextBox.Text = "Okno jest puste";
                CompanyNameTextBox.Background = Brushes.Red;
            }

            try
            {
                companyType = CompanyTypeComboBox.Text;
            }
            catch
            {
                MessageBox.Show("Proszę wybrać typ firmy");
            }

            DbMenagmentProvider db = new DbMenagmentProvider();
            db.AddCompany(new Company(companyName, companyType));

            List<Company> list = db.ProvideListWithCompanies();
            CompaniesListBox.ItemsSource = list;
        }

        private void RemoveCompany(object sender, RoutedEventArgs e)
        {
            string companyToRemove = "";

            try
            {
                companyToRemove = CompaniesListBox.SelectedItem.ToString();
            }

            catch
            {
                MessageBox.Show("Proszę wybrać któraś z firm do usunięcia");
            }


            DbMenagmentProvider db = new DbMenagmentProvider();
            db.RemoveCompany(companyToRemove);

            //odswiezanie
            List<Company> list = db.ProvideListWithCompanies();
            CompaniesListBox.ItemsSource = list;
        }



        private void AddBank_Click(object sender, RoutedEventArgs e)
        {
            string bank = "";

            try
            {
                bank = AddBankTextBox.Text;
            }
            catch
            {
                MessageBox.Show("Proszę wprowadzić nazwę banku");
            }

            DbMenagmentProvider db = new DbMenagmentProvider();
            db.AddBank(bank);
            List<Bank> list = db.ProvideListWithBanks();
            BanksListBox.ItemsSource = list;

        }


        private void AddValueToComboBox_Click(object sender, RoutedEventArgs e)
        {

            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Company> list = db.ProvideListWithCompanies();
            CompaniesListBox.ItemsSource = list;

        }


        private void RefreshListBoxWithBanks_Click(object sender, RoutedEventArgs e)
        {
            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Bank> list = db.ProvideListWithBanks();
            BanksListBox.ItemsSource = list;
        }

        private void RemoveBankButton_Click(object sender, RoutedEventArgs e)
        {

            string bank = "";

            try
            {
                bank = BanksListBox.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Wybierz bank do usunięcia");
            }

            DbMenagmentProvider db = new DbMenagmentProvider();
            db.RemoveBank(bank);

            //odswiezania po usunieciu
            List<Bank> list = db.ProvideListWithBanks();
            BanksListBox.ItemsSource = list;

        }

        private void TestKalendarz_Click(object sender, RoutedEventArgs e)
        {
            if(DayOfTransactionCalendar.SelectedDate.HasValue)
            {
                MessageBox.Show(DayOfTransactionCalendar.SelectedDate.Value.ToShortDateString());
            }
                
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            TransactionsListView.Items.Clear();
            

            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Transaction> transactionsList;

            try
            {
                transactionsList = db.GetTransactionsList();
            }
            catch (FileNotFoundException exce)
            {
                MessageBox.Show("Nie odnaleziono na dysku pliku z listą tranzakcji");
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd: {0}", exce.Message));
                return;
            }


            //MessageBox.Show(string.Format("od {0} do {1}",  Int32.Parse(MinValueFilterTextBox.Text).ToString(), Int32.Parse(MaxValueFilterTextBox.Text).ToString()));

            //MessageBox.Show(newTransactionsList.ToList().Count.ToString());

            //var newTransactionsList = transactionsList.Where(x => x.Amount >= Int32.Parse(MinValueFilterTextBox.Text)).ToList();

            foreach (var item in transactionsList)
            {
                this.TransactionsListView.Items.Add(item);
            }

            LoadTransactionsListButton.Content = "Odśwież";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            TransactionsListView.Items.Clear();

            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Transaction> transactionsList = db.GetTransactionsList();


            string currencyValue = CurrencyFilterComboBox.Text;
            int fromValue=0, toValue=Int32.MaxValue;
            string date = "";
            string status;
            string reciver = "";
            string cardId = "";
            string currency = "";

            //obsługa zakresu tranzakcji
            try
            {
                fromValue = Int32.Parse(MinValueFilterTextBox.Text);
            }
            catch
            {
                fromValue = 0;
            }

            try
            {
                toValue = Int32.Parse(MaxValueFilterTextBox.Text);
            }
            catch
            {
                toValue = Int32.MaxValue;
            }
            transactionsList = transactionsList.Where(x => x.Amount >= fromValue && x.Amount <= toValue).ToList();


            //obsluga kalendarza
            if (DayOfTransactionCalendar.SelectedDate.HasValue)
            {
                date = DayOfTransactionCalendar.SelectedDate.Value.ToShortDateString();
                transactionsList = transactionsList.Where(x => x.DateTime == date).ToList();  
            }

            //obsługa zaakceptowanie/niezaakceptowane
            if(StatusCheckBox.IsChecked.Value)
            {
                status = "Accepted";
                //MessageBox.Show("checked");
                transactionsList = transactionsList.Where(x => x.IfAccepted == status).ToList();
                
            }

            //obsługa obiorcy
            if(ReciverFilterTextBox.Text != reciver)
            {
                reciver = ReciverFilterTextBox.Text;
                transactionsList = transactionsList.Where(x => x.ReciverOfAmount == reciver).ToList();
            }

            //obsługa nr id karty
            if (CardIdFilterTextBox.Text != cardId)
            {
                cardId = CardIdFilterTextBox.Text;
                transactionsList = transactionsList.Where(x => x.UsedCardId == cardId).ToList();
            }
            
            //obsługa waluty
            if(CurrencyFilterComboBox.Text != "Dowolna")
            {
                currency = CurrencyFilterComboBox.Text;
                transactionsList = transactionsList.Where(x => x.Currency == currency).ToList();
            }


            
            
            foreach (var item in transactionsList)
            {
                this.TransactionsListView.Items.Add(item);
            }
        }

        private void DateFreeChoiceCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (DayOfTransactionCalendar.SelectedDate.HasValue)
                DayOfTransactionCalendar.SelectedDates.Remove(DayOfTransactionCalendar.SelectedDate.Value);
        }

        private void DayOfTransactionCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFreeChoiceCheckBox.IsChecked = false;
        }

        






    }
}
