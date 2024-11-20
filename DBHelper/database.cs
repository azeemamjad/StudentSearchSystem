using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSearchSystem.models;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace StudentSearchSystem.DBHelper
{
    public class database
    {

        public List<Student> searchByCity(string city_name)
        {
            List<Student> list = new List<Student>();

            string query = $"Select * from student where city=@city";
            string connectionString = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                    conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@city", city_name);

                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            list.Add(new Student()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                registration = reader["registration"].ToString(),
                                city = reader["city"].ToString(),
                            });
                        }
                    }
                }
            }


            return list;
        }

        public static string GetConnectionString()
        {
            string configFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            string connectionString = configuration.ConnectionStrings.ConnectionStrings["connString"].ConnectionString;
            return connectionString;
        }

    }
}
