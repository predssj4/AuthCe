using AuthCe.Domain.DataAccessLayer;
using AuthCe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthCe.Domain
{
    public class AuthorizationCentre
    {
        private Bank SelectedBank;

        public bool RealizeRequest(double amount, string reciver, string cardid, string currency)
        {
            bool ifAccepted = false;

            try
            {
                this.SelectedBank = new Bank(GetNameOfBankWitchRealizesTranzaction(cardid));
                ifAccepted = SelectedBank.RealizeRequestFromAuthorizationCentre(amount, cardid);
            }
            catch(CardNotFoundException)
            {
                throw new IndexOutOfRangeException();
            }
            catch(Exception)
            {
                throw new Exception();
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

            return ifAccepted;
        }

        private string GetNameOfBankWitchRealizesTranzaction(string cardid)
        {
            DbMenagmentProvider db = new DbMenagmentProvider();
            List<Card> list;

            try
            {
                list = db.IssuedCardByBank();
            }
            catch(FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch
            {
                throw new Exception();
            }

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
            catch(NullReferenceException)
            {
                throw new CardNotFoundException();
            }
        }



        
    }
}
