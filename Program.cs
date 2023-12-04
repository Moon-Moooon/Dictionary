using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace LearnMsSql
{
    // контейнерезация подключения к БД, прочтитать как это правильно делать
    // Ругается среда когда в public методе есть у переменной private static string connectionString  = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;
    // private static SqlConnection sqlConnection = null;
    // какой подход лучше? запись базы в код или лучше напрямую делать запросы в sql ? В ado.net Есть классы которые напрямую работают // Пока буду делать через отдлеьные запросы sql запросы :)



    internal class Program
    {
        public static void ShowMenu()
        {

            Console.WriteLine("1.Добавить слово");
            Console.WriteLine("2.Тренировка");
            Console.WriteLine("3.Редактировать "); // Подумать как !
            Console.WriteLine("4.Удалеине слова");
            Console.WriteLine("5.Выход");

            Console.Write(">");

            string choise = Console.ReadLine();
            // Тут можно попытаться реализовать как в примерах с обобщеннем 
            int intChoise = 0;

            if (int.TryParse(choise, out intChoise)) { }
            else
            {
                Console.WriteLine("Неккоретно введенное значение! Необходимо ввести чесло соответствующее одному из пунктов меню");
            }

            GetСhoiceMenu(intChoise);
            // return intChoise;
        }


        //private static void SqlConnect()
        //{
        //    // В Ado net пока понятно что придется создавать большой класс где будет происходить подключение к БД и реализация комманд с работой в БД
        //    string connectionString = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;

        //    SqlConnection sqlConnection = new SqlConnection(connectionString);

        //    sqlConnection.Open();

        //     static void WriteWordInDB(SqlConnection sqlConnection, string rusWord, string polWord)
        //    {

        //        SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Table] (Rus_Name, Pol_Name) VALUES (N'{rusWord}', '{polWord}')", sqlConnection);
        //        Console.WriteLine(sqlCommand.ExecuteNonQuery());


        //    }

        //    sqlConnection.Close();

        //}

        static void Main(string[] args)
        {

            ShowMenu();


        }

        public static void ReadWord() // нужна обработка входных значений слов что бы туда попадали только слова
        {
            #region 
            Console.WriteLine("Для добавления нового слова в словарь вам необходимо ввести слова на русском и на польском");
            Console.WriteLine("Введите слово на русском");
            Console.Write(">");
            string rusWord = Console.ReadLine();
            Console.WriteLine("Введите слово на польском");
            Console.Write(">");
            string polWord = Console.ReadLine();
            Console.WriteLine($"Добавить новую пару слов: {rusWord} с переводом {polWord}?");
            //Console.Write(">");
            //string anwer = Console.ReadLine();
            #endregion

            DBModificatet.WriteWordInDB(rusWord, polWord);
        }

        public static void GetСhoiceMenu(int menuChoice)
        {
            switch (menuChoice)
            {
                case 1:
                    ReadWord();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }

        public static void EditCoupleOfWords()
        {
            // Слова могут щахотет
            Console.WriteLine("");
        }

        public static void ToExit()
        {
            Environment.Exit(0);

        }


    }

    

    class DBModificatet
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


       public static void WriteWordInDB(string rusWord, string polWord)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Table] (Rus_Name, Pol_Name) VALUES (N'{rusWord}', '{polWord}')", sqlConnection);

            Console.WriteLine(" Запись - ", sqlCommand.ExecuteNonQuery());

            sqlConnection.Close();


        }

    }
}


#region
/*
 .будет меню
    1.добавить слово
    2.тренировка
    3.удалить слово
    4.выход из игры

.Обработка н наличие Уже имеющихся слов ! как то можно сделать через бд

 */
#endregion
