using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    // Позволяет формировать последовательнсоть вызово методов, а затем возвращаться последовательно назад
    public static class MenuHistori
    {
        static Stack<NodeMenuHistore> stac = new Stack<NodeMenuHistore>();

        public static void Add(NodeMenuHistore menu)
        {
            stac.Push(menu);
        }

        public static void HistoriClear() 
        {
            stac.Clear();
        }

        public static void GotMenuHistore()
        {
            Console.Clear(); 
            NodeMenuHistore menuH;
            if (stac.Count < 2) menuH = stac.Peek(); 
            else menuH = stac.Pop();
            NewStartMenu menu = new(menuH.list, menuH.setting);
        }

        public  static void voidForEach() 
        {
            Console.WriteLine(stac.Count);
        }
    }
}
