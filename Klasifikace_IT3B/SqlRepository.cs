using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace Klasifikace_IT3B
{
    public class SqlRepository
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;
                                      Initial Catalog=Klasifikace;
                                      Integrated Security=True;
                                      Connect Timeout=30;
                                      Encrypt=False;
                                      TrustServerCertificate=False;
                                      ApplicationIntent=ReadWrite;
                                      MultiSubnetFailover=False";
        private Dictionary<int,Teacher> teachers;
        private Dictionary<int, Subject> subjects;

        public SqlRepository()
        {
            teachers = GetTeachersFromDB();
            subjects = GetSubjectsFromDB();
        }

        private List<Student> TempStudents()
        {
            List<Student> students = new List<Student>();
            var student1 = new Student()
            {
                Id = 1,
                Firstname = "Pepa",
                Lastname = "Zdepa",
                Birthday = DateTime.Parse("1.1.2000"),
                Grades = new List<Grade>()
            };
            students.Add(student1);
            var student2 = new Student()
            {
                Id = 1,
                Firstname = "Franta",
                Lastname = "Skočdopole",
                Birthday = DateTime.Parse("28.2.2000"),
                Grades = new List<Grade>()
            };
            students.Add(student2);
            return students;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "select * from Student";
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                students.Add(new Student()
                                {
                                    Id = Convert.ToInt32(sqlDataReader["IdStudent"]),
                                    Firstname = sqlDataReader["Firstname"].ToString(),
                                    Lastname = sqlDataReader["Lastname"].ToString(),
                                    Birthday = Convert.ToDateTime(sqlDataReader["Birthday"])
                                });
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Some error happend (Exception: {ex.Message})");
            }
            

            //return TempStudents();
            return students;
        }

        public List<Grade> GetGrades(Student student)
        {
            List<Grade> grades = new List<Grade>();
            using(SqlConnection sqlConnection = new SqlConnection(connString))
            {
                using(SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                {
                    sqlCommand.CommandText = "";
                }
            }
            return grades;
        }

        private Dictionary<int,Teacher> GetTeachersFromDB()
        {
            Dictionary<int, Teacher> teachers = new Dictionary<int, Teacher>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "select * from Teacher";
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                var id = Convert.ToInt32(sqlDataReader["IdTeacher"]);
                                teachers.Add(id, new Teacher()
                                {
                                    Id = id,
                                    Name = sqlDataReader["Name"].ToString(),
                                    ShortName = sqlDataReader["ShortName"].ToString()                                    
                                });
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error happend (Exception: {ex.Message})");
            }

            return teachers;
        }

        public List<Teacher> GetTeachers()
        {
            return teachers.Values.AsEnumerable().ToList();
        }

        public Teacher GetTeacher(int id)
        {
            if (teachers.ContainsKey(id))
            {
                return teachers[id];
            }
            return null;
        }

        private Dictionary<int, Subject> GetSubjectsFromDB()
        {
            Dictionary<int, Subject> subjects = new Dictionary<int, Subject>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "select * from Subject";
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                var id = Convert.ToInt32(sqlDataReader["IdSubject"]);
                                subjects.Add(id, new Subject()
                                {
                                    Id = id,
                                    Name = sqlDataReader["Name"].ToString(),
                                    ShortName = sqlDataReader["ShortName"].ToString(),
                                    Teacher = GetTeacher(Convert.ToInt32(sqlDataReader["IdTeacher"]))
                                }); 
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error happend (Exception: {ex.Message})");
            }

            return subjects;
        }

        public List<Subject> GetSubjects()
        {
            return subjects.Values.AsEnumerable().ToList();
        }

        public Subject GetSubject(int id)
        {
            if (subjects.ContainsKey(id))
            {
                return subjects[id];
            }
            return null;
        }
    }
}
