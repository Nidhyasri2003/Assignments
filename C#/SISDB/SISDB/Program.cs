
    using System;
   
    using SISDB.entity;
using SISDB.dao;
using SISProject.dao;

namespace SISDB.main
    {
        class Program
        {
            static void Main(string[] args)
            {
                IStudentDAO studentDAO = new StudentDAO();
                ITeacherDAO teacherDAO = new TeacherDAO();
                ICourseDAO courseDAO = new CourseDAO();
                IEnrollmentDAO enrollmentDAO = new EnrollmentDAO();
                IPaymentDAO paymentDAO = new PaymentDAO();

                while (true)
                {
                    Console.WriteLine("\n========== Student Information System ==========");
                    Console.WriteLine("1. Add Student");
                    Console.WriteLine("2. Add Teacher");
                    Console.WriteLine("3. Add Course");
                    Console.WriteLine("4. Enroll Student to Course");
                    Console.WriteLine("5. Record Payment");
                    Console.WriteLine("6. View All Students");
                    Console.WriteLine("7. View All Teachers");
                    Console.WriteLine("8. View All Courses");
                    Console.WriteLine("9. View All Enrollments");
                    Console.WriteLine("10. View All Payments");
                    Console.WriteLine("11. Exit");
                    Console.WriteLine("12. Assign Teacher to an Existing Course");
                    Console.WriteLine("13. Generate Enrollment Report by Course Name");
                    Console.Write("Enter choice: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Student ID: ");
                                int sid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("First Name: ");
                                string fname = Console.ReadLine();
                                Console.Write("Last Name: ");
                                string lname = Console.ReadLine();
                                Console.Write("DOB (yyyy-mm-dd): ");
                                DateTime dob = Convert.ToDateTime(Console.ReadLine());
                                Console.Write("Email: ");
                                string email = Console.ReadLine();
                                Console.Write("Phone: ");
                                string phone = Console.ReadLine();

                                studentDAO.AddStudent(new Student(sid, fname, lname, dob, email, phone));
                                Console.WriteLine("✅ Student added!");
                                break;

                            case 2:
                                Console.Write("Teacher ID: ");
                                int tid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("First Name: ");
                                string tFname = Console.ReadLine();
                                Console.Write("Last Name: ");
                                string tLname = Console.ReadLine();
                                Console.Write("Email: ");
                                string tEmail = Console.ReadLine();

                                teacherDAO.AddTeacher(new Teacher(tid, tFname, tLname, tEmail));
                                Console.WriteLine("✅ Teacher added!");
                                break;

                            case 3:
                                Console.Write("Course ID: ");
                                int cid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Course Name: ");
                                string cname = Console.ReadLine();
                                Console.Write("Credits: ");
                                int credits = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Teacher ID (or leave blank): ");
                                string inputTid = Console.ReadLine();
                                int? assignedTeacher = string.IsNullOrEmpty(inputTid) ? (int?)null : Convert.ToInt32(inputTid);

                                courseDAO.AddCourse(new Course(cid, cname, credits, assignedTeacher));
                                Console.WriteLine("✅ Course added!");
                                break;

                            case 4:
                                Console.Write("Enrollment ID: ");
                                int eid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Student ID: ");
                                int esid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Course ID: ");
                                int ecid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enrollment Date (yyyy-mm-dd): ");
                                DateTime edate = Convert.ToDateTime(Console.ReadLine());

                                enrollmentDAO.EnrollStudent(new Enrollment(eid, esid, ecid, edate));
                                Console.WriteLine("✅ Student enrolled!");
                                break;

                            case 5:
                                Console.Write("Payment ID: ");
                                int pid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Student ID: ");
                                int psid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Amount: ");
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                Console.Write("Payment Date (yyyy-mm-dd): ");
                                DateTime pdate = Convert.ToDateTime(Console.ReadLine());

                                paymentDAO.RecordPayment(new Payment(pid, psid, amount, pdate));
                                Console.WriteLine("✅ Payment recorded!");
                                break;

                            case 6:
                                var students = studentDAO.GetAllStudents();
                                Console.WriteLine("\n--- Students ---");
                                foreach (var s in students)
                                    Console.WriteLine($"{s.StudentId}: {s.FirstName} {s.LastName}");
                                break;

                            case 7:
                                var teachers = teacherDAO.GetAllTeachers();
                                Console.WriteLine("\n--- Teachers ---");
                                foreach (var t in teachers)
                                    Console.WriteLine($"{t.TeacherId}: {t.FirstName} {t.LastName} - {t.Email}");
                                break;

                            case 8:
                                var courses = courseDAO.GetAllCourses();
                                Console.WriteLine("\n--- Courses ---");
                                foreach (var c in courses)
                                    Console.WriteLine($"{c.CourseId}: {c.CourseName} ({c.Credits} credits) | Teacher ID: {c.TeacherId}");
                                break;

                            case 9:
                                var enrollments = enrollmentDAO.GetAllEnrollments();
                                Console.WriteLine("\n--- Enrollments ---");
                                foreach (var e in enrollments)
                                    Console.WriteLine($"EnrollmentID: {e.EnrollmentId}, StudentID: {e.StudentId}, CourseID: {e.CourseId}, Date: {e.EnrollmentDate.ToShortDateString()}");
                                break;

                            case 10:
                                var payments = paymentDAO.GetAllPayments();
                                Console.WriteLine("\n--- Payments ---");
                                foreach (var p in payments)
                                    Console.WriteLine($"PaymentID: {p.PaymentId}, StudentID: {p.StudentId}, Amount: ₹{p.Amount}, Date: {p.PaymentDate.ToShortDateString()}");
                                break;

                            case 11:
                                Console.WriteLine("👋 Exiting...");
                                return;

                            case 12:
                                Console.Write("Enter Course ID to update: ");
                                int updateCourseId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter Teacher ID to assign: ");
                                int updateTeacherId = Convert.ToInt32(Console.ReadLine());

                                // Update course's teacher_id
                                var courseList = courseDAO.GetAllCourses();
                                var courseToUpdate = courseList.Find(c => c.CourseId == updateCourseId);
                                if (courseToUpdate != null)
                                {
                                    Course updatedCourse = new Course(courseToUpdate.CourseId, courseToUpdate.CourseName, courseToUpdate.Credits, updateTeacherId);
                                    courseDAO.AddCourse(updatedCourse); // This can be replaced with a real update method
                                    Console.WriteLine("✅ Teacher assigned to the course.");
                                }
                                else
                                {
                                    Console.WriteLine("❌ Course not found.");
                                }
                                break;

                            case 13:
                                Console.Write("Enter Course Name to generate report: ");
                                string courseName = Console.ReadLine();

                                var matchedCourse = courseDAO.GetAllCourses().Find(c => c.CourseName.ToLower() == courseName.ToLower());
                                if (matchedCourse == null)
                                {
                                    Console.WriteLine("❌ Course not found.");
                                    break;
                                }

                                var allEnrollments = enrollmentDAO.GetAllEnrollments();
                                var matchedEnrollments = allEnrollments.FindAll(e => e.CourseId == matchedCourse.CourseId);

                                if (matchedEnrollments.Count == 0)
                                {
                                    Console.WriteLine("⚠️ No students enrolled in this course.");
                                }
                                else
                                {
                                    Console.WriteLine($"\n--- Enrollment Report for {courseName} ---");
                                    foreach (var en in matchedEnrollments)
                                    {
                                        var stu = studentDAO.GetAllStudents().Find(s => s.StudentId == en.StudentId);
                                        Console.WriteLine($"StudentID: {stu.StudentId}, Name: {stu.FirstName} {stu.LastName}, Enrolled On: {en.EnrollmentDate.ToShortDateString()}");
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please select from 1 to 13.");
                                break;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("❌ Error: " + ex.Message);
                    }
                }
            }
        }
    }
