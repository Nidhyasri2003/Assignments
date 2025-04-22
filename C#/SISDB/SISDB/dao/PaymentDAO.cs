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
    public class PaymentDAO : IPaymentDAO
    {
        public void RecordPayment(Payment payment)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Payments VALUES (@id, @studentId, @amount, @date)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", payment.PaymentId);
                cmd.Parameters.AddWithValue("@studentId", payment.StudentId);
                cmd.Parameters.AddWithValue("@amount", payment.Amount);
                cmd.Parameters.AddWithValue("@date", payment.PaymentDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Payments";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Payment p = new Payment(
                        (int)reader["payment_id"],
                        (int)reader["student_id"],
                        (decimal)reader["amount"],
                        (DateTime)reader["payment_date"]
                    );
                    payments.Add(p);
                }
            }
            return payments;
        }
    }
}