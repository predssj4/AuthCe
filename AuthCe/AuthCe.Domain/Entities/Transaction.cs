using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Transaction
    {
        public double Amount { get; set; }
        public string ReciverOfAmount { get; set; }
        public string UsedCardId { get; set; }
        public string DateTime { get; set; }
        public string Currency { get; set; }
        public string IfAccepted { get; set; }
    }
}
