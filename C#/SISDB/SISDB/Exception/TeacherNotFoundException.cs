﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.Exception
{
    public class TeacherNotFoundException :System. Exception
    {
        public TeacherNotFoundException(string message) : base(message) { }
    }
}