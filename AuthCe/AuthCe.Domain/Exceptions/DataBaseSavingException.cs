using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCe.Domain.Exceptions
{
    class DataBaseSavingException : Exception
    {
        public DataBaseSavingException()
            : base()
        {

        }


        public DataBaseSavingException(String message)
            : base(message)
        {

        }

        public DataBaseSavingException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
