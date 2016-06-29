using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain
{
    public class Company
    {
        public string Name { get; set; }
        public string CompanyType { get; set; }

        public Company(string name, string type)
        {
            this.Name = name;
            this.CompanyType = type;
        }

        public Company()
        {

        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
