using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EntityFramework.Lessons_one.Properties;

namespace EntityFramework.Lessons_one
{
    class Program
    {
        static void Main(string[] args)
        {
            RelationsCustomerOnProduct();
        }

        #region Получение данных из БД вручную
        private static void GetCustomers()
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
                
                foreach (var customer in customers)
                {
                    Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
                        customer.Id,
                        customer.Name);
                }
                
            }
        }
        #endregion

        #region Получение данных из БД EntityFramework

        private static void GetCustomersEf()
        {
            var context = new TestDbContext();

            //IQueryable<Customer> query = context.Сustomers.Where(c => c.CustomerId == 1);

            var query = from c in context.Сustomers
                where c.CustomerId == 1
                select c;


            List < Customer > customers = query.ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine("Идентификатор: {0}\t Имя: {1}",
                    customer.CustomerId,
                    customer.CustomerName);
            }
        }
        
        #endregion

        private static void RelationsCustomerOnProduct()
        {
            var context = new TestDbContext();

            var transactions = from c in context.Сustomers
                join o in context.Orders on c.CustomerId equals o.CustomerId
                join op in context.OrderProducts on o.OrderId equals op.OrderId
                join p in context.Products on op.ProductId equals p.ProductId
                select new
                {  
                    CustomerName = c.CustomerName,
                    ProductName = p.ProductName,
                    ProductCount = op.Count
                };

            foreach (var transaction in transactions)
            {
                Console.WriteLine("Покупатель: {0}\tПродукт: {1}\tКоличество: {2}", 
                    transaction.CustomerName,transaction.ProductName,transaction.ProductCount);
            }
        }
    }
}
