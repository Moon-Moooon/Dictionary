using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Нужно переосмыслить подход к формированию меню
// При нажатии Enter считывать как мето для открытия вложения при вызове словаря
// Скорее всего надо будет МенюСтарт в классс превращать
// Отказался от метода ингициализации меню потому что параметры можно установать по Дефолту! 
// ++ В классе Старт в автосвойстве елементс при использовании метода внутреннего Добавляются элементы но ведб свойство это обертка поля массива и почему то не просит указать четко размер массива что довольно интересно и пока не очень понятно!!
// Все хорошо ошибка есть
// Плохо понимаю видимость переменных не зависимо от модификатора когда они вложенны в классы внутри других классов
namespace LearnMsSql
{
    internal class MenuWork
    {
        public static void MenuStart(Dictionary<string, GetDelegate.CommandHandler> Dic)
        {
          //  bool ExecuteClear = eExecuteClear;

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
            
        public class Start
        {
            public static int size { get; set; }
            int posX { get; set; }
            int posY { get; set; }

            public bool StartExecuteClear = true;
            // static Element[] elems { get; set; }
            public Dictionary<string, GetDelegate.CommandHandler> Dic { get; set; }
            public Start(Dictionary<string, GetDelegate.CommandHandler> Diction) : this(Diction, true) { }
            public Start(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear)
            {
                this.Dic = Diction;
                this.StartExecuteClear = ExecuteClear;
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
            public void SideForMenu() // Не продуманный до конца метод!
            {
                int intX;

                int intY;

                CursorMove.CursorPosit(out intX, out intY);

                CursorMove.TextMoev(intY);

                Console.SetCursorPosition(intX, intY);
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
            public bool ExecuteClear { get; set; } // Недописано

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
                if (StartExecuteClear) Console.Clear(); // когда будет вывод словоря и попыткавызвать вложенное меню, все упалзет!!!
                Command.Invoke();
            }
        }

    }
}