using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Lessons_one.Properties;

namespace EntityFramework.Lessons_one
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IDbConnection connection = new SqlConnection(Settings.Default.DbConnect))
            {
                IDbCommand command = new SqlCommand("SELECT * FROM t_customer");
                command.Connection = connection;

                connection.Open();

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
                        reader.GetInt32(0),
                        reader.GetString(1));
                }

                Console.ReadLine();
            }
        }
    }
}
