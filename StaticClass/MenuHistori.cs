using System;
using Slovar.Abstracts;

namespace Slovar.StaticClass
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
        // Решено что всегда что то будет записыаться в стек
        public static void GotMenuHistore()
        {
            Console.Clear();
            NodeMenuHistore menuH;
            if (stac.Count == 1) menuH = stac.Peek();
            else
            {
                stac.Pop();
                if (stac.Count == 1) menuH = stac.Peek();
                else menuH = stac.Pop();
            }
            NewStartMenu menu = new(menuH.list, menuH.setting);
        }

        public static void voidForEach()
        {
            Console.WriteLine(stac.Count);
        }
    }
}
