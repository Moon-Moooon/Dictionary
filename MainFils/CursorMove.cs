using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql.MainFils
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

        public static void TextMoev(int NumbRows, int SourceTop, int TargetTop)
        {
            Console.MoveBufferArea(0, SourceTop, Console.BufferWidth, NumbRows, 0, TargetTop);
            // 1арг - крайняя левая точка
            //2 - самая верхяя точка начала перемещения 
            //3 общие число столбцов (ширина) - бурем по всей шерене буфера консоли 
            //4 общее число строк при пемещ
            // 5 крайний слева - куда перемещаем
            //6 - самый верх - куда перемещ
            //
            // Console.SetCursorPosition(0, line);
        }

        public static void ClearLines(int top, int lower, int rows)
        {
            Console.MoveBufferArea(0, top, Console.BufferWidth, rows, Console.BufferWidth, lower, ' ', ConsoleColor.White, ConsoleColor.Black);
            // Если трогать окно строки ползают и могут менять свое положение 
        }
    }
}
