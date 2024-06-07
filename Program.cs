using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnextionExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. подготовить строку подключения
            string connectionString = @"Data Source=HOME-PC\SQLEXPRESS;
                                        Initial Catalog=students_db_pv324;
                                        Integrated Security=SSPI;
                                        Connect Timeout=5;";
            //2. создать объект подключения к БД
            SqlConnection connection = new SqlConnection(connectionString);
            //3. открыть соединение к Бд
            connection.Open();
            Console.WriteLine("Connection established successfully");
            // ЗАДАНИЕ. Выполнить запрос, который выводитв первом столбце "Фамилия И.",
            // во втором столбце рейтинг студента, только для студентов со стипендией
            
            //4. получить и вывести данные
            //4.1. подготовим строку sql запроса
            string queryString = @"SELECT * FROM students_t;";
            //4.2 создать объект sql запроса
            SqlCommand query = new SqlCommand(queryString, connection);
            //4.3 выполнить sql запрос с табличным результатом
            SqlDataReader queryResult = query.ExecuteReader();
            // 4.4 считать результат sql запроса 
            while (queryResult.Read())
            {
                
                string lastName = queryResult.GetString(queryResult.GetOrdinal("last_name_f"));
                string firstName = queryResult.GetString(queryResult.GetOrdinal("first_name_f"));
                int rating = queryResult.GetInt32(queryResult.GetOrdinal("rate_f"));
                string initials = lastName.Substring(0, 1) + ".";
                decimal scholarship = queryResult.GetDecimal(queryResult.GetOrdinal("grants_f"));
                
                string fullName = $"{lastName} {initials} - {rating}";
                if (scholarship > 0)
                {
                    Console.WriteLine(fullName);
                }

                
            }
            // 4.5 закрыть объект результата sql запроса
            queryResult.Close();
            
            // n отключение от Бд
            connection.Close();
            Console.WriteLine("the connection was successfully closed");

            
        }
    }
}
