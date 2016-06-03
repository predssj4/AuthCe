using AuthCe.Domain;
using AuthCe.Domain.DataAccessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Test
{
    [TestFixture]
    public class AuthCeTest
    {
        [Test]
        public void RealizeRequest_Test()
        {
            List<Card> cards = new List<Card>()
            {
                new Card{CardId="1111", IssuedBy="pko"},
                new Card{CardId="2222", IssuedBy="millenium"},
                new Card{CardId="3333", IssuedBy="mbank"}
            };

            List<Card> list = cards;
            Bank SelectedBank;
            string cardid="1111";

            var bankName =
                from element in list
                where element.CardId == cardid
                select new Bank
                {
                    Name = element.IssuedBy
                };


            SelectedBank = new Bank(bankName.FirstOrDefault().Name);

            Assert.AreEqual(SelectedBank.Name, "pko");

        }

        [Test]
        public void CheckWeatherAuthCeWorksWithBank_Test()
        {

            DbMenagmentProvider db = new DbMenagmentProvider();

            //List<Bank> bankList = db.ProvideListWithBanks();

            //var bank = 
            //    from element in bankList
            //    where element.Name == "PKO"


            //Assert.AreEqual(answer, true);

        }

        [Test]
        public void Filter_test()
        {
            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Transaction> transactionsList = db.GetTransactionsList();

            var newTransactionsList =
                (from transaction in transactionsList
                where transaction.Amount >= 200 &&
                transaction.Amount <= 300
                select transaction).ToList();

            Assert.AreEqual(newTransactionsList.Count, 2);
        }
    }
}
