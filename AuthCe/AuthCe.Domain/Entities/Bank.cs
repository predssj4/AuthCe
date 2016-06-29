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

        public Bank(string name)
        {
            this.Name = name;
        }

        public Bank()
        {

        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool RealizeRequestFromAuthorizationCentre(double amount, string cardId)
        {
            Random rnd = new Random();

            int propability = rnd.Next(1, 100);

            if (propability <= 75)
                return true;
            else
                return false;
        }
    }
}
