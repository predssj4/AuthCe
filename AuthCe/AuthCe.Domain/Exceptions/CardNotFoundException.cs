using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain.Exceptions
{
    class CardNotFoundException : Exception
    {
        public CardNotFoundException()
            : base()
        {

        }


        public CardNotFoundException(String message)
            : base(message)
        {

        }

        public CardNotFoundException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
