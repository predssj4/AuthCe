using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Transaction
    {
        private ulong TransactionId;
        private double Amount;
        private string ReciverOfAmount;
        private ulong UsedCard;
        private DateTime DateTime;
        private string Currency;

        public Transaction (double amount, string reciver, ulong cardId, string currency)
        {
            this.Amount = amount;
            this.ReciverOfAmount = reciver;
            this.UsedCard = cardId;
            this.DateTime = DateTime.Now;
            this.Currency = currency;

        }
    }
}
