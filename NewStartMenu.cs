using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    internal class NewStartMenu
    {
        public static int size { get; set; }
        public int NumberOfLins { get; set; }
        public byte ExecuteClear { get; set; }
        List<BaseInfNode> list { get; set; }
         
        public NewStartMenu(List<BaseInfNode> list, int NumberOfLins, byte ExecuteClear)
        {
            this.list = list;
            this.ExecuteClear = ExecuteClear;
            this.NumberOfLins = NumberOfLins;
            size = this.list.Count;
            menuStart();
        }
        public virtual void menuStart()   //Надо убедитя можно ли так делать или нетЁ!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Element[] elems = FillingElmens(); // Программа понимает что надо реализовывать метод указанный в классе оюъекта инициализации !!!! (Над очетко понять как работает !)
            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins, ExecuteClear = ExecuteClear };
            SelectMenu(ref menu);
        }

        public virtual Element[] FillingElmens()
        {
            Element[] elems = new Element[size];

            for (int i = 0; i < size; i++) 
            {
                elems[i] = new Element(list[i], "i");
            }
            return elems;
        }
        public void SelectMenu(ref Menu menu) // Можно и это метод сделать виртуальными или каккой то отдельный сигмент (Происходящая очистка при выполнении нажатии) 
        {
            while (true)
            {
                menu.Draw();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.SelectPrev();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.SelectNext();
                        break;
                    case ConsoleKey.Enter:
                        menu.ExecuteSelected();
                        break;
                    default: return;
                }
            }
        }
    }
}
