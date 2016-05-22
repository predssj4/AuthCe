using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Bank
    {
        private string Name;
        private List<PrivateClient> ClientsOfBank;
        private List<Card> IssuedCards;

        public bool RealizeRequestFromAuthorizationCentre(Card card)
        {
            return true;
        }

        Bank(string name)
        {
            this.Name = name;
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
