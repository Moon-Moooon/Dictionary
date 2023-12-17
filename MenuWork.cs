using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    internal class MenuWork
    {
        public static void MenuStart(Dictionary<string, GetDelegate.CommandHandler> Dic)
        {
            int size = Dic.Count;

            int posX = Console.CursorLeft;

            int posY = Console.CursorTop;

            Element[] elems = new Element[size];

            int counnter = 0;

            if (counnter < size)
            {
                foreach (var item in Dic)
                {
                    elems[counnter] = new Element(item.Key, item.Value);
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
            public Menu(Element[] elems)
            {
                this.Index = 0;
                this.Elements = elems;
                Elements[Index].IsSelected = true;
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

            public GetDelegate.CommandHandler Command;

            public Element(string text, GetDelegate.CommandHandler Comm)
            {
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
                Command.Invoke();
            }
        }

    }
}