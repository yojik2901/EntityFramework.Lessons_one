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
            var customers = GetCustomers();

            foreach (var customer in customers)
            {
                Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
                    customer.Id,
                    customer.Name);
            }

            Console.ReadLine();
        }

        private static List<CustomerProxy> GetCustomers()
        {
            using (IDbConnection connection = new SqlConnection(Settings.Default.DbConnect))
            {
                IDbCommand command = new SqlCommand("SELECT * FROM t_customer");
                command.Connection = connection;

                connection.Open();

                IDataReader reader = command.ExecuteReader();

                List<CustomerProxy> customers = new List<CustomerProxy>();

                while (reader.Read())
                {
                    CustomerProxy customer = new CustomerProxy();

                    customer.Id = reader.GetInt32(0);
                    customer.Name = reader.GetString(1);

                    customers.Add(customer);
                }

                return customers;
            }
        }
    }
}
