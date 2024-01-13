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
        public static void RedactionWord(Word word) // Тестовый вариант при полной сборке билда, Допустить вариант не передачи слова
        {
            int inY = Console.CursorTop;
            string p;
            string r;
            word.GetName(out r, out p);
            int maxLenght;
            int rightItnerval;
            int pX = 0;
            int inx = 0;
            bool loop = true;
            StringBuilder fullString = new($"{r} - {p}");
            Console.Write(fullString);
            Console.SetCursorPosition(0, inY);
            while (loop)
            {
                rightItnerval = r.Length + 3;
                maxLenght = rightItnerval + p.Length - 1;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        loop = false;
                        Word newWord = new(word.IDword, p, r);
                        Message(newWord);
                        break;
                    case ConsoleKey.Backspace:
                        if (inx >= rightItnerval) p = p.Remove(pX, 1);
                        else r = r.Remove(inx, 1);
                        Console.SetCursorPosition(0, inY);
                        CursorMove.ClearLines(inY, inY, 1);
                        Console.Write($"{r} - {p}");
                        if (inx == r.Length || inx == maxLenght) inx--;
                        if (inx == maxLenght) pX--;
                        Console.SetCursorPosition(inx, inY);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (inx == 0) break;
                        if (inx == rightItnerval) inx = r.Length;
                        if (inx > rightItnerval) pX--;
                        inx--;
                        Console.SetCursorPosition(inx, inY);
                        break;
                    case ConsoleKey.RightArrow:
                        if (inx == maxLenght) break;
                        inx++;
                        if (inx == r.Length) inx = rightItnerval;
                        if (inx > rightItnerval) pX++;
                        Console.SetCursorPosition(inx, inY);
                        break;
                }
            }

        }
        public static void Message(Word newWord) // Тест, лишние есть
        {
            // link2.backMenu =  // Надо както передать сюда меню :(!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Console.WriteLine("Уверены что хотите сохранить изменения ?");

            List<BaseInfNode> list = new List<BaseInfNode>();

            NodeEditWord link1 = new("1.Да", newWord, CallUpdateWord);
            list.Add(link1);
            NodeEditWord link2 = new("2.Нет - недописан", newWord, Program.SubMenu);
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
