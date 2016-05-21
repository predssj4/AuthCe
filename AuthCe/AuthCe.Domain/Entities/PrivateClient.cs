using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class PrivateClient
    {
        private string Name;
        private string LastName;
        private List<Card> PossessionedCards;

        public PrivateClient(string name, string lastName)
        {
            this.Name = name;
            this.LastName = lastName;
        }

        public void AddCarForClient(Card card)
        {

        }
    }
}
