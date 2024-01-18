using LearnMsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public class Word
    {
        public int IDword { get; set; }
        public string RusName { get; set;}
        public string PolName { get; set; }

        public Word(int IDword, string RusName, string PolName)
        {
            this.IDword = IDword;
            this.RusName = RusName;
            this.PolName = PolName;
        }

        public void Print()
        {
            Console.WriteLine($"Русский вариант - Польский вариант \n{RusName} - {PolName}");
        }

        public string doubl()
        {
            string s = $"{RusName} - {PolName}";
            return s ;
        }

        public void GetName(out string RusName, out string PolName )
        {
            PolName = this.PolName;
            RusName = this.RusName;
        }

        public void GetInfo(out int IDWord,out string RusName, out string PolName)
        {
            IDWord = this.IDword;
            PolName = this.PolName;
            RusName = this.RusName;
        }
        // Надо хорошо подумать как это лучше сделать !
        public static void RedactionWord(Word word) // Нельзя вводить вообще ничего же - бред!
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
                        if (curs == RightSide) curs  = r.Length + 1; // 1 нужно для более корректного перехода 
                        curs--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (curs == maxLenght) break;
                         if (curs == r.Length) curs = RightSide - 1; // 1 нужно для более корректного перехода 
                        curs++;
                        
                        break;
                    case ConsoleKey.Escape:
                        MenuHistori.GotMenuHistore(); // Тест
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
        public static void Message(Word newWord) // Тест нужен
        {
            Console.WriteLine("Уверены что хотите сохранить изменения ?");

            List<BaseInfNode> list = new List<BaseInfNode>();

            NodeEditWord link1 = new("1.Да", newWord, CallUpdateWord);
            list.Add(link1);
            NodeEditWord link2 = new("2.Нет - недописан", newWord, RedactionWord);
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


//public static void RedactionWord(Word word) // Нельзя вводить вообще ничего же - бред!
//{
//    int inY = Console.CursorTop;
//    string p;
//    string r;
//    word.GetName(out r, out p);
//    int maxLenght;
//    int rightItnerval;
//    int pX = 0;
//    int inx = 0;

//    int inR = 0;
//    int inP = 0;
//    int curX = 0;
//    // bool loop = true;
//    StringBuilder fullString = new($"{r} - {p}");
//    Console.Write(fullString);
//    Console.SetCursorPosition(0, inY);
//    while (true)
//    {
//        Console.SetCursorPosition(inx, inY);
//        rightItnerval = r.Length + 3; // переработать
//        maxLenght = rightItnerval + p.Length - 1; //-
//        switch (Console.ReadKey(true).Key)
//        {
//            case ConsoleKey.Enter:
//                //loop = false;
//                Word newWord = new(word.IDword, p, r);
//                Message(newWord);
//                break;
//            case ConsoleKey.Backspace:
//                if (inx == 0) break;
//                else
//                {
//                    inx--;
//                    if (inx >= rightItnerval) p = p.Remove(pX, 1);
//                    else r = r.Remove(inx, 1);
//                }

//                //if (inx >= rightItnerval) p = p.Remove(pX, 1);
//                //else r = r.Remove(inx, 1);
//                Console.SetCursorPosition(0, inY);
//                CursorMove.ClearLines(inY, inY, 1);
//                Console.Write($"{r} - {p}");
//                //if (inx == r.Length || inx == maxLenght) inx--; // -
//                //if (inx == maxLenght) pX--; // -
//                //Console.SetCursorPosition(inx, inY);
//                break;
//            case ConsoleKey.LeftArrow:
//                if (inx == 0) break; // if inR == 0 break 
//                if (inx == rightItnerval) inx = r.Length; // if inP == 0 { inR - 3}
//                if (inx > rightItnerval) pX--;
//                inx--;
//                //Console.SetCursorPosition(inx, inY);
//                break;
//            case ConsoleKey.RightArrow:
//                if (inx == maxLenght) break; // inP == p.lenght break
//                inx++;                       // if inR == r.lenght, {inR + 3, }
//                                             // if inR > r.lenght inP++
//                if (inx == r.Length) inx = rightItnerval;
//                if (inx > rightItnerval) pX++;

//                break;
//            case ConsoleKey.Escape:
//                MenuHistori.GotMenuHistore(); // Тест
//                                              // Тут должен происходить откат до Слов 
//                break;
//        }
//    }

//}