using SISDB.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.dao
{
    public interface ITeacherDAO
    {
        void AddTeacher(Teacher teacher);
        List<Teacher> GetAllTeachers();
    }
}