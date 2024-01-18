using LearnMsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public static class MyConsole
    {
        public static string  MyReadLine() // Нужен серьезный тест
        {
            // Есть какая то странная пробелма при попытке записать ConsoleKeyInfo myKey.KeyChar в char

            int inY = Console.CursorTop;
            string str = "";
            string ch = string.Empty ;
            int count = 0;
            ConsoleKeyInfo myKey; // Я очень плохо понимаю эту струтуру 
            Console.SetCursorPosition(0, inY);
            while (true)
            {
                Console.SetCursorPosition(count, inY);
                myKey = Console.ReadKey(true);

                switch (myKey.Key)
                {
                    case ConsoleKey.Enter:
                        return str;
                        break;
                    case ConsoleKey.LeftArrow:

                        LeftGO(count, inY, out count);
                        break;
                    case ConsoleKey.RightArrow:
                        RightGO(count, str.Length, inY, out count);
                        break;
                    case ConsoleKey.Backspace:
                        
                        BackspaceGO(count, str, inY,out str, out count);
                        break;
                    case ConsoleKey.Escape:

                        MenuHistori.GotMenuHistore();
                        break;
                    default:
                        
                        DefaultGO(count, str, myKey, inY,out str, out count);
                        break;
                }
            }
            return str;
        }

        public static void LeftGO(int count,int inY, out int Outcount)
        {
            if (count == 0) Outcount = count;
            else count--;
            Outcount = count;
        }

        public static void RightGO(int count, int lenght, int inY, out int Outcount)
        {
            if (count == lenght) Outcount = count;
            else count++;
            Outcount = count;
        }

        public static void BackspaceGO(int count, string str,int inY, out string OutStr, out int outCount)
        {
            OutStr = str;
            if (count == 0) outCount = count;
            else
            {
                count--;
                OutStr = str.Remove(count, 1);
                outCount = count;
                CursorMove.ClearLines(inY, inY, 1);
                Console.SetCursorPosition(0, inY);
                Console.Write(OutStr);
            }
        }

        public static void DefaultGO(int count, string str, ConsoleKeyInfo myKey, int inY,out string outStr, out int outCount)
        {
            CursorMove.ClearLines(inY, inY, 1);
            Console.SetCursorPosition(0, inY);
            string ch = string.Empty;
            ch += myKey.KeyChar;
            outStr = str.Insert(count, ch);
            count++;
            outCount = count;
            Console.Write(outStr);
        }
    }
}


//public static string MyReadLine() // Нужен серьезный тест
//{
//    // Есть какая то странная пробелма при попытке записать ConsoleKeyInfo myKey.KeyChar в char

//    int inY = Console.CursorTop;
//    string str = "aboba";
//    string ch = string.Empty;
//    // bool w = true;
//    int count = 0;
//    ConsoleKeyInfo myKey; // Я очень плохо понимаю эту струтуру 

//    while (true)
//    {
//        CursorMove.ClearLines(inY, inY, 1);
//        Console.SetCursorPosition(0, inY);
//        Console.Write(str);
//        Console.SetCursorPosition(count, inY);
//        myKey = Console.ReadKey(true);

//        switch (myKey.Key)
//        {
//            case ConsoleKey.Enter:
//                return str;
//                break;
//            case ConsoleKey.LeftArrow:
//                if (count == 0) break;
//                count--;
//                break;
//            case ConsoleKey.RightArrow:
//                //if (count == str.Length) break;
//                //count++;
//                RightGO(count, str.Length, out count);
//                break;
//            case ConsoleKey.Backspace:
//                //if (count == 0) break;
//                //count--;
//                //str = str.Remove(count, 1);
//                BackspaceGO(count, str, out str, out count);
//                break;
//            case ConsoleKey.Escape:
//                MenuHistori.GotMenuHistore();
//                break;
//            default:
//                //ch += myKey.KeyChar;
//                //str = str.Insert(count, ch);
//                //count++;
//                //ch = string.Empty;
//                DefaultGO(count, str, myKey, out str, out count);
//                break;
//        }
//    }
//    return str;
//}

// Вариант без лага курсора 

//public static string MyReadLine() // Нужен серьезный тест
//{
//    // Есть какая то странная пробелма при попытке записать ConsoleKeyInfo myKey.KeyChar в char

//    int inY = Console.CursorTop;
//    string str = "";
//    string ch = string.Empty;
//    // bool w = true;
//    int count = 0;
//    ConsoleKeyInfo myKey; // Я очень плохо понимаю эту струтуру 
//    Console.SetCursorPosition(0, inY);
//    while (true)
//    {

//        // CursorMove.ClearLines(inY, inY, 1); // можно это перенести в бэкспейс - зачем повторять при каждом проходе
//        //Console.SetCursorPosition(0, inY);
//        //Console.Write(str);
//        Console.SetCursorPosition(count, inY);
//        myKey = Console.ReadKey(true);

//        switch (myKey.Key)
//        {
//            case ConsoleKey.Enter:
//                return str;
//                break;
//            case ConsoleKey.LeftArrow:

//                LeftGO(count, inY, out count);
//                break;
//            case ConsoleKey.RightArrow:
//                RightGO(count, str.Length, inY, out count);
//                break;
//            case ConsoleKey.Backspace:

//                BackspaceGO(count, str, inY, out str, out count);
//                break;
//            case ConsoleKey.Escape:

//                MenuHistori.GotMenuHistore();
//                break;
//            default:

//                DefaultGO(count, str, myKey, inY, out str, out count);
//                break;
//        }
//    }
//    return str;
//}

//public static void LeftGO(int count, int inY, out int Outcount)
//{
//    if (count == 0) Outcount = count;
//    else count--;
//    Outcount = count;
//}

//public static void RightGO(int count, int lenght, int inY, out int Outcount)
//{
//    if (count == lenght) Outcount = count;
//    else count++;
//    Outcount = count;
//}

//public static void BackspaceGO(int count, string str, int inY, out string OutStr, out int outCount)
//{
//    CursorMove.ClearLines(inY, inY, 1);
//    Console.SetCursorPosition(0, inY);
//    OutStr = str;
//    if (count == 0) outCount = count;
//    else
//    {
//        count--;
//        OutStr = str.Remove(count, 1);
//        outCount = count;
//    }
//    Console.Write(OutStr);
//}

//public static void DefaultGO(int count, string str, ConsoleKeyInfo myKey, int inY, out string outStr, out int outCount)
//{
//    CursorMove.ClearLines(inY, inY, 1);
//    Console.SetCursorPosition(0, inY);
//    string ch = string.Empty;
//    ch += myKey.KeyChar;
//    outStr = str.Insert(count, ch);
//    count++;
//    outCount = count;
//    Console.Write(outStr);
//}