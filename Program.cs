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
    // Прикольно можно сделать что значения в консоли не цифрами дрочить А ! перкход нажатием клавиши что автоматом лишает проблемы связанной с ошибкой ввода некорректных данных
    // Нужно разрешить пробел ставить в словах
    // Можно попытаться подключить модуль для Авто опреда  яыкуа, При выводу неккоректного символа

    #region // Что надо сделать

    #endregion

    internal class Program
    {

        public static void ShowMenu()
        {
            GetDelegate.CommandHandler comm = AddWord;

            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {
                {"1.добавить слово", AddWord }

            };
            MenuWork.MenuStart(Links);

        }
        //фикст чего то 


        public static int ParceInputMenuCHoise(string choise)
        {

            int intChoise = 0;

            if (int.TryParse(choise, out intChoise))   // Метод должен как то сам выбирать точку переход
            {
                return (intChoise);
            }
            else
            {
                Console.WriteLine("Неккоретно введенное значение! Необходимо ввести чесло соответствующее одному из пунктов меню");

                ShowMenu();

            }

            return 0;
        }

        static void Main(string[] args)
        {

            GetDelegate.CommandHandler comm = AddWord;

            Dictionary <string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {
                {"1.добавить слово", AddWord }

            };

            MenuWork.MenuStart(Links);

        }

        public static void ExeminationPolWord (string item)
        {
            item = item.ToLower();
            string str = "ąćęłńóśźż abcdefghijklmnopqrstuvwxyz";
            int n = 0;
            bool bl = false;
            while (bl == false)
            {
                for (int j = 0; j < item.Length; j++)
                {
                    for (int i = 0; i < str.Length; i++)
                    {

                        if (item[j] != str[i])
                        {

                            n++;

                        }
                        else
                        {
                            j++;

                            i = -1;

                            n = 0;
                        }

                        if (n == str.Length || n < i)
                        {

                            Console.WriteLine($" Не верный символ {item[j]}");

                            getAfterBedCHois(item[j]);
                        }

                        if (j == item.Length)
                        {

                            bl = true;

                            i = str.Length;

                        }

                    }
                }
            }
        }

        public static void AddWord() // нужна обработка входных значений слов что бы туда попадали только слова
        {                              // Если вдруг разхотелось слово добовлять !
            #region 
            Console.WriteLine("Для добавления нового слова в словарь вам необходимо ввести слова на русском и на польском");
            Console.WriteLine("Введите слово на русском");
            Console.Write(">");
            string rusWord = Console.ReadLine();
            ExeminationRusWord(rusWord); // Тут мы и остановились !!!!
            Console.WriteLine("Введите слово на польском");
            Console.Write(">");
            string polWord = Console.ReadLine();
            ExeminationPolWord(polWord);
            Console.WriteLine($"Добавить новую пару слов: {rusWord} с переводом {polWord}?");
            //Console.Write(">");
            //string anwer = Console.ReadLine();
            #endregion

            DBModificatet.WriteWordInDB(rusWord, polWord);
        }

        public static string ExeminationRusWord(string rusWord) // Наличие 2 параметра вообще на какой то кал похоже 
        {
            string word = rusWord.ToLower();

            char[] bak = new char[1];
            int i = 0;
            int j = 1;

            foreach (char item in word)
            {
                bak[i] = item;

                byte[] wordAsByteMass = Encoding.Default.GetBytes(bak);

                int f = wordAsByteMass.Length;

                if (wordAsByteMass.Length < 2)
                {
                    if (wordAsByteMass[i] == 32)
                    {

                    }
                    else
                    {
                        getAfterBedCHois(item);
                    }
                }
                else
                {

                    if (
                       (wordAsByteMass[i] >= 208 && wordAsByteMass[i] <= 209) &&
                       ((176 <= wordAsByteMass[j] && wordAsByteMass[j] <= 191) || (wordAsByteMass[j] >= 128 && wordAsByteMass[j] <= 143) || (wordAsByteMass[j] == 145))
                       )
                    {
                    
                    }
                    else
                    {
                        getAfterBedCHois(item);
                    }

                }
            }

            return word;
        }

        public static void getAfterBedCHois (char warningChar)
        {
            Console.Clear();



            Console.WriteLine($"Символ {warningChar} - является некорректным или не на соответствующем языке");

            Console.WriteLine("Хотите добавить слово еще раз ?");

            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {
                {"1.Да", AddWord },
                { "2.Нет", ShowMenu}
            };

            MenuWork.MenuStart(Links);

        }


        public static void GetСhoiceMenu(int menuChoice )
        {
            switch (menuChoice)
            {
                case 1:
                    AddWord();
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

            Console.WriteLine($" Запись - {sqlCommand.ExecuteNonQuery()}");

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
