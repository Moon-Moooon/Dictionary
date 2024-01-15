using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    // Позволяет формировать последовательнсоть вызово методов, а затем возвращаться последовательно назад
    internal class MenuHistori
    {
        static Stack<NodeMenuHistore> stac = new Stack<NodeMenuHistore>();

        public delegate void editStac(NodeMenuHistore stac);

        public event editStac evenStack;

        // В какий то методах будет обработчик нажатия ESC -
        public static void Add(NodeMenuHistore menu)
        {

            stac.Push(menu);

        }

        public static void HistoriClear() 
        {
            stac.Clear();
        }

        public static NodeMenuHistore GetMenu()
        {
            return stac.Peek();
        }

        public static void GotMenuHistore()
        {
            NodeMenuHistore menuH = stac.Peek();
            NewStartMenu menu = new(menuH.list, menuH.NumberOfLins, menuH.ExecuteClear);
        }
    }
}
