using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    internal class DBModificatet
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        public static void WriteWordInDB(string rusWord, string polWord)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Table] (Rus_Name, Pol_Name) VALUES (N'{rusWord}', '{polWord}')", sqlConnection);

            Console.WriteLine($" Запись - {sqlCommand.ExecuteNonQuery()}");

            sqlConnection.Close();


        }

        public static SqlDataReader SelectWord(string word)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"SELECT Rus_Name, Pol_Name FROM Words WHERE Rus_Name LIKE N'%{word}%'", sqlConnection);

            return sqlCommand.ExecuteReader();

            sqlConnection.Close();
        }
    }
}
