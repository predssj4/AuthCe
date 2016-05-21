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
        private Company ReciverOfAmount;
        private Card UsedCard;
        private DateTime DateTime;
        private string currency;

        public Transaction createTransaction()
        {
            return new Transaction();
        }
    }
}
