using AuthCe.Domain.DataAccessLayer;
using AuthCe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthCe.Domain
{
    public class AuthorizationCentre
    {
        private List<Company> CompaniesUsingThisAuthCentre = new List<Company>();
        //private List<Bank> BanksCooperatingWithThisAuthCentre = new List<Bank>();

        private Bank SelectedBank;

        public bool RealizeRequest(double amount, string reciver, string cardid, string currency)
        {
            bool ifAccepted = false;

            try
            {
                this.SelectedBank = new Bank(GetNameOfBankWitchRealizesTranzaction(cardid));
                ifAccepted = SelectedBank.RealizeRequestFromAuthorizationCentre(amount, cardid);
            }
            catch
            {
                throw new IndexOutOfRangeException();
            }

            try
            {
                DbMenagmentProvider db = new DbMenagmentProvider();
                db.SaveTransactionToDataBase(new Transaction(){
                    Amount=amount,
                    Currency=currency,
                    DateTime= DateTime.Now.Date.ToShortDateString(),
                    ReciverOfAmount = reciver,
                    UsedCardId = cardid,
                    IfAccepted = ifAccepted ? "Accepted" : "Not accepted"
                });
            }
            catch
            {
                throw new DataBaseSavingException();
                
            }

            return ifAccepted? true : false;
        }

        private string GetNameOfBankWitchRealizesTranzaction(string cardid)
        {
            DbMenagmentProvider db = new DbMenagmentProvider();

            List<Card> list = db.IssuedCardByBank();

            var bankName =
                from element in list
                where element.CardId == cardid
                select new Bank
                {
                    Name = element.IssuedBy
                };

            try
            {
                return bankName.FirstOrDefault().Name;
            }
            catch
            {
                throw new IndexOutOfRangeException();
            }
        }



        
    }
}
