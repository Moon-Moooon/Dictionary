using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Пока будет плохое решение - Значание bool которое будет указывать как чистить консоль по исполнению комманды будет хранится в каждом экземплярк элемента меню :(

// При нажатии Enter считывать как меcто для открытия вложения при вызове словаря
// index в классе меню уже означает положение полджение с верху 0 и внизу n
// Нужно в меню сделать очистку при выполнении команды
namespace LearnMsSql
{
    public class Menu
    {
        public Element[] Elements { get; set; }
        public int Index { get; set; }
        public int posX { get; set; } 
        public int posY { get; set; }
        public int NumberOfLinsUP { get; set; }
        public byte ExecuteClear { get; set; } // Тестовый варинт для работы "Esc"
        public Menu(Element[] elems)
        {
            this.Index = 0;
            this.Elements = elems;
            Elements[Index].IsSelected = true;
            posX = Console.CursorLeft;
            posY = Console.CursorTop;
        }
        public void SideForMenu(int AddTop) // ++\- Не продуманный до конца метод!
        {

            // дял вычисления полной высоты нам нужно знать позицию гдебыло нажато Enter - эьо индексатор, а то что было напечатоно сверху это будет additionalTop
            int intX = 0;

            int SorseTop = Index + AddTop + 1;

            int TargetTop = SorseTop + 4; // магическое число - число на которое опускаем, пока хз как сюда передать кол.во комманд. Наверное когда будет метод с ними :)

            //CursorMove.CursorPosit(out intX, out intY);
            int NumbRows = AddTop + Elements.Length - SorseTop;
            CursorMove.TextMoev(NumbRows, SorseTop, TargetTop);
            Console.SetCursorPosition(intX, SorseTop);
            //Program.SubMenu();
            // Console.SetCursorPosition(intX, intY);
        }

        public void Draw() // 
        {
            Console.SetCursorPosition(posX, posY); // И забыл вообще зачем это !Еще не проверено !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
        // false, 0 - Cons.clear
        // true, 2 -  SideForMenu
        // 3 - Это будет вызов команды ESC тест
        public void ExecuteSelected() // Требует теста
        {
            if (ExecuteClear == 0 || ExecuteClear == 3) Console.Clear();
            if (ExecuteClear == 2)SideForMenu(NumberOfLinsUP);
            if (ExecuteClear == 3) ;
            Elements[Index].Execute(Elements[Index]); // Требует теста
            //Elements[Index].Execute(Elements[Index]); // Требует теста
        }
    }

    public class Element // Через диструктор можно мб ламать объект и получать его команду 
    {
        //инициализация объекта информации  InfarmationNode<T> // объект с текстом и свойством указателем на исполняемый метод
        public BaseInfNode obj {  get; set; }
        public ConsoleColor SelectedForeColor { get; set; }
        public ConsoleColor SelectedBackColor { get; set; }
        public bool IsSelected { get; set; }

        public Word word { get; set; }

        public CommandHandler Command { get; set; }

        public Element(BaseInfNode obj)
        {
            this.obj = obj; // InformatioObject
            this.SelectedForeColor = ConsoleColor.Black;
            this.SelectedBackColor = ConsoleColor.Gray;
            this.IsSelected = false;
        }
        // public Element(string text, CommandHandler Comm, Word word)
        public void Print()
        {
            if (this.IsSelected)
            {
                Console.BackgroundColor = this.SelectedBackColor;
                Console.ForegroundColor = this.SelectedForeColor;
            }
            Console.WriteLine(obj.Text);
            Console.ResetColor();
        }

        public void Execute(Element element) // А как правильно даункастить
        {
            obj.InvokeDeleg();
        }
    }

}

