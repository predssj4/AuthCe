using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Company
    {
        private string Name;
        private string CompanyType;

        public Company(string name, string type)
        {
            this.Name = name;
            this.CompanyType = type;
        }

        public bool SendConfirmationRequest(Card card)
        {
            return true;
        }
    }
}
