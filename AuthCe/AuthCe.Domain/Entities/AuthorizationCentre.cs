using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthCe.Domain
{
    public class AuthorizationCentre
    {
        private List<Company> CompaniesUsingThisAuthCentre = new List<Company>();
        private List<Bank> BanksCooperatingWithThisAuthCentre = new List<Bank>();

        public bool RealizeRequest(double amount, string reciver, ulong cardId, string currency)
        {


            return true;
        }

        
    }
}
