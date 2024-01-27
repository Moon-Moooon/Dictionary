using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public  class MenuSet
    {
        public static void ShowMenu()
        {
            // Полная очистка Истории меню
            MenuHistori.HistoriClear(); // Не уверен что это красиво, *1

            List<BaseInfNode> listNode = new List<BaseInfNode>();

            NodeCommandHandler link1 = new("1.добавить слово", AddWord); // Хранятся в списке с типо родителя как типы наследников !!!!!
            listNode.Add(link1);
            NodeCommandHandler link2 = new("2.Поиск слова", SearchWordInDB);
            listNode.Add(link2);
            NodeCommandHandler link3 = new("3.Тренировка знаний", stub);
            listNode.Add(link3);
            NodeCommandHandler link4 = new("4.Выход", ToExit);
            listNode.Add(link4);

            MenuHistori.Add(new(listNode)); // Тест

            MenuSettings menuSettings = new MenuSettingBullider().Build();

            NewStartMenu start = new(listNode);

        }

        public static void SearchWordInDB()
        {
            Console.WriteLine("Поиск слова осуществлять на: ");

            List<BaseInfNode> list = new List<BaseInfNode>();

            NodeActionstring link1 = new("1.Поск на русском", "rus", readWord);
            list.Add(link1);
            NodeActionstring link2 = new("2.Поиск на польском", "pol", readWord);
            list.Add(link2);

           // MenuHistori.Add(new(list)); // Тест

            NewStartMenu menu = new(list); //0,0
        }

        public static void stub()
        {
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeCommandHandler link1 = new("Это заглушка :) ! А значит что то не дописано", stub);
            list.Add(link1);

            NewStartMenu menu = new(list); // 0,0
        }

        // большая пролема что при возвращении объекта типа DateRider мы не оканчиваем sqlConnection
        // из за чего мы не можем зана возвращаючь по новигации ESC делать новый запрос на поиск
        public static void readWord(string leng) // Не дописан -- стоит лучше продумать как к нему возвращаться и стоит ли вообще
        {
            // Интересно как это выглядит когда я переменную типа Object передаю форматированной стракой как stringt, скорее всего там под капотный боксинг :(
            // А что если ничего не нашли ?
            // а почему я ID не читаю
            Console.Write("Введите слово на русском\n >");
            // ChekESC(); // Проверка на ESC
            string stringSercch = MyConsole.MyReadLine();
            Console.WriteLine();
            if (leng == "rus") ExeminationRusWord(stringSercch); // говно, надо переделывать 
            else ExeminationRusWord(stringSercch);

            SqlDataReader Date = DBModificatet.SelectWord(stringSercch, leng); // А если запрос будет со словами 
            List <Word> wordToHistor;
            List<BaseInfNode> list = new List<BaseInfNode>();
            int counRows = 0;
            
            while (Date.Read())
            {
                counRows++;
                // Через DAte можно видимо сразу вызывать конкретный тип данных ?
                // нужно будет потестить по времени исполнения 
                int idWord = Date.GetInt32(0);

                string rusWord = Date.GetString(1);

                string polName = Date.GetString(2);

                // object idWord = Date.GetValue(2);
                string WordRow = $"{rusWord} - {polName}";
                Word word = new Word(0, $"{rusWord}", $"{polName}");
                NodeEditWord newWord = new(WordRow, word, SubMenu);
                list.Add(newWord);
            }
            Date.Close(); // необходимо закрыть для корректной работы 
            NodeMenuHistore node = new(list, new MenuSettingDefolt(0, false, false)); // Тест, тут без строк
            MenuHistori.Add(node);
            //Node
            NodeMenuHistore node1 = 

            NewStartMenu menu = new(list, new MenuSettingDefolt(2, false, false));
        }

        public static void SubMenu(Word word) // не корректно происходит вложение под меню
        {
            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeEditWord link1 = new("1.Редактировать", word, RedactionWord.Redaction);
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Удолить", stub);
            list.Add(link2);
            NodeCommandHandler link3 = new("3.Вернуться к словорю", stub);
            list.Add(link3);
            NodeCommandHandler link4 = new("4.Вернутсья к главному меню", stub);
            list.Add(link4);

            NodeMenuHistore node = new(list, new MenuSettingDefolt(0, true, true)); // Тест

            NewStartMenu menu = new(list,new MenuSettingDefolt(0, true, true));
        }

        public static void ExeminationPolWord(string item)
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
            string rusWord = MyConsole.MyReadLine();
            ExeminationRusWord(rusWord);
            Console.WriteLine("");
            Console.WriteLine("Введите слово на польском");
            string polWord = MyConsole.MyReadLine();
            ExeminationPolWord(polWord);
            Console.WriteLine("");
            Console.WriteLine($"Добавить новую пару слов: '{rusWord}' с переводом '{polWord}'?");

            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeAction2string link1 = new("1.Да", rusWord, polWord, AddWordInDB); // Проблема!
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Нет", ShowMenu);
            list.Add(link2);

            NewStartMenu menu = new(list, new MenuSettingDefolt(6));
            #endregion

        }

        public static void AddWordInDB(string rusWord, string polWord)
        {
            DBModificatet.WriteWordInDB(rusWord, polWord);
        }

        public static void ExeminationRusWord(string rusWord)
        {
            string word = rusWord.ToLower();

            char[] bak = new char[1];
            int i = 0;
            int j = 1;

            foreach (char item in word) // 274 слишком хардкод
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
                        Console.Clear();
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
                        Console.Clear();
                        getAfterBedCHois(item);
                    }

                }
            }

            //return word;
        }

        public static void getAfterBedCHois(char warningChar)
        {
            Console.WriteLine($"Символ {warningChar} - является некорректным или не на соответствующем языке.\n Хотите попробовать ?");

            List<BaseInfNode> list = new List<BaseInfNode>();
            NodeCommandHandler link1 = new("1.Да", AddWord); // Проблема!
            list.Add(link1);
            NodeCommandHandler link2 = new("2.Нет", ShowMenu);
            list.Add(link2);

            //MenuHistori.Add(new(list, new MenuSettingDefolt(2))); // Тут вообщето есть строка лол

            NewStartMenu menu = new(list, new MenuSettingDefolt(2));
        }

        public static void ToExit()
        {
            Environment.Exit(0);

        }
    }
}
