using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slovar.StaticClass;

namespace Slovar.Abstracts
{
    internal class NewStartMenu
    {
        // static int size { get; set; }
        List<BaseInfNode> list { get; set; }
        MenuSettings setting { get; set; }
        public NewStartMenu(List<BaseInfNode> list) : this(list, null) { }
        public NewStartMenu(List<BaseInfNode> list, MenuSettings setting)
        {
            this.list = list;
            this.setting = setting;
            //size = this.list.Count;
            this.setting = setting;
            menuStart();
        }
        public void menuStart()
        {
            Element[] elems = FillingElmens();
            
            // Программа понимает что надо реализовывать метод указанный в классе оюъекта инициализации !!!! (Над очетко понять как работает !)
            setSetting(); // если передал пустой то генерится оыбчное настройки
            Menu menu = new Menu(elems, setting);
            SelectMenu(ref menu);
        }

        public Element[] FillingElmens()
        {
            Element[] elems = new Element[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                elems[i] = new Element(list[i]);
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
                    case ConsoleKey.Escape:
                        MenuHistori.GotMenuHistore();
                        break;
                    default: return;
                }
            }
        }

        public void setSetting()
        {
            setting ??= new MenuSettings();
        }
    }
}
