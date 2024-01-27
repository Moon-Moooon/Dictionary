using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    internal class NewStartMenu
    {
        static int size { get; set; }
        List<BaseInfNode> list { get; set; }
        MenuSettengs setting { get; set; } // Для проверки на нулл и устанвку тест
        public NewStartMenu(List<BaseInfNode> list) : this(list,null) { }
        public NewStartMenu(List<BaseInfNode> list, MenuSettengs setting)
        {
            this.list = list;
            this.setting = setting;
            size = this.list.Count;
            this.setting = setting;
            menuStart();
        }
        public virtual void menuStart()
        {
            Element[] elems = FillingElmens(); // Программа понимает что надо реализовывать метод указанный в классе оюъекта инициализации !!!! (Над очетко понять как работает !)
            setSetting(); // если передал пустой то генерится оыбчное настройки
            Menu menu = new Menu(elems, setting);
            SelectMenu(ref menu);
        }

        public virtual Element[] FillingElmens()
        {
            Element[] elems = new Element[size];

            for (int i = 0; i < size; i++) 
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
            setting??= new MenuSettingDefolt();
        }
    }
}
