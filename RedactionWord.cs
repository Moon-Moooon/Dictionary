using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public class RedactionWord // название должно быть редактирование пары 
    {
        public static void Redaction(Word word) // Нельзя вводить вообще ничего же - бред!
        {
            int inY = Console.CursorTop;
            string p;
            string r;
            word.GetName(out r, out p);
            int maxLenght;
            int curs = 0;
            int RightSide = 0;
            int delete = 0;

            StringBuilder fullString = new($"{r} - {p}");
            Console.Write(fullString);
            Console.SetCursorPosition(0, inY);
            ConsoleKeyInfo myKey;
            while (true)
            {

                delete = curs - RightSide;
                maxLenght = RightSide + p.Length;
                RightSide = r.Length + 3;
                Console.SetCursorPosition(curs, inY);

                myKey = Console.ReadKey(true);
                switch (myKey.Key)
                {
                    case ConsoleKey.Enter: // нужна обработка на случай если введут 1 только пробел в 1 из слови отправят


                        Word newWord = new(word.IDword, p, r);
                        Message(newWord);


                        break;
                    case ConsoleKey.Backspace:
                        if (curs == 0) break;
                        if (delete == 0) break;
                        else
                        {
                            curs--;
                            if (curs > r.Length)
                            {
                                delete += -1;
                                p = p.Remove(delete, 1);
                            }
                            else r = r.Remove(curs, 1);
                        }

                        Console.SetCursorPosition(0, inY);
                        CursorMove.ClearLines(inY, inY, 1);
                        Console.Write($"{r} - {p}");
                        break;
                    case ConsoleKey.LeftArrow:
                        if (curs == 0) break;
                        if (curs == RightSide) curs = r.Length + 1;
                        curs--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (curs == maxLenght) break;
                        if (curs == r.Length) curs = RightSide - 1;
                        curs++;

                        break;
                    case ConsoleKey.Escape:
                        MenuHistori.GotMenuHistore();
                        break;
                    default:
                        CursorMove.ClearLines(inY, inY, 1);
                        Console.SetCursorPosition(0, inY);
                        string ch = string.Empty;
                        ch += myKey.KeyChar;
                        if (curs > r.Length)
                        {
                            p = p.Insert(delete, ch);
                        }
                        else r = r.Insert(curs, ch);
                        curs++;
                        Console.Write($"{r} - {p}");

                        break;
                }
            }

        }

        public static void CheckWord(Word word)
        {
            string rusName;
            string polName;

            word.GetName(out rusName, out polName);

            string[] lines = new string[] { rusName, polName};

            //foreach (string line in lines)
            //{
            //    ChecLinesOn.Go(line);
            //}
            
            //if ( line == " ") // через регулярки можно 
            //{
            //    Console.WriteLine($"Слово - {e.Line} пустое");

            //    List<BaseInfNode> list = new List<BaseInfNode>();
            //    NodeCommandHandler link1 = new("1.Редактирвоать еще раз", Add); // Проблема!
            //    list.Add(link1);
            //    NodeCommandHandler link2 = new("2.Главное меню", ShowMenu);
            //    list.Add(link2);

            //    MenuHistori.Add(new(list, new MenuSettingDefolt(2))); // Тут вообщето есть строка лол

            //    NewStartMenu menu = new(list, new MenuSettingDefolt(2));
            //}


            //foreach (string line in lines)
            //{
            //    ChecLinesOn.Go(line);
            //}
        }

        public static void Message(Word newWord) // подобные методы вообще можно универсовализать
        {
            Console.WriteLine("Уверены что хотите сохранить изменения ?");

            List<BaseInfNode> list = new List<BaseInfNode>();

            NodeEditWord link1 = new("1.Да", newWord, CallUpdateWord);
            list.Add(link1);
            NodeEditWord link2 = new("2.Нет - недописан", newWord, Redaction);
            list.Add(link2);
        }

        public static void CallUpdateWord(Word newWord)
        {
            int IDword;
            string newRusName;
            string newPolName;
            newWord.GetInfo(out IDword, out newRusName, out newPolName);

            DBModificatet.UpdateWord(newRusName, newPolName, IDword);
        }

    }
}
