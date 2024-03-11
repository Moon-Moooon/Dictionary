using Microsoft.Data.SqlClient;
using System;
using Slovar.Abstracts;
using Slovar.Lenguages;
using Slovar.UserJson;
using Slovar.Entity;

namespace Slovar.StaticClass
{
    public class MenuSet
    {
        public static void ShowMenu()
        {
            MenuHistori.HistoriClear(); // Не уверен что это красиво, *1

            List<BaseInfNode> listNode = new List<BaseInfNode>()
            {
                new NodeAction($"1.Изменить словарь, сейчас установлен {Settings.SetupDictionary[0]}-{Settings.SetupDictionary[1]} ",  MenuDictionaritys.Go),
                new NodeAction("2.Добавить слово", AddWord),
                // Нужна проверка состояния снизу
                new NodeAction("3.Поиск слова", SearchWordInDB),
                new NodeAction("4.Тренировка знаний", stub),
                new NodeAction("5.Выход", ToExit)
                
            };

            MenuHistori.Add(new(listNode)); // Тест

            MenuSettings menuSettings = new MenuSettingBullider().Build();

            NewStartMenu start = new(listNode);
            
        }

        public static void SearchWordInDB()
        {
            var listLengs =  Json.GetListLenguages();

            List<BaseInfNode> list = new List<BaseInfNode>();
            
            for (int i = 0; i < listLengs.Count; i++)
            {
                list.Add(new NodeAction<string>($"1.Поиск по языку{listLengs[i]}", listLengs[i], TestToDb));
            }
            
            MenuHistori.Add(new(list));
            // При возвращении и новому поиску есть ошибка! т.к. Не закрыт поток вещания при вызове sqlReader
            
            NewStartMenu menu = new(list);
        }

        public static void TestToDb(string s)
        {
            
        }
        
        public static void stub()
        {
            List<BaseInfNode> list = new List<BaseInfNode>()
            {
                new NodeAction("Это заглушка :) ! А значит что то не дописано", ShowMenu)
            };

            NewStartMenu menu = new(list);
        }

        // большая пролема что при возвращении объекта типа DateRider мы не оканчиваем sqlConnection
        // из за чего мы не можем зана возвращаючь по новигации ESC делать новый запрос на поиск
        // аПодобная реализация хороший вариант чтоб при декомпиляции можно было увидеть какие преобразования происходят когда вместо соответ типа переменные слова пичем в object

        // public static void readWord(string leng) // Не дописан -- стоит лучше продумать как к нему возвращаться и стоит ли вообще
        // {
        //     // Интересно как это выглядит когда я переменную типа Object передаю форматированной стракой как stringt, скорее всего там под капотный боксинг :(
        //     Console.Write($"Введите слово на {leng}\n >");
        //     string stringSercch = MyConsole.MyReadLine();
        //     Console.WriteLine();
        //     if (leng == "rus") ReviewLengs.ExeminationRusWord(stringSercch); // говно, надо переделывать 
        //     else ReviewLengs.ExeminationPolWord(stringSercch);
        //
        //     SqlDataReader Date = DBModificatet.SelectWord(stringSercch, leng); // А если запрос будет со словами 
        //     List<BaseInfNode> list = new();
        //     int counRows = 0;
        //
        //     if (!Date.HasRows)
        //     {
        //         Date.Close();
        //         BulShiiiit(stringSercch);
        //         // Надо что то вводить если нет символов   
        //     }
        //
        //     while (Date.Read())
        //     {
        //         counRows++;
        //         int idWord = Date.GetInt32(0);
        //
        //         string rusWord = Date.GetString(1);
        //
        //         string polName = Date.GetString(2);
        //
        //         string WordRow = $"{rusWord} - {polName}";
        //         Word word = new Word(idWord, rusWord, polName);
        //         NodeAction<Word> newWord = new(WordRow, word, SubMenu);
        //         list.Add(newWord);
        //     }
        //
        //     Date.Close(); // необходимо закрыть для корректной работы 
        //
        //     NodeMenuHistore fullMetod = new(list, new MenuSettingBullider()
        //         .ExecuteClear(false)
        //         .Build()); // Тест, тут без строк
        //     MenuHistori.Add(fullMetod);
        //
        //     NewStartMenu menu = new(list, new MenuSettingBullider()
        //         .NumberOfLinsUp(2)
        //         .ExecuteClear(false)
        //         .Build());
        // }

        public static void SubMenu(Word word) // не корректно происходит вложение под меню
        {
            List<BaseInfNode> list = new List<BaseInfNode>()
            {
                new NodeAction<Word>("1.Редактировать", word, RedactionWord.Redaction),
                new NodeAction("2.Удолить", stub),
                new NodeAction("3.Вернуться к словорю", stub),
                new NodeAction("4.Вернутсья к главному меню", stub)
            };


            NodeMenuHistore node = new(list, new MenuSettingBullider()
                .TabElemens(true)
                .Build()); // Тест

            NewStartMenu menu = new(list, new MenuSettingBullider()
                .TabElemens(true)
                .Build());
        }

        public static void BulShiiiit(string str)
        {
            Console.WriteLine($"Слов со стракой '{str}' - небыло найдено.\n Хотите попробовать ?");

            List<BaseInfNode> list = new List<BaseInfNode>()
            {
                // Проблема!
                new NodeAction("1.Да", SearchWordInDB),
                new NodeAction("2.Нет", ShowMenu)
            };

            //MenuHistori.Add(new(list, new MenuSettingDefolt(2))); // Тут вообщето есть строка лол
            // Нужгодописать настройку

            MenuSettings menuSettings = new MenuSettingBullider()
                .NumberOfLinsUp(2)
                .Build();

            NewStartMenu menu = new(list, new MenuSettingBullider()
                .NumberOfLinsUp(2)
                .Build());
        }

        public static void AddWord()
        {
            string firstWord;
            string secondWord;

            #region

            Console.WriteLine(
                $"Для добавления нового слова в словарь вам необходимо ввести слова на {Settings.SetupDictionary[0]} и на {Settings.SetupDictionary[1]}");
            Console.WriteLine($"Введите слово на {Settings.SetupDictionary[0]}");
            firstWord = MyConsole.MyReadLine();
            ReviewLengs.Go(firstWord, Settings.SetupDictionary[0]);

            Console.WriteLine("");
            Console.WriteLine($"Введите слово на {Settings.SetupDictionary[1]}");
            secondWord = MyConsole.MyReadLine();
            ReviewLengs.Go(secondWord, Settings.SetupDictionary[1]);

            Console.WriteLine("");
            Console.WriteLine($"Добавить новую пару слов: '{firstWord}' с переводом '{secondWord}'?");
            
            // работает до сюда :) 
            
            List<BaseInfNode> list = new List<BaseInfNode>()
            {
              //  new NodeAction<string, string>("1.Да", firstWord, secondWord, AddCoupInDb),
                new NodeAction("2.Нет", ShowMenu)
            };

            NewStartMenu menu = new(list, new MenuSettingBullider()
                .NumberOfLinsUp(6)
                .Build());

            #endregion
        }

        public static void AddCoupInDb()
        {
            
        }
        
        // public static void getAfterBedCHois(char warningChar)
        // {
        //     Console.WriteLine($"Символ {warningChar} - является некорректным или не на соответствующем языке." +
        //                       $"\n Хотите попробовать ?");
        //
        //     List<BaseInfNode> list = new List<BaseInfNode>()
        //     {
        //         new NodeAction("1.Да", SearchWordInDB),
        //         new NodeAction("2.Нет", ShowMenu)
        //     };
        //
        //     //MenuHistori.Add(new(list, new MenuSettingDefolt(2))); // Тут вообщето есть строка лол
        //
        //     NewStartMenu menu = new(list, new MenuSettingBullider()
        //         .NumberOfLinsUp(2)
        //         .Build());
        // }

        public static void ToMenu(string line, Word word)
        {
            Console.WriteLine($"Слово -  '{line}'  пустое");

            List<BaseInfNode> list = new()
            {
                new NodeAction<Word>("1.Редактирвоать пару еще раз", word, RedactionWord.Redaction),
                new NodeAction("2.Главное меню", MenuSet.ShowMenu)
            };
            // Проблема!

            NewStartMenu menu = new(list, new MenuSettingBullider()
                .NumberOfLinsUp(1)
                .Build());
        }

        public static void ToExit()
        {
            Environment.Exit(0);
        }
    }
}
