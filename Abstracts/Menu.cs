﻿using Slovar.StaticClass;


namespace Slovar.Abstracts
{
    public class Menu
    {
        public Element[] Elements { get; set; }
        MenuSettings setting { get; }
        public int Index { get; set; }
        internal Menu(Element[] elems, MenuSettings setting)
        {
            this.setting = setting;
            Index = 0;
            Elements = elems;
            Elements[Index].IsSelected = true;
        }
        public void SideForMenu(int AddTop) // ++\- 
        {

            // дял вычисления полной высоты нам нужно знать позицию гдебыло нажато Enter - эьо индексатор, а то что было напечатоно сверху это будет additionalTop
            int intX = 0;

            int SorseTop = Index + AddTop + 1;

            int TargetTop = SorseTop + 4; // магическое число - число на которое опускаем, пока хз как сюда передать кол.во комманд. Наверное когда будет метод с ними :)

            //CursorMove.CursorPosit(out intX, out intY);
            int NumbRows = AddTop + Elements.Length - SorseTop;
            CursorMove.TextMoev(NumbRows, SorseTop, TargetTop);
            Console.SetCursorPosition(intX, SorseTop);
        }

        public void Draw()
        {
            setting.SetCursorMenu();
            int counter = 0;

            foreach (var element in Elements)
            {
                // Установка курсора чтобы меню реовалось на заданном месте
                setting.SetCursorElem(counter);
                counter++;

                element.Print();
            }
            MenuHistori.voidForEach(); // Для теста
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
            if (setting.ExecuteClear) Console.Clear();
            else SideForMenu(setting.NumberOfLinsUp);
            Elements[Index].Execute();
        }
    }

    public class Element
    {
        public BaseInfNode obj { get; set; }
        public ConsoleColor SelectedForeColor { get; set; }
        public ConsoleColor SelectedBackColor { get; set; }
        public bool IsSelected { get; set; }

        public Element(BaseInfNode obj)
        {
            this.obj = obj;
            SelectedForeColor = ConsoleColor.Black;
            SelectedBackColor = ConsoleColor.Gray;
            IsSelected = false;
        }
        public void Print()
        {
            if (IsSelected)
            {
                Console.BackgroundColor = SelectedBackColor;
                Console.ForegroundColor = SelectedForeColor;
            }

            Console.WriteLine(obj.Text);

            Console.ResetColor();

        }

        public void Execute()
        {
            obj.InvokeDeleg();
        }
    }

}

