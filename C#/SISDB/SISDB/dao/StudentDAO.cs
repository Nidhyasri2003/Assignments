using SISDB.entity;
using SISDB.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.dao
{
    public class StudentDAO : IStudentDAO
    {
        public void AddStudent(Student student)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Students VALUES (@id, @fname, @lname, @dob, @email, @phone)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", student.StudentId);
                cmd.Parameters.AddWithValue("@fname", student.FirstName);
                cmd.Parameters.AddWithValue("@lname", student.LastName);
                cmd.Parameters.AddWithValue("@dob", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@phone", student.PhoneNumber);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student(
                        (int)reader["student_id"],
                        reader["first_name"].ToString(),
                        reader["last_name"].ToString(),
                        (DateTime)reader["date_of_birth"],
                        reader["email"].ToString(),
                        reader["phone_number"].ToString()
                    );
                    students.Add(s);
                }
            }
            return students;
        }
    }
}