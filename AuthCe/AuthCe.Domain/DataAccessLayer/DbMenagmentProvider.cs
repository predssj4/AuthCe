using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            catch
            {

               xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("ListaFirm", new XAttribute("xmlns",""))
                    );
            }


            XElement root = new XElement("Company");
            root.Add(new XElement("Name", company.Name));
            root.Add(new XElement("CompanyType", company.CompanyType));
            xml.Element("ListaFirm").Add(root);

            xml.Save("Companies.xml");
        }

        public void RemoveCompany(Company company)
        {
            XDocument xDocument = XDocument.Load("Companies.xml");
            foreach (var element in xDocument.Descendants("Company")  // Iterates through the collection of "Profile" elements
                                                    .ToList())               // Copies the list (it's needed because we modify it in the foreach (when the element is removed)
            {
                if (element.Element("Name").Value == company.Name)   // Checks the name of the profile
                {
                    element.Remove();                                 // Removes the element
                }
            }
            xDocument.Save("Companies.xml");
        }

        public void AddBank(Bank bank)
        {

            XDocument xml;
            try
            {
                xml = XDocument.Load("Banks.xml");
            }
            catch
            {

                xml = new XDocument(
                     new XDeclaration("1.0", "utf-8", "yes"),
                     new XElement("BanksList", new XAttribute("xmlns", ""))
                     );
            }

            XElement root = new XElement("Company");
            root.Add(new XElement("Name", bank.Name));
            xml.Element("BanksList").Add(root);

            xml.Save("Banks.xml");
        }

        public void RemoveBank(Bank bank)
        {
            XDocument xDocument = XDocument.Load("Banks.xml");
            foreach (var element in xDocument.Descendants("Banks") 
                                                    .ToList())              
            {
                if (element.Element("Name").Value == bank.Name)   
                {
                    element.Remove();                                 
                }
            }
            xDocument.Save("Banks.xml");
        }

        public List<Company> ProvideListWithCompanies()
        {
            XDocument xml = XDocument.Load("Companies.xml");

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

        //public void AddNewCardToDb(Card card)
        //{

        //}

        //public List<Bank> GetListOfBanks()
        //{
        //    ;
        //}

        //public List<Company> GetListOfCompanies()
        //{
        //    ;
        //}

        public void SaveTransactionToDataBase(Transaction transaction, bool StatusOfSuccess)
        {

        }

        //public AuthorizationCentre AuthCentreContentProvider(string shop)
        //{
        //    //shop bedzie informowal o tym z jakiego centrum skorzystac przy wyborze jakiego sklepu

        //    throw new NotImplementedException();
        //}
    }
}
