using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Text;
using System.Drawing;
using Microsoft.Identity.Client;

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


    // Разделе

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

        //public static Stack<TestMenuStruct> stac = new Stack<TestMenuStruct>(6);

        //public delegate TestMenuStruct DelegMenu();
        
        //public static void test1(Stack<TestMenuStruct> stac, TestMenuStruct menu)
        //{

        //    stac.Push(menu);

        //}

        // Все выше это тест
        public static void ShowMenu()
        {
            
            Dictionary<string, CommandHandler> Links = new Dictionary<string, CommandHandler>()
            {
                {"1.добавить слово", AddWord },
                {"2.Поиск слова", readWord},
                {"5.Выход", ToExit}
            };

            // StartMenuWork start = new(Links);
            // MenuDefolt menuDefolt = new MenuDefolt(Links);

            List<BaseInfNode> listNode = new List<BaseInfNode>();

            NodeCommandHandler link1 = new("1.добавить слово", AddWord); // Хранятся в списке с типо родителя как типы наследников !!!!!
            listNode.Add(link1);
            NodeCommandHandler link2 = new("2.Поиск слова", readWord);
            listNode.Add(link2);
            NodeCommandHandler link3 = new("5.Выход", ToExit);
            listNode.Add(link3);

            NewStartMenu start = new(listNode, 0, 0);
            // Теперь надо как то передать
            Console.ReadKey();
        }

        public static void SearchWordInDB()
        {
            Console.WriteLine("Поиск слова осуществлять на: ");
            // Вызов менюшки 
            // если вызов на русском -> обработать что это русский -> Формировать запрос в БД
            // Поиск будет по не полному слвоу 
            // Есть проблема с тем что вызывая меню выбора на каком языке искать нужно по факту 2 одинаковы реализации вызова проверки слов на язык
            
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeCommandHandler link1 = new("1.Поск на русском", readWord);
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Поиск на польском", readWord);
            list.Add(link2);

            NewStartMenu menu = new(list, 0, 0);
        }

        public static void stub() 
        { 
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeCommandHandler link1 = new("Это заглушка :) ! А значит что то не дописано", stub);
            list.Add(link1);
            NewStartMenu menu = new(list,0,0);
        }

        public static void readWord() // Не дописан 
        {
            // Интересно как это выглядит когда я переменную типа Object передаю форматированной стракой как stringt, скорее всего там под капотный боксинг :(
            // Ступенчатый цикл где конечное и начальное значение изменяется 
            Console.WriteLine("Введите слово на русском\n >");
            // string word = Console.ReadLine();
            // ExeminationRusWord(word);
            var Dicti = new Dictionary<string, CommandHandler>();
            
            //var DateList = new List<string>();

            SqlDataReader Date = DBModificatet.SelectWord("к");

            List<Word> WordCollection = new List<Word>(); // Тест
            List<BaseInfNode> list = new List<BaseInfNode>();
            int counRows = 0;


            while (Date.Read()) 
            {
                counRows++;

                object rusWord = Date.GetValue(0);

                object polName = Date.GetValue(1);

                string WordRow = $"{rusWord} - {polName}";
                Word word = new Word(0, $"{rusWord}", $"{polName}");
                NodeEditWord newWord = new(WordRow, word, SubMenu); 
                list.Add(newWord);
            }

            NewStartMenu menu = new(list, 2, 2);
        }

        public static void SubMenu(Word word) // не корректно происходит вложение под меню
        {
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeEditWord link1 = new("1.Редактировать", word, Word.RedactionWord);
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Поиск на польском", readWord);
            list.Add(link2);
            NodeCommandHandler link3 = new("3.Вернуться к словорю", readWord);
            list.Add(link3);
            NodeCommandHandler link4 = new("4.Вернутсья к главному меню", readWord);
            list.Add(link4);
            NewStartMenu menu = new(list, 0, 2);
        }


        static void Main(string[] args)
        {
            ShowMenu();

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

            Dictionary<string, CommandHandler> Links = new Dictionary<string, CommandHandler>()
            {
                {"1.Да", AddWord },
                { "2.Нет", ShowMenu}
            };
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeCommandHandler link1 = new("1.Да", AddWord);
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Нет", ShowMenu);
            list.Add(link2);
            NewStartMenu menu = new(list,0,0);
        }

        public static void ToExit()
        {
            Environment.Exit(0);

        }
    }
}
