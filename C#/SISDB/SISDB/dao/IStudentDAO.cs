using SISDB.entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.dao
{
    public interface IStudentDAO
    {
        void AddStudent(Student student);
        List<Student> GetAllStudents();
    }
}