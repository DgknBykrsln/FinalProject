using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinalProject
{
    class Program
    {
        static void ReadFromTable(SqlConnection connection)
        {
            try
            {
                // Veri �ekme i�lemi
                string selectQuery = "SELECT * FROM Table_1";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string column1Value = reader.GetString(0);
                    int column2Value = reader.GetInt32(1);
                    // ... Di�er s�tun de�erlerini alabilirsiniz

                    Console.WriteLine($"Blood Type: {column1Value}, Donor Name: {column2Value}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veri �ekme hatas�: " + ex.Message);
            }
        }

        static void WriteToTable(SqlConnection connection)
        {
            try
            {
                // Veri yazma i�lemi
                string insertQuery = "INSERT INTO Table_1 ([Blood Type], [Donor Name], [Donation Date], [Units]) VALUES (@value1, @value2, @value3, @value4)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                DateTime dateTime = DateTime.Now;

                // Parametre de�erlerini belirleme
                insertCommand.Parameters.AddWithValue("@value1", "Test1");
                insertCommand.Parameters.AddWithValue("@value2", "Test2");
                insertCommand.Parameters.AddWithValue("@value3", dateTime);
                insertCommand.Parameters.AddWithValue("@value4", 2);

                int rowsAffected = insertCommand.ExecuteNonQuery();

                Console.WriteLine($"Etkilenen sat�r say�s�: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veri yazma hatas�: " + ex.Message);
            }
        }

        static void Main(string[] args)
        {
            string connectionString = "Server=tcp:final-project-yu.database.windows.net,1433;Initial Catalog=blood;Persist Security Info=False;User ID=TheViperI2KI;Password=Gs605dba!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Ba�lant� ba�ar�l�!");

                    // Tablodan veri okuma
                    ReadFromTable(connection);

                    // Tabloya veri ekleme
                    WriteToTable(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ba�lant� veya i�lem hatas�: " + ex.Message);
                }
            }
        }
    }
}
