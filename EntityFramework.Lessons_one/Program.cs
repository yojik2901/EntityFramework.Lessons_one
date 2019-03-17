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
            #region Получение данных из БД вручную

            //var customers = GetCustomers();

            //foreach (var customer in customers)
            //{
            //    Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
            //        customer.Id,
            //        customer.Name);
            //}

            #endregion

            #region Получение данных из БД EntityFramework

            var customers = GetCustomersEf();

            foreach (var customer in customers)
            {
                Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
                    customer.CustomerId,
                    customer.CustomerName);
            }
            
            #endregion
        }

        #region Получение данных из БД вручную
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
        #endregion

        #region Получение данных из БД EntityFramework

        private static List<Customer> GetCustomersEf()
        {
            var context = new TestDbContext();

            var customers = context.Customer.ToList();

            return customers;
        }


        #endregion
    }
}
