using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.Exception
{
    public class InvalidEnrollmentDataException : System.Exception
    {
        public InvalidEnrollmentDataException(string message) : base(message) { }
    }
}