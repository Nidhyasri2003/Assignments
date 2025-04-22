using SISDB.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.dao
{
    public interface IEnrollmentDAO
    {
        void EnrollStudent(Enrollment enrollment);
        List<Enrollment> GetAllEnrollments();
    }
}
