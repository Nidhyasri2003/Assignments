using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.Exception
{
    public class StudentNotFoundException : System.Exception
    {
        public StudentNotFoundException(string message) : base(message) { }
    }
}
