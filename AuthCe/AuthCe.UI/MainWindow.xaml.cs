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
            catch(FormatException)
            {
                AmountTestBox.Text = "To powinna być wartość";
                AmountTestBox.Background = Brushes.OrangeRed;
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd: {0}", exce.Message));
                return;
            }

            try
            {
                id = ulong.Parse(CardIdTextBox.Text);
            }
            catch(FormatException)
            {
                CardIdTextBox.Text = "To powinna być wartość";
                CardIdTextBox.Background = Brushes.OrangeRed;
                return;
            }
            catch (Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd: {0}", exce.Message));
                return;
            }

            
            try
            {
                reciver_shop= ((ListBoxItem)ListWithShopsListBox.SelectedValue).Content.ToString();
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Wybierz miejsce zakupów");
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd: {0}", exce.Message));
                return;
            }

            currency = CurrencyComboBox.Text;

            //realizacja zamówienia
            AuthorizationCentre authCe = new AuthorizationCentre();

            try
            {
                MessageBox.Show((authCe.RealizeRequest(amount, reciver_shop, id.ToString(), currency) ? "Tranzakcja zrealizowana pomyślnie" : "Nie udało się zrealizować tranzakcji" ));
            }
            catch(IndexOutOfRangeException exce)
            {
                MessageBox.Show("Podany numer karty jest nieprawidłowy. {0}", exce.Message);
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

            //walidacja danych
            if(CompanyNameTextBox.Text.Count()!=0)
                companyName = CompanyNameTextBox.Text;
            else
            {
                CompanyNameTextBox.Text = "Okno jest puste";
                CompanyNameTextBox.Background = Brushes.Red;
                return;
            }
                

            if (CompanyTypeComboBox.Text.Count() != 0)
                companyType = CompanyTypeComboBox.Text;
            else
            {
                MessageBox.Show("Nie wybrano typu firmy");
                return;
            }
                

            DbMenagmentProvider db = new DbMenagmentProvider();
            try
            {
                db.AddCompany(new Company(companyName, companyType));
            }
            catch(Exception exce)
            {
                MessageBox.Show("Nieoczekiwany błąd przy dodawaniu nowej firmy: {0}", exce.Message);
            }
            
            this.UpdateListOfCompanies();
            
        }

        private void RemoveCompany(object sender, RoutedEventArgs e)
        {
            string companyToRemove = "";

            try
            {
                companyToRemove = CompaniesListBox.SelectedItem.ToString();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Proszę wybrać któraś z firm do usunięcia");
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd", exce.Message));
                return;
            }


            DbMenagmentProvider db = new DbMenagmentProvider();

            try
            {
                db.RemoveCompany(companyToRemove);
            }
            catch(FileNotFoundException exce)
            {
                MessageBox.Show("Nie można usunąć firmy z listy, ponieważ nie odnaleziono pliku. {0}", exce.Message);
                return;
            }
            
            UpdateListOfCompanies();
        }



        private void AddBank_Click(object sender, RoutedEventArgs e)
        {
            string bank = "";

           
            if(AddBankTextBox.Text.Count() != 0)
                bank = AddBankTextBox.Text;
            else
            {
                AddBankTextBox.Text = "Okno jest puste";
                AddBankTextBox.Background = Brushes.Red;
                return;
            }
                
           
            DbMenagmentProvider db = new DbMenagmentProvider();

            try
            {
                db.AddBank(bank);
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nie można dodać banku: {0}", exce.Message));
                return;
            }

            UpdateListOfBanks();

        }


        private void AddValueToComboBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateListOfCompanies();
        }


        private void RefreshListBoxWithBanks_Click(object sender, RoutedEventArgs e)
        {
            UpdateListOfBanks();
        }

        private void RemoveBankButton_Click(object sender, RoutedEventArgs e)
        {

            string bank = "";

            try
            {
                bank = BanksListBox.SelectedItem.ToString();
            }
                
            catch(NullReferenceException)
            {
                MessageBox.Show("Wybierz bank do usunięcia");
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd podczas usuwania banku", exce.Message));
            }
           
                
            try
            {
                DbMenagmentProvider db = new DbMenagmentProvider();
                db.RemoveBank(bank);
            }
            catch(Exception exce)
            {
                MessageBox.Show("Usunięcie banku z listy nie powiodło się {0}", exce.Message);
            }
            
            UpdateListOfBanks();
 
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
                MessageBox.Show("Nie odnaleziono na dysku pliku z listą tranzakcji. {0}", exce.Message);
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd: {0}", exce.Message));
                return;
            }

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
            List<Transaction> transactionsList;

            try
            {
                transactionsList = db.GetTransactionsList();
            }
            catch(FileNotFoundException exce)
            {
                MessageBox.Show(string.Format("Błąd: {0}", exce.Message.ToString()));
                return;
            }
            catch(Exception exce)
            {
                MessageBox.Show(string.Format("Nieoczekiwany błąd podczas filtrowania listy tranzakcji {0}", exce.Message));
                return;
            }
            

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
            catch(FormatException)
            {
                fromValue = 0;
            }

            try
            {
                toValue = Int32.Parse(MaxValueFilterTextBox.Text);
            }
            catch(FormatException)
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

        private void UpdateListOfCompanies()
        {
           
            DbMenagmentProvider db = new DbMenagmentProvider();

            try
            {
                List<Company> list = db.ProvideListWithCompanies();
                CompaniesListBox.ItemsSource = list;
            }
            catch (FileNotFoundException exce)
            {
                MessageBox.Show("Nie odnaleziono pliku z listą firm. Sprawdź czy plik istnieje. {0}", exce.Message);
            }
            catch (Exception exce)
            {
                MessageBox.Show("Nieoczekiwany błąd przy aktualizowaniu listy firm. {0}", exce.Message);
            }
        }

        private void UpdateListOfBanks()
        {
            DbMenagmentProvider db = new DbMenagmentProvider();

            try
            {
                List<Bank> list = db.ProvideListWithBanks();
                BanksListBox.ItemsSource = list;
            }
            catch(FileNotFoundException exce)
            {
                MessageBox.Show("Nie odnaleziono pliku z listą banków. Sprawdź czy plik istnieje. {0}", exce.Message);
            }
            catch(Exception exce)
            {
                MessageBox.Show("Nieoczekiwany błąd przy aktualizowaniu listy banów. {0}", exce.Message);
            }
            
        }

        private void RefreshShoppingPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            DbMenagmentProvider db = new DbMenagmentProvider();

            try
            {
                List<Company> list = db.ProvideListWithCompanies();
                ListWithShopsListBox.ItemsSource = list;
            }
            catch (FileNotFoundException exce)
            {
                MessageBox.Show("Nie odnaleziono pliku z listą firm. Sprawdź czy plik istnieje. {0}", exce.Message);
            }
            catch (Exception exce)
            {
                MessageBox.Show("Nieoczekiwany błąd przy aktualizowaniu listy firm. {0}", exce.Message);
            }

        }





    }
}
