using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Text;

namespace LearnMsSql
{
    // контейнерезация подключения к БД, прочтитать как это правильно делать
    // Ругается среда когда в public методе есть у переменной private static string connectionString  = ConfigurationManager.ConnectionStrings["DT"].ConnectionString;
    // private static SqlConnection sqlConnection = null;
    // какой подход лучше? запись базы в код или лучше напрямую делать запросы в sql ? В ado.net Есть классы которые напрямую работают // Пока буду делать через отдлеьные запросы sql запросы :)
    // Необходимо обрабатывать на входе слова, что бы точно быть уверенным что используется русский и польский языки


    internal class Program
    {
        public static void ShowMenu()
        {

            Console.WriteLine("1.Добавить слово");
            Console.WriteLine("2.Тренировка");
            Console.WriteLine("3.Редактировать "); 
            Console.WriteLine("4.Удалеине слова");
            Console.WriteLine("5.Выход");

            Console.Write(">");

            string choise = Console.ReadLine();
            // Тут можно попытаться реализовать как в примерах с обобщеннем 
            //int intChoise = 0;

            //if (int.TryParse(choise, out intChoise)) { }
            //else
            //{
            //    Console.WriteLine("Неккоретно введенное значение! Необходимо ввести чесло соответствующее одному из пунктов меню");
            //}

            //GetСhoiceMenu(intChoise);
            // return intChoise;
        }

        public static void ParceInputMenuCHoise(string choise)
        {

            int intChoise = 0;

            if (int.TryParse(choise, out intChoise))
            {
                GetСhoiceMenu(intChoise);
            }
            else
            {
                Console.WriteLine("Неккоретно введенное значение! Необходимо ввести чесло соответствующее одному из пунктов меню");
            }

        }


        static void Main(string[] args)
        {

            // ShowMenu();

           // Console.WriteLine(s.ToLower());   

        }

        public static bool AnalysisRussianWord(string rusWord)
        {

            return true;
        }

        public static void ReadWord() // нужна обработка входных значений слов что бы туда попадали только слова
        {                              // Если вдруг разхотелось слово добовлять !
            #region 
            Console.WriteLine("Для добавления нового слова в словарь вам необходимо ввести слова на русском и на польском");
            Console.WriteLine("Введите слово на русском");
            Console.Write(">");
            string rusWord = Console.ReadLine();
            ExeminationRusWord(rusWord, out rusWord);
            Console.WriteLine("Введите слово на польском");
            Console.Write(">");
            string polWord = Console.ReadLine();
            Console.WriteLine($"Добавить новую пару слов: {rusWord} с переводом {polWord}?");
            //Console.Write(">");
            //string anwer = Console.ReadLine();
            #endregion

            DBModificatet.WriteWordInDB(rusWord, polWord);
        }

        public static string ExeminationRusWord(string rusWord, out string warningChar)
        {
            string word = rusWord.ToLower();

            warningChar = string.Empty;

            byte[] listUtf8 = Encoding.Default.GetBytes(rusWord);

            int j = 1;

            for (int i = 0; i < listUtf8.Length; i++)
            {
                if (
                   (listUtf8[i] >= 208 && listUtf8[i] <= 209) &&
                   ((176 <= listUtf8[j] && listUtf8[j] <= 191) || (listUtf8[j] >= 128 && listUtf8[j] <= 143) || (listUtf8[j] == 145))
                   )
                { }

                else
                {
                    return null;
                }

                i++;
                j += 2;
            }

            return word;
        }

        public static void getAfterBedCHois (string warningChar)
        {
            Console.WriteLine($"Символ {warningChar} - является некорректным");
            Console.WriteLine("Хотите добавить слово еще раз ?");
            string anwer = Console.ReadLine();  

        }


        public static void GetСhoiceMenu(int menuChoice )
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
            // Слова могут попытаться начать искать и на русском и на польском 
            // Слово на русском - записали - 
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
