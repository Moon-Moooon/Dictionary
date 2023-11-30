using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;


namespace LearnMsSql
{
    internal class Program
    {
        public static int ShowMenu()
        {//1
            // second branch
            Console.WriteLine("1.Добавить слово");
            Console.WriteLine("2.Тренировка");
            Console.WriteLine("3.Редактировать "); // Подумать как !
            Console.WriteLine("4.Удалеине слова");
            Console.WriteLine("5.Выход");

            Console.Write(">");

            string choise = Console.ReadLine();
            // Тут можно попытаться реализовать как в примерах с обобщеннем 
            int intChoise = 0;

            if (int.TryParse(choise, out intChoise)) {}
            else
            {
                Console.WriteLine("Неккоретно введенное значение! Необходимо ввести чесло соответствующее одному из пунктов меню");
            }

            Console.ReadKey();

            return intChoise;
        }

        private static string connectionString  = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;

        private static SqlConnection sqlConnection = null;


        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();


             // addWord();


            sqlConnection.Close();


        }

        public static void addWord()
        {  
            Console.WriteLine("Для добавления нового слова в словарь вам необходимо ввести слова на русском и на польском");
            Console.WriteLine("Введите слово на русском");
            Console.Write(">");
            string rusWord =  Console.ReadLine();
            Console.WriteLine("Введите слово на польском");
            Console.Write(">");
            string polWord = Console.ReadLine();
            Console.WriteLine($"Добавить новую пару слов: {rusWord} с переводом {polWord}?");
            Console.Write(">");
            string anwer = Console.ReadLine(); // добавить обработку набуквы y и n 


            if (true)
            {

            }

            SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Table] (Rus_Name, Pol_Name) VALUES (N'{rusWord}', '{polWord}')" ,sqlConnection);

            Console.WriteLine(sqlCommand.ExecuteNonQuery());

            static void add()
            {

            }

        }

        public static void GetСhoiceMenu(int menuChoice)
        {
            switch (menuChoice)
            {
                case 0:
                    addWord();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        public static void ToExit ()
        {
            sqlConnection.Close();
            Environment.Exit();

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