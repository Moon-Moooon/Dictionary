using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public static class CursorMove
    {
        public static void CursorPosit(out int intX, out int intY)
        {
            int inX = Console.CursorLeft;
            intX = inX;
            int inY = Console.CursorTop;
            intY = inY;
        }

        public static void TextMoev(int line)
        {
            int inX;

            int inY;

            int sourceHeight;

            int targetTop;

            CursorPosit(out inX, out inY);

            sourceHeight = inY - line;

            targetTop = line + 1;

            Console.MoveBufferArea(0, line, Console.BufferWidth, sourceHeight, 0, targetTop);

            Console.SetCursorPosition(0, line);
        }

        public static void ClearLines(int top, int lower, int rows)
        {
            Console.MoveBufferArea(0, top, Console.BufferWidth, rows, Console.BufferWidth, lower, ' ', ConsoleColor.White, ConsoleColor.Black);
            // Если трогать окно строки ползают и могут менять свое положение 
        }
    }
}
