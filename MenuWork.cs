using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Пока будет плохое решение - Значание bool которое будет указывать как чистить консоль по исполнению комманды будет хранится в каждом экземплярк элемента меню :(

// Нужно переосмыслить подход к формированию меню !!!
// При нажатии Enter считывать как меcто для открытия вложения при вызове словаря
// Отказался от метода ингициализации меню потому что параметры можно установать по Дефолту! 
// index в классе меню уже означает положение полджение с верху 0 и внизу n


namespace LearnMsSql
{
    internal class MenuWork
    {
        public static int size { get; set; }
        int posX { get; set; }
        int posY { get; set; }
        int NumberOfLins { get; set; }
        public bool ExecuteClear;
        // static Element[] elems { get; set; }
        public Dictionary<string, GetDelegate.CommandHandler> Dic { get; set; }
        public MenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction) : this(Diction, true) { } // Нельзя указывать магисеское число !
        public MenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear) : this(Diction, true, 0) { }
        public MenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear, int NumberOfLins)
        {
            this.NumberOfLins = NumberOfLins;
            this.Dic = Diction;
            this.ExecuteClear = ExecuteClear;
            posX = Console.CursorLeft;
            posY = Console.CursorTop;
            size = Dic.Count;
            menuStart();
        }

        private void menuStart()
        {
            Element[] elems = new Element[size];
            int counnter = 0;
            if (counnter < size)
            {
                foreach (var item in this.Dic)
                {
                    elems[counnter] = new Element(item.Key, item.Value, ExecuteClear);
                    counnter++;
                }
            }

            Menu menu = new Menu(elems);
            while (true)
            {
                menu.Draw();

                Console.SetCursorPosition(posX, posY);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.SelectPrev();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.SelectNext();
                        break;
                    case ConsoleKey.Enter:
                        if(!ExecuteClear) { menu.SideForMenu(NumberOfLins); }
                        menu.ExecuteSelected();
                        break;
                    default: return;
                }
            }
        }
            
        private class Menu
        {
            public Element[] Elements { get; set; }
            public int Index { get; set; }
            public int EmptyX { get; set; } //Эксперемент
            public int EmptyY { get; set; } //Эксперемент
            public Menu(Element[] elems)
            {
                this.Index = 0;
                this.Elements = elems;
                Elements[Index].IsSelected = true;
            }
            public void SideForMenu(int AddTop) // Не продуманный до конца метод!
            {
                // дял вычисления полной высоты нам нужно знать позицию гдебыло нажато Enter - эьо индексатор, а то что было напечатоно сверху это будет additionalTop
                int intX = 0;

                int SorseTop = Index + AddTop + 1;

                int TargetTop = SorseTop + 4; // магическое число - число на которое опускаем, пока хз как сюда передать кол.во комманд. Наверное когда будет метод с ними :)

                //CursorMove.CursorPosit(out intX, out intY);
                int NumbRows = AddTop + Elements.Length - SorseTop;
                CursorMove.TextMoev(NumbRows, SorseTop, TargetTop);
                Console.SetCursorPosition(intX, SorseTop);
                Program.SubMenu();
               // Console.SetCursorPosition(intX, intY);
            }
            public void Draw()
            {
              //  Console.Clear();
                foreach (var element in Elements)
                {
                    element.Print();
                }
            }

            public void SelectNext()
            {
                if (Index == Elements.Length - 1) return;
                Elements[Index].IsSelected = false;
                Elements[++Index].IsSelected = true;
            }

            public void SelectPrev()
            {
                if (Index == 0) return;
                Elements[Index].IsSelected = false;
                Elements[--Index].IsSelected = true;
            }

            public void ExecuteSelected()
            {
                Elements[Index].Execute();
            }
        }

        private class Element
        {
            public string Text { get; set; }
            public ConsoleColor SelectedForeColor { get; set; }
            public ConsoleColor SelectedBackColor { get; set; }
            public bool IsSelected { get; set; }
            public bool ExecuteClear { get; set; } 

            public GetDelegate.CommandHandler Command;

            public Element(string text, GetDelegate.CommandHandler Comm, bool ExExecuteClear)
            {
                this.ExecuteClear = ExExecuteClear;
                this.Command = Comm;
                this.Text = text;
                this.SelectedForeColor = ConsoleColor.Black;
                this.SelectedBackColor = ConsoleColor.Gray;
                this.IsSelected = false;
            }

            public void Print()
            {
                if (this.IsSelected)
                {
                    Console.BackgroundColor = this.SelectedBackColor;
                    Console.ForegroundColor = this.SelectedForeColor;
                }
                Console.WriteLine(this.Text);
                Console.ResetColor();
            }

            public void Execute()
            {
                if (Command == null) return;
                if (ExecuteClear) Console.Clear();
                Command.Invoke();
            }
        }

    }
}