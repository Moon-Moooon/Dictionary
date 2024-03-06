using Microsoft.Data.SqlClient;
using System;
using System.Configuration;

namespace Slovar.StaticClass
{
    internal class DBModificatet
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;
        
        static SqlConnection sqlConnection = new SqlConnection(connectionString);
        
        const string a = "";

        public static void WriteWordInDB(string rusWord, string polWord)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Words] (Rus_Name, Pol_Name) VALUES (N'{rusWord}', '{polWord}')", sqlConnection);

            Console.WriteLine($" Запись - {sqlCommand.ExecuteNonQuery()}");

            sqlConnection.Close();
        }
            
        public static SqlDataReader SelectWord(string word, string leng)
        {
            string str = string.Empty;
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(str = SelectLeng(leng, word), sqlConnection);

            return sqlCommand.ExecuteReader();

            sqlConnection.Close();
        }
        
        public static void UpdateWord(string rusName, string polName, int IDWord)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"UPDATE [Words] SET Rus_Name = {rusName}, Pol_Name = {polName} WHERE id = {IDWord}", sqlConnection);

            sqlConnection.Close();
        }

        public static string SelectLeng(string leng, string word)
        {
            if (leng == "rus") return $"SELECT Id, Rus_Name, Pol_Name FROM Words WHERE Rus_Name LIKE N'%{word}%'";
            else return $"SELECT Id, Rus_Name, Pol_Name FROM Words WHERE Pol_Name LIKE N'%{word}%'";
        }
    }
}
