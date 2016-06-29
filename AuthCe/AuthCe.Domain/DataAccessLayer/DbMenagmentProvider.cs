using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using AuthCe.Domain.Exceptions;

namespace AuthCe.Domain.DataAccessLayer
{
    public class DbMenagmentProvider
    {
        public void AddCompany(Company company)
        {
            XDocument xml;
            try
            {
                xml = XDocument.Load("Companies.xml");
            }
            catch(FileNotFoundException)
            {

               xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("ListaFirm", new XAttribute("xmlns",""))
                    );
            }
            catch(Exception)
            {
                throw new Exception();
            }

            XElement root = new XElement("Company");
            root.Add(new XElement("Name", company.Name));
            root.Add(new XElement("CompanyType", company.CompanyType));
            xml.Element("ListaFirm").Add(root);

            xml.Save("Companies.xml");
        }

        public void RemoveCompany(string companyName)
        {
            XDocument xDocument;

            try
            {
                xDocument = XDocument.Load("Companies.xml");
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            
            foreach (var element in xDocument.Descendants("Company")  
                                                    .ToList())        
            {
                if (element.Element("Name").Value == companyName)   
                {
                    element.Remove();                                 
                }
            }
            xDocument.Save("Companies.xml");
        }

        public void AddBank(string bankName)
        {

            XDocument xml;

            try
            {
                xml = XDocument.Load("Banks.xml");
            }
            catch (FileNotFoundException)
            {

                xml = new XDocument(
                     new XDeclaration("1.0", "utf-8", "yes"),
                     new XElement("BanksList", new XAttribute("xmlns", ""))
                     );
            }
            catch(Exception)
            {
                throw new Exception();
            }

            XElement root = new XElement("Bank");
            root.Add(new XElement("Name", bankName));
            xml.Element("BanksList").Add(root);

            xml.Save("Banks.xml");
        }


        public void RemoveBank(string bankName)
        {
            XDocument xDocument;

            try
            {
                xDocument = XDocument.Load("Banks.xml");
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
                

            foreach (var element in xDocument.Descendants("Bank").ToList())              
            {
                if (element.Element("Name").Value == bankName)   
                {
                    element.Remove();                                 
                }
            }
            xDocument.Save("Banks.xml");
        }


        public List<Company> ProvideListWithCompanies()
        {
            XDocument xml;

            try
            {
                xml = XDocument.Load("Companies.xml");
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
            

            IEnumerable<Company> list = new List<Company>();

            list =
                 from company in xml.Descendants("Company")
                 select new Company
                 {
                     CompanyType = company.Element("CompanyType").Value,
                     Name = company.Element("Name").Value
                 };

            return list.ToList();
        }
       
        public List<Bank>  ProvideListWithBanks()
        {
            XDocument xml;

            try
            {
                xml = XDocument.Load("Banks.xml");
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch(Exception)
            {
                throw new Exception();
            }
            

            IEnumerable<Bank> list = new List<Bank>();

            list =
                 from bank in xml.Descendants("Bank")
                 select new Bank
                 {
                     Name = bank.Element("Name").Value
                 };

            return list.ToList();
            
        }

        public void SaveTransactionToDataBase(Transaction transaction)
        {
            XDocument xml;
            try
            {
                xml = XDocument.Load("Transactions.xml");
            }
            catch(FileNotFoundException)
            {

                xml = new XDocument(
                     new XDeclaration("1.0", "utf-8", "yes"),
                     new XElement("TransactionsList", new XAttribute("xmlns", ""))
                     );
            }
            catch
            {
                throw new Exception();
            }


            XElement root = new XElement("Transaction");
            root.Add(new XElement("CardId", transaction.UsedCardId));
            root.Add(new XElement("Status", transaction.IfAccepted));
            root.Add(new XElement("Reciver", transaction.ReciverOfAmount));

            //eleminuje bląd przecinek/kropka
            //domyślnie jest zapisywane jako kropka -> wymagana zmiana na przecinek aby pózniej było można wczytać na double
            root.Add(new XElement("Amount", transaction.Amount.ToString().Replace(".",",")));
            root.Add(new XElement("DateTime", transaction.DateTime));
            root.Add(new XElement("Currency", transaction.Currency));
            xml.Element("TransactionsList").Add(root);

            xml.Save("Transactions.xml");
        }


        public List<Card> IssuedCardByBank()
        {
            XDocument xml;
            try
            {
                xml = XDocument.Load("IssuedCards.xml");
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch
            {
                throw new Exception();
            }
                
            IEnumerable<Card> list = new List<Card>();

            list =
                 from card in xml.Descendants("Card")
                 select new Card
                 {
                     CardId = card.Element("CardId").Value,
                     IssuedBy = card.Element("IssuedBy").Value
                 };

            return list.ToList();
        }

        public List<Transaction> GetTransactionsList()
        {
            XDocument xml = new XDocument();

            try
            {
                xml = XDocument.Load("Transactions.xml");
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch
            {
                throw new Exception();
            }


            IEnumerable<Transaction> list = new List<Transaction>();

            list =
                 from trans in xml.Descendants("Transaction")
                 select new Transaction
                 {
                     UsedCardId=trans.Element("CardId").Value,
                     ReciverOfAmount = trans.Element("Reciver").Value,
                     IfAccepted = trans.Element("Status").Value,
                     DateTime = trans.Element("DateTime").Value,
                     Currency = trans.Element("Currency").Value,
                     Amount = double.Parse(trans.Element("Amount").Value)
                 };

            return list.ToList();
        }
    }
}
