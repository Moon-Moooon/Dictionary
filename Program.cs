using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Text;
using System.Drawing;


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
    // По окончанию основого функцонала надо будет провести полный рефакторинг 
    // какого черта методом Read разрешено пользоваться 1 раз а после окончания цикла все :(
    // Ступенчатый цикл где конечное и начальное значение изменяется 
    // Баг вывода меню если после большого набора меню вызывать маленькой
    
    #region // Что надо сделать
    //Поиск по слову, доделать поиск как на русском так и на польском
    // Необходимо полностью продумать отрисовку меню !
    // 
    #endregion

    internal class Program
    {
        #region
        /*
            1.добавить слово
            2.тренировка
            3.Поиск и работа со словами 
            4.выход

         */
        #endregion

        public static void ShowMenu()
        {
            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {

                {"1.добавить слово", AddWord },
                {"2.Поиск слова", readWord},
                {"5.Выход", ToExit}

            };

            MenuWork start = new(Links);

        }

        public static void SearchWordInDB()
        {
            Console.WriteLine("Поиск слова осуществлять на: ");
            // Вызов менюшки 
            // если вызов на русском -> обработать что это русский -> Формировать запрос в БД
            // Поиск будет по не полному слвоу 
            // Есть проблема с тем что вызывая меню выбора на каком языке искать нужно по факту 2 одинаковы реализации вызова проверки слов на язык
            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {

                {"1.Поск на русском", readWord },
                { "2.Поиск на польском", readWord}

            };

            MenuWork start = new(Links);
        }

        public static void stub() 
        { 
            var Dicti = new Dictionary<string, GetDelegate.CommandHandler>()
            { 
                {"Заглушка",stub }
            };

            MenuWork start = new(Dicti);
        }

        public static void readWord() // Не дописан 
        {
            Console.WriteLine("Введите слово на русском\n >");
            // string word = Console.ReadLine();
            // ExeminationRusWord(word);
            var Dicti = new Dictionary<string, GetDelegate.CommandHandler>();

            //var DateList = new List<string>();

            SqlDataReader Date = DBModificatet.SelectWord("к");
            // Ступенчатый цикл где конечное и начальное значение изменяется 
            int counRows = 0;


            while (Date.Read())
            {
                counRows++;

                object rusWord = Date.GetValue(0);

                object polName = Date.GetValue(1);

                string WordRow = $"{rusWord} - {polName}";

                Dicti.Add(WordRow, stub);
            }

            MenuWork start = new(Dicti, false, 2); // Магическое число, это число (не индекс!!) строк
            // необходимо продумать навигацию меню уже с выданным словарем.
            // допустим выбирая слово выходит подМеню под выбранным словом со сдвигом в право
            // в под меню
            // 1.редактировать
            // 2.Удолить
            // 3.Вернуться к словарю

            //int sourceLeft;
            //int sourceTop;

            //void TextMoev(int line)
            //{
            //    int inX;

            //    int inY;

            //    int sourceHeight;

            //    int targetTop;

            //    WriteCutsPosic(out inX, out inY);

            //    sourceHeight = inY - line;

            //    targetTop = line + 1;

            //    Console.MoveBufferArea(0, line, Console.BufferWidth, sourceHeight, 0, targetTop);

            //    // Console.SetCursorPosition(0,line);
            //}

            //void ClearLine(int line)
            //{
            //    Console.MoveBufferArea(0, line, Console.BufferWidth, 4, 0, line + 1);
            //    // Если трогать окно строки ползают и могут менять свое положение 
            //}

            //static void WriteCutsPosic(out int intX, out int intY)
            //{
            //    int inX = Console.CursorLeft;
            //    intX = inX;
            //    int inY = Console.CursorTop;
            //    intY = inY;
            //}

            //Console.WriteLine("1111111111111111111111111111111111111111111111111111111111111111");
            //Console.WriteLine("2222222222222222222222222222222222222222222222222222222222222222");
            //Console.WriteLine("3333333333333333333333333333333333333333333333333333333333333333");
            //Console.WriteLine("4444444444444444444444444444444444444444444444444444444444444444");
            //Console.WriteLine("5555555555555555555555555555555555555555555555555555555555555555");
            //Console.WriteLine("6555566666666666666666666666666666666666666666666666666666666666");
            //Console.WriteLine("1777777777777777777777777777777777777777777777777777777777777777");

            //TextMoev(1);

            //ShowMenu();

        }

        public static void SubMenu()
        {

            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {

                {"1.Редактировать", stub },
                {"2.Удолить пару", stub},
                {"3.Вернуться к словорю", stub},
                {"4.Вернутсья к главному меню",stub }
            };

            MenuWork start = new(Links);
        }



        public static void RedactionWord()
        {

        }


        static void Main(string[] args)
        {
            readWord();
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
            ExeminationRusWord(rusWord); 
            Console.WriteLine("Введите слово на польском");
            Console.Write(">");
            string polWord = Console.ReadLine();
            ExeminationPolWord(polWord);
            Console.WriteLine($"Добавить новую пару слов: {rusWord} с переводом {polWord}?");
            #endregion

            DBModificatet.WriteWordInDB(rusWord, polWord);
        }

        public static string ExeminationRusWord(string rusWord) 
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
            Console.WriteLine($"Символ {warningChar} - является некорректным или не на соответствующем языке.\n Хотите добавить слово еще раз ?");

            Dictionary<string, GetDelegate.CommandHandler> Links = new Dictionary<string, GetDelegate.CommandHandler>()
            {
                {"1.Да", AddWord },
                { "2.Нет", ShowMenu}
            };

            MenuWork start = new(Links);

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

       public static SqlDataReader SelectWord (string word)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"SELECT Rus_Name, Pol_Name FROM Words WHERE Rus_Name LIKE N'%{word}%'", sqlConnection);

            return sqlCommand.ExecuteReader();

            sqlConnection.Close();
        }
    }
}
