using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    internal class Test
    {





        //ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
        //    bool w = true;
        //    string t = string.Empty;

        //    do
        //    {
        //        if (readKeyResult.Key == ConsoleKey.Escape)
        //        {

        //        }
        //        else
        //        {
        //            bool r = true;
        //            w = false;
        //            t += readKeyResult.KeyChar;
        //            Console.SetCursorPosition(0, 0);
        //            Console.Write(t);
        //            while(r)
        //            {

        //            }

        //        }
        //    }
        //    while (w);


        //    if(readKeyResult.Key == ConsoleKey.Escape)
        //    {

        //    }
        //    else
        //    {
        //        t += readKeyResult.KeyChar;
        //    }
        //    Console.Write(t);
        //    t += Console.ReadLine();
        //    Console.WriteLine(t);


        //    static string ReadLineOrEsc()
        //    {
        //        string retString = "";

        //        int curIndex = 0;
        //        do
        //        {
        //            ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

        //            // handle Esc
        //            if (readKeyResult.Key == ConsoleKey.Escape)
        //            {
        //                Console.WriteLine();
        //                return null;
        //            }

        //            // handle Enter
        //            if (readKeyResult.Key == ConsoleKey.Enter)
        //            {
        //                Console.WriteLine();
        //                return retString;
        //            }

        //            // handle backspace
        //            if (readKeyResult.Key == ConsoleKey.Backspace)
        //            {
        //                if (curIndex > 0)
        //                {
        //                    retString = retString.Remove(retString.Length - 1);
        //                    Console.Write(readKeyResult.KeyChar);
        //                    Console.Write(' ');
        //                    Console.Write(readKeyResult.KeyChar);
        //                    curIndex--;
        //                }
        //            }
        //            else
        //            // handle all other keypresses
        //            {
        //                retString += readKeyResult.KeyChar;
        //                Console.Write(readKeyResult.KeyChar);
        //                curIndex++;
        //            }
        //        }
        //        while (true);
        //    }


        //        сам спросил — сам ответил.
        //вообще я хотел, чтобы консоль при нажатии на клавишу создавала готовый аргумент вроде KeyEventArgs, который потом легче обработать.
        //уже забыл про этот вопрос, случайно наткнулся на код в книжке Шилдта.

        //// спасибо герберту шилдту за код
        //// и за наше счастливое детство

        //using System;
        //using System.ComponentModel;

        //// создаем класс для обработчика
        //class myKeyEventArgs : HandledEventArgs
        //    {
        //        // нажатая кнопка
        //        public ConsoleKeyInfo key;

        //        public myKeyEventArgs(ConsoleKeyInfo _key)
        //        {
        //            key = _key;
        //        }
        //    }

        //    // класс события
        //    class KeyEvent
        //    {
        //        // событие нажатия
        //        public event EventHandler<myKeyEventArgs> KeyPress;

        //        // метод запуска события
        //        public void OnKeyPress(ConsoleKeyInfo _key)
        //        {
        //            KeyPress(this, new myKeyEventArgs(_key));
        //        }
        //    }

        //    // прога
        //    class KeyEventDemo
        //    {
        //        static void Main()
        //        {
        //            // объект события
        //            KeyEvent kevt = new KeyEvent();

        //            // кнопа
        //            ConsoleKeyInfo key;

        //            // обработчик
        //            kevt.KeyPress += (sender, e) =>
        //            {
        //                // отслеживает нажатый альт
        //                if (e.key.Modifiers == ConsoleModifiers.Alt)
        //                    Console.WriteLine(" ALT! ");

        //                // и позволяет вводить только цифры и точку
        //                char ch = e.key.KeyChar;
        //                if (!char.IsDigit(ch) && ch != '.')
        //                {
        //                    e.Handled = true;
        //                }
        //                else Console.WriteLine(" нажато: " + ch);
        //            };

        //            Console.WriteLine("вводи символы, друг");
        //            // пока точку не нажмешь
        //            do
        //            {
        //                // нажатая не отображается
        //                key = Console.ReadKey(true);
        //                // событие произошло
        //                kevt.OnKeyPress(key);
        //            }
        //            while (key.KeyChar != '.');
        //        }
        //    }

        //}

        //public static void ChekESC() // Оччень плохое решение 
        //{
        //    bool w = true;
        //    do
        //    {
        //        switch (Console.ReadKey(true).Key)
        //        {
        //            case ConsoleKey.Escape:
        //                Console.WriteLine(ConsoleKey.Escape);
        //                MenuHistori.GotMenuHistore();
        //                w = false;
        //                break;
        //            default:
        //                w = false;
        //                break;
        //        }
        //    }
        //    while (w);

        //    // return str;
        //}


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

        //class myKeyEventArgs : HandledEventArgs
        //{
        //    // нажатая кнопка
        //    public ConsoleKeyInfo key;

        //    public myKeyEventArgs(ConsoleKeyInfo _key)
        //    {
        //        key = _key;
        //    }
        //}

        //// класс события
        //class KeyEvent
        //{
        //    // событие нажатия
        //    public event EventHandler<myKeyEventArgs> KeyPress;

        //    // метод запуска события
        //    public void OnKeyPress(ConsoleKeyInfo _key)
        //    {
        //        KeyPress(this, new myKeyEventArgs(_key));
        //    }
        //}

        //// прога
        //class KeyEventDemo
        //{
        //    static void Main()
        //    {
        //        // объект события
        //        KeyEvent kevt = new KeyEvent();

        //        // кнопа
        //        ConsoleKeyInfo key;

        //        // обработчик
        //        kevt.KeyPress += (sender, e) =>
        //        {
        //            // отслеживает нажатый альт
        //            if (e.key.Modifiers == ConsoleModifiers.Alt)
        //                Console.WriteLine(" ALT! ");

        //            // и позволяет вводить только цифры и точку
        //            char ch = e.key.KeyChar;
        //            Console.WriteLine(char.IsDigit(ch));
        //            if (!char.IsDigit(ch) && ch != '.')
        //            {
        //                // e.Handled = true;
        //            }
        //            else Console.WriteLine(" нажато: " + ch);
        //        };

        //        Console.WriteLine("вводи символы, друг");
        //        // пока точку не нажмешь
        //        do
        //        {
        //            // нажатая не отображается
        //            key = Console.ReadKey(true);
        //            // событие произошло
        //            kevt.OnKeyPress(key);
        //        }
        //        while (key.KeyChar != '.');
        //    }
        //}

    }
}
