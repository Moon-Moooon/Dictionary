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

             StartMenuWork start = new(Links);
            // MenuDefolt menuDefolt = new MenuDefolt(Links);
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

            StartMenuWork start = new(Links);
        }

        public static void stub() 
        { 
            var Dicti = new Dictionary<string, GetDelegate.CommandHandler>()
            { 
                {"Заглушка",stub }
            };

            StartMenuWork start = new(Dicti);
        }

        public static void readWord() // Не дописан 
        {
            // Интересно как это выглядит когда я переменную типа Object передаю форматированной стракой как stringt, скорее всего там под капотный боксинг :(
            // Ступенчатый цикл где конечное и начальное значение изменяется 
            Console.WriteLine("Введите слово на русском\n >");
            // string word = Console.ReadLine();
            // ExeminationRusWord(word);
            var Dicti = new Dictionary<string, GetDelegate.CommandHandler>();
            
            //var DateList = new List<string>();

            SqlDataReader Date = DBModificatet.SelectWord("к");

            List<Word> WordCollection = new List<Word>(); // Тест

            int counRows = 0;


            while (Date.Read())
            {
                counRows++;

                object rusWord = Date.GetValue(0);

                object polName = Date.GetValue(1);

                string WordRow = $"{rusWord} - {polName}";

                Dicti.Add(WordRow, stub);

                WordCollection.Add(new Word(0,$"{rusWord}", $"{polName}"));
            }

            StartMenuWork start = new(Dicti, false, 2, WordCollection); //!!!!!!!!!!!!!
            // Магическое число, это число (не индекс!!) строк

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

            StartMenuWork start = new(Links);
        }



        public static void RedactionWord(Word word) // Рефакт!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! - Вроде норм работает 
        {
            string s = word.doubl();
             
            int inY = Console.CursorTop;
            string polName;
            string rusName;

            word.GetName(out rusName, out polName);

            string newPolName;
            string newRusName;

            string p = polName;
            string r = rusName;
            int rightItnerval = r.Length + 3;
            StringBuilder fullString = new($"{r} - {p}");
            int pX = 0;
            bool loop = true;
            int inx =0;
            Console.Write(fullString);
            Console.SetCursorPosition(0, inY);
            while (loop)
            {
                int maxLenght = rightItnerval + p.Length - 1;
                switch (Console.ReadKey(true).Key) 
                {
                    case ConsoleKey.Enter:
                        // Тут метод спрашивает а надо ли менять 
                        loop = false;
                        newPolName = p;
                        newRusName = r;
                        Message(r, p, word.IDword);
                        break;
                    case ConsoleKey.Backspace:
                        if(inx >= rightItnerval) p = p.Remove(pX, 1);
                        else r = r.Remove(inx, 1);
                        Console.SetCursorPosition(0, inY);
                        CursorMove.ClearLines(inY, inY, 1);
                        Console.Write($"{r} - {p}");
                        if (inx == r.Length)
                        {
                            inx--;
                        }
                        if ( inx < rightItnerval)
                        {
                            rightItnerval--;
                        }
                        if (inx == maxLenght)
                        {
                            inx--;
                            pX--;
                        }
                        Console.SetCursorPosition(inx, inY);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (inx == 0) break;
                        if (inx == rightItnerval) inx = r.Length;
                        if(inx > rightItnerval) pX--;
                        inx--;
                        Console.SetCursorPosition(inx, inY);
                        break;
                    case ConsoleKey.RightArrow:
                        if(inx == maxLenght) break;
                        inx++;
                        if (inx == r.Length) inx = rightItnerval;
                        if (inx > rightItnerval) pX++;

                        Console.SetCursorPosition(inx, inY);
                        break;
                }
            }

            static void Message(string newRusNAme, string newPolName, int IDword)
            {
                setStructDelegate item = new setStructDelegate(); // метод который заберает слово а затем пушит в БД
                item.editWord =;///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                setStructDelegate item1 = new setStructDelegate(); // надо возвращать прошлое меню 

                Console.WriteLine("Уверены что хотите сохранить изменения ?");

                Dictionary<string, setStructDelegate> Links = new Dictionary<string, setStructDelegate>()
                {
                    {"1.Да",item}, // Да - передача нового слова. new Word с передачей параметров 
                    {"2.Нет",item1}, // нет - возвращение к меню слов.
                };
            }
            //rf rf
            static void CallUpdateWord(string newRusNAme, string newPolName, int IDword) //Нет теста 
            {
                DBModificatet.UpdateWord(newRusNAme, newPolName, IDword);
            }
            // Тут ужес помощью Выражений

        }

        static void Main(string[] args)
        {
            // 
            setStructDelegate item = new setStructDelegate();

            Word word = new(1, "Спать", "Spac");
            item.editWord = RedactionWord;

            setStructDelegate item2 = new setStructDelegate();

            Word word2 = new(1, "2", "3");
            item2.CH = SubMenu;

            List<Word> list = new List<Word>();
            list.Add(word);
            list.Add(word2);
            Dictionary<string, setStructDelegate> Links = new Dictionary<string, setStructDelegate>()
            {
                {"1.Редактировать",item},
                {"2.Удолить пару",item2},
                //{"3.Вернуться к словорю", Back()},
                //{"4.Вернутсья к главному меню",ShowMenu }
            };

            TestMenuStruct start = new(Links, list);


            //Dictionary<string, GetDelegate.CommandHandler> Linkss = new Dictionary<string, GetDelegate.CommandHandler>()
            //{

            //    {"1.добавить слово", AddWord },
            //    {"2.Поиск слова", readWord},
            //    {"3.Редактировать", SubMenu},
            //    {"5.Выход", ToExit}

            //};

            //MenuDefolt menuDefolt = new(Linkss, true);
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

        public static void AddWord() 
        {                              
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

            StartMenuWork start = new(Links);

        }

        public static void ToExit()
        {
            Environment.Exit(0);

        }
    }
}
