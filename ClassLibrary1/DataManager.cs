using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TestAdoNet;

namespace ClassLibrary1
{
    public class DataManager:DbProvider
    {
        List<Student> Students;
        public DataManager()
        {
            Students = new List<Student>();
        }
        public async Task LoadStudentsAsync()
        {
            
            Students.Clear();
            string ?query = "select * from Students";
            await SqlConnection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, SqlConnection))
            {
                using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                {
                    while (await sqlDataReader.ReadAsync())
                    {
                        Student student = new Student(sqlDataReader["Name"].ToString(), (int)sqlDataReader["Age"], (double)sqlDataReader["Rate"]);
                        Students.Add(student);
                    }
                }

            }
            SqlConnection.Close();

        }

        public async Task DisplayData()
        {
            try
            {
                if (Students.Count == 0)
                {
                    throw new Exception("Zero Students");
                }
                int i = 0;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Display:");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (var el in Students)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.1));
                    Console.WriteLine($"{++i}) Name = {el.Name}, Age = {el.Age}, Rate = {el.Rate}");
                }
                Console.WriteLine();
            }
            catch(Exception eror)
            {
                Console.WriteLine(eror.Message);
            }

        }
        public async Task AddStudentsAsync(Student s)
        {
            string? query = "insert into Students(Name, Age, Rate) values(@name,@age,@rate)";
            await SqlConnection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, SqlConnection))
            {
                cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50).Value = s.Name;
                cmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = s.Age;
                cmd.Parameters.Add("@rate", System.Data.SqlDbType.Float).Value = s.Rate;
                cmd.ExecuteNonQuery();
                SqlConnection.Close();
                Students.Add(s);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Succes ADD!!!");
            }
        }
        public async void DeleteStudents(string name)
        {
            try
            {
                
                string? query = $"DELETE FROM Students WHERE Name = @name";
                await SqlConnection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, SqlConnection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.ExecuteNonQuery();
                    SqlConnection.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Succes Delete!!!");
                    await LoadStudentsAsync();
                }

            }
            catch(Exception){ }
        }
        public async Task ChangeRateAsync(string name, double rate)
        {
            string? query = $"UPDATE Students SET Rate=@rate WHERE Name = @name";
            await SqlConnection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, SqlConnection))
            {
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                SqlConnection.Close();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Succes change rate!!!");
            }
            await LoadStudentsAsync();
        }
    }
}
