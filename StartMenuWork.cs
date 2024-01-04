using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnMsSql
{
    public class TestStsrtMenuW<T> // Проще просто наследоваться и не делать его обстрактным
    {
        public static int size { get; set; }
        public int NumberOfLins { get; set; }
        public bool ExecuteClear; // Это булева означает при Тру = чистится консоль при переходе к команде, фолс = выполняется особый сдвиг меню ивысов под меню для редлактирования словаря 
        public Dictionary<string, T> Dic { get; set; } // Это обязхательная констурки без нею нет перехода по меню 
        // public List<Word> WordCollection { get; set; } // Только при работе со словами - не обязательная
        public TestStsrtMenuW() : this(null, 0) { }
        public TestStsrtMenuW(Dictionary<string, T> Diction) : this(Diction, 0) { }
        public TestStsrtMenuW(Dictionary<string, T> Diction, int NumberOfLins)
        {
            size = Dic.Count;
            Dic = Diction;
            this.NumberOfLins = NumberOfLins;
            // menuStart();
        }
        public static Element[] elems = new Element[size];

        public virtual void FillingElmens(ref Element[] elems) { }
        
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
                        menu.ExecuteClear(ExecuteClear); // !
                        menu.ExecuteSelected();
                        break;
                    default: return;
                }
            }
        }
    }

    public class MenuDefolt : TestStsrtMenuW<GetDelegate.CommandHandler>
    {
        public MenuDefolt(Dictionary<string, GetDelegate.CommandHandler> Diction) : base(Diction) { }
        public MenuDefolt(Dictionary<string, GetDelegate.CommandHandler> Diction, int NumberOfLins) 
        {
            base.Dic = Diction;
            base.NumberOfLins = NumberOfLins;
            menuStart();
        }

        public void menuStart() // Слова не обязательные поэтому, надо сделать разделение заполнения в случаи наличия или отсутсвия слов !!!!
        {
           // Element[] elems = new Element[size];
            FillingElmens(ref elems);
            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins };
            base.SelectMenu(ref menu);
        }

        public virtual void FillingElmens(ref Element[] elems) 
        {
            int counnter = 0;
            if (counnter < size)
            {
                foreach (var item in this.Dic)
                {
                    elems[counnter] = new Element(item.Key, item.Value, ExecuteClear);
                    counnter++;
                }
            }
        }

    }

    internal class StartMenuWork
    {

        // выбор редактирования -> человеку дается строка (можно наверное просто дать строку формата - "НаРУсском - НаПольском", Она будет форматироваться при чтении и будет обрабатываться ") -->
        // --> Нужно будет в Старт меню редактирования передовать как то класс "слово"
        public static int size { get; set; }
        //int posX { get; set; }
        //int posY { get; set; }
        int NumberOfLins { get; set; }

        public bool ExecuteClear;
        // static Element[] elems { get; set; }
        public Dictionary<string, GetDelegate.CommandHandler> Dic { get; set; }
        public List<Word> WordCollection { get; set; }
        public StartMenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction) : this(Diction, true) { }
        public StartMenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear) : this(Diction, true, 0, null) { }
        public StartMenuWork(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear, int NumberOfLins, List<Word> WordCollection) // перегрузка не продумана
        {
            this.WordCollection = WordCollection;
            this.NumberOfLins = NumberOfLins;
            this.Dic = Diction;
            this.ExecuteClear = ExecuteClear;
            // posX = Console.CursorLeft;
            // posY = Console.CursorTop;
            size = Dic.Count;
            menuStart();
        }
        
        private void menuStart() // Слова не обязательные поэтому, надо сделать разделение заполнения в случаи наличия или отсутсвия слов !!!!
        {
            Element[] elems = new Element[size]; 
            int counnter = 0;
            if (counnter < size)
            {
                foreach (var item in this.Dic)
                {
                    //elems[counnter] = new Element(item.Key, item.Value, ExecuteClear, WordCollection[counnter]); // при отсутствии передачи колекции слов, заполняя констурктор вылетает ошибка
                    elems[counnter] = new Element(item.Key, item.Value, ExecuteClear);
                    counnter++;
                }
            }

            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins };
            while (true)
            {
                menu.Draw();

               // Console.SetCursorPosition(posX, posY);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.SelectPrev();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.SelectNext();
                        break;
                    case ConsoleKey.Enter:
                        menu.ExecuteClear(ExecuteClear);
                        menu.ExecuteSelected();
                        break;
                    default: return;
                }
            }
        }
    }
}
