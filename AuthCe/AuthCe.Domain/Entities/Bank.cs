using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Bank
    {
        public string Name { get; set; }

        private List<PrivateClient> ClientsOfBank;
        private List<Card> IssuedCards;

        public bool RealizeRequestFromAuthorizationCentre(Card card)
        {
            return true;
        }

        public Bank(string name)
        {
            this.Name = name;
        }

        public Bank()
        {

        }

        public bool IssueANewCard()
        {
            return true;
        }

        public bool AddANewPrivateClient(PrivateClient privateClient)
        {
            return true;
        }
    }
}
