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

    #region // Что надо сделать
    
    #endregion

    internal class Program
    {
        // килир фича
        //клирФича 2
        //килирФича3
        public static void ShowMenu()
        {
            Console.WriteLine("1.Добавить слово");
            Console.WriteLine("2.Тренировка");
            Console.WriteLine("3.Редактировать "); 
            Console.WriteLine("4.Удалеине слова");
            Console.WriteLine("5.Выход");

            Console.Write(">");

            string choise = Console.ReadLine();
            ParceInputMenuCHoise(choise);

        }

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
         
        }

        public static void ExeminationPolWord (string item)
        {
            item = null;
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

        public static void ReadWord() // нужна обработка входных значений слов что бы туда попадали только слова
        {                              // Если вдруг разхотелось слово добовлять !
            #region 
            Console.WriteLine("Для добавления нового слова в словарь вам необходимо ввести слова на русском и на польском");
            Console.WriteLine("Введите слово на русском");
            Console.Write(">");
            string rusWord = Console.ReadLine();
            ExeminationRusWord(rusWord, out rusWord); // Тут мы и остановились !!!!
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

        public static string ExeminationRusWord(string rusWord, out string warningChar) // Наличие 2 параметра вообще на какой то кал похоже 
        {
            string word = rusWord.ToLower();

            warningChar = string.Empty;

            byte[] listUtf8 = Encoding.Default.GetBytes(rusWord);

            char[] bak;

            int j = 1;

            foreach (char item in word)
            {
                bak = new char[item];

                byte[] wordAsByteMass = Encoding.Default.GetBytes(bak);

                if (wordAsByteMass.Length < 2)
                {
                    if (wordAsByteMass[0] == 32)
                    {

                    }
                    else
                    {
                        getAfterBedCHois(item);
                    }
                }
                else
                {
                    for (int i = 0; i < listUtf8.Length; i++)
                    {
                        if (
                           (listUtf8[i] >= 208 && listUtf8[i] <= 209) &&
                           ((176 <= listUtf8[j] && listUtf8[j] <= 191) || (listUtf8[j] >= 128 && listUtf8[j] <= 143) || (listUtf8[j] == 145))
                           )
                        { }

                        else
                        {
                            getAfterBedCHois(item);
                        }

                        i++;
                        j += 2;
                    }
                }
            }


            return word;
        }

        public static void getAfterBedCHois (char warningChar)
        {
            Console.WriteLine($"Символ {warningChar} - является некорректным");

            Console.WriteLine("Хотите добавить слово еще раз ?");
            Console.WriteLine("1.Да");
            Console.WriteLine("2.Нет");

            string choise = Console.ReadLine(); 

            int menuChoise = ParceInputMenuCHoise(choise);
            
            switch(menuChoise)
            {
                case 1:
                    GetСhoiceMenu(menuChoise);
                    break;
                case 2:
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Неверно выбранные пункты меню!");

                    getAfterBedCHois(warningChar);

                    break;

            }
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


//public static string ExeminationRusWord(string rusWord, out string warningChar) // Наличие 2 параметра вообще на какой то кал похоже 
//{
//    string word = rusWord.ToLower();

//    warningChar = string.Empty;

//    byte[] listUtf8 = Encoding.Default.GetBytes(rusWord);

//    char[] bak;

//    int j = 1;

//    foreach (char item in word)
//    {
//        bak = new char[item];

//        byte[] wordAsByteMass = Encoding.Default.GetBytes(bak);

//        if (wordAsByteMass.Length < 2)
//        {
//            if (wordAsByteMass[0] == 32)
//            {

//            }
//            else
//            {
//                getAfterBedCHois(item);
//            }
//        }
//        else
//        {
//            for (int i = 0; i < listUtf8.Length; i++)
//            {
//                if (
//                   (listUtf8[i] >= 208 && listUtf8[i] <= 209) &&
//                   ((176 <= listUtf8[j] && listUtf8[j] <= 191) || (listUtf8[j] >= 128 && listUtf8[j] <= 143) || (listUtf8[j] == 145))
//                   )
//                { }

//                else
//                {
//                    getAfterBedCHois(item);
//                }

//                i++;
//                j += 2;
//            }
//        }
//    }


//    return word;
//}



//  Сортировка польского по байтам
//int j = 1;
//string a = "ąćęłńóśźż";
//string ff = "ąćęłńóśźż";
//string str = "ąćęłńóśźż abcdefghijklmnopqrstuvwxyz";
//string ba = "ę";
//string warningString;
//byte[] warningByte;
//byte[] listUtf8 = Encoding.Default.GetBytes(str);
//// 97 - 122
//byte[] ErroneousChar;

//foreach (char item in str)
//{
//    char[] massivItem = { item }; // создание переменных идет в теле метода 

//    byte[] simvol = Encoding.Default.GetBytes(massivItem);

//    if (simvol.Length < 2)
//    {
//        if (((simvol[0] >= 97) && (simvol[0] <= 122)) || (simvol[0] == 32))
//        {

//        }
//        else
//        {

//            getAfterBedCHois(item);

//        }
//    }
//    else
//    {
//        // Обязательно надо убдет переделать но это позже
//        if (
//            (massivItem[0] >= 195) && (massivItem[0] <= 197) ||
//            ((massivItem[1] == 133) && (massivItem[1] == 135) && (massivItem[1] == 153) && (massivItem[1] == 130) && (massivItem[1] == 132) && (massivItem[1] == 179) && (massivItem[1] == 155) && (massivItem[1] == 186) && (massivItem[1] == 188))
//            )
//        {

//        }
//        else
//        {

//            getAfterBedCHois(item);

//        }

//    }
//}