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

        private List<Card> IssuedCards;

        public bool RealizeRequestFromAuthorizationCentre(double amount, string cardId)
        {
            Random rnd = new Random();

            int propability = rnd.Next(1, 100);

            if (propability <= 75)
                return true;
            else
                return false;

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

        public override string ToString()
        {
            return this.Name;
        }

    }
}
