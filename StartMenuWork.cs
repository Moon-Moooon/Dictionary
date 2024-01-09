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

    /// <summary>
    ///  2Написание первого метода
    ///  3Забыл ка кработает сдвиг меню, надо хорошо закоментирвоать и сделать сдвиг - сдвигом в сторону
    ///  4Вспомнить как вообще координаты сохраняются у базового меню - вообще ничего не помню
    ///  5.Желательна полная очистка файла с мню
    public class TestStsrtMenuW<T> // Проще просто наследоваться и не делать его обстрактным
    {
        public static int size { get; set; }
        public int NumberOfLins { get; set; } // Это указывается только в тех случаях когда будет вызов подМеню
        public bool ExecuteClear { get; set; } // Это булева означает при Тру = чистится консоль при переходе к команде, фолс = выполняется особый сдвиг меню ивысов под меню для редлактирования словаря 
        public Dictionary<string, T> Dic { get; set; } // Это обязхательная констурки без нею нет перехода по меню 

        // public Element[] elems = new Element[size]; // При выполнение и переходе в этот клас перед инициализацией кеонструтора отрабатывает создание путого массива, что вызывает ошибки
        public TestStsrtMenuW(Dictionary<string, T> Diction) : this(Diction, false) { }
        public TestStsrtMenuW(Dictionary<string, T> Diction, bool ExecuteClear) : this(Diction, 0, ExecuteClear) { }
        public TestStsrtMenuW(Dictionary<string, T> Diction, int NumberOfLins, bool ExecuteClear)
        {
            this.ExecuteClear = ExecuteClear;
            Dic = Diction;
            size = Dic.Count;
            this.NumberOfLins = NumberOfLins;
            // menuStart(); - Не все параметры успевают установится, поэтому надо вписывать этот метод в конструтор производный
        }
        public virtual void menuStart()   //Надо убедитя можно ли так делать или нетЁ!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Element[] elems = FillingElmens(); // Программа понимает что надо реализовывать метод указанный в классе оюъекта инициализации !!!! (Над очетко понять как работает !)
            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins, executeClear = ExecuteClear };
            SelectMenu(ref menu);
        }

        public virtual Element[] FillingElmens() { Element[] elems = new Element[size];return  elems;}
        
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

    public class MenuDefolt : TestStsrtMenuW<GetDelegate.CommandHandler>
    {
        public MenuDefolt(Dictionary<string, GetDelegate.CommandHandler> Diction) : base(Diction) { }
        public MenuDefolt(Dictionary<string, GetDelegate.CommandHandler> Diction, bool ExecuteClear) : base(Diction, ExecuteClear) { }
        public MenuDefolt(Dictionary<string, GetDelegate.CommandHandler> Diction, int NumberOfLins, bool ExecuteClear) :base(Diction, NumberOfLins, ExecuteClear) 
        {
            // base.Dic = Diction;
            // base.NumberOfLins = NumberOfLins;
             menuStart();
        }

        public override Element[] FillingElmens() 
        {
            Element[] elems = new Element[size];
            int counnter = 0;
            if (counnter < size)
            {
                foreach (var item in this.Dic) // Плохо понимаю почему тут указано this вроде надо же указывать base раз это свойство из радительского класса - т.к. это наследник, то свойство Dic является и так его свойством!
                {
                    elems[counnter] = new Element(item.Key, item.Value);
                    counnter++;
                }
            }
            
            return elems;
        }
    }
     
    public class TestMenuStruct : TestStsrtMenuW<setStructDelegate>
    {
        public List<Word> WordCollection { get; set; }
        public TestMenuStruct(Dictionary<string, setStructDelegate> Diction, List<Word> WordCollection): base(Diction)
        {
            this.WordCollection = WordCollection;
            base.ExecuteClear = false;  //ТУт передолвать не надо, а можно сразу самому установить
            base.menuStart();
        }

        public override Element[] FillingElmens()
        {
            Element[] elems = new Element[size];
            int counnter = 0;
            if (counnter < size)
            {
                foreach (var item in this.Dic) // Плохо понимаю почему тут указано this вроде надо же указывать base раз это свойство из радительского класса - т.к. это наследник, то свойство Dic является и так его свойством!
                {
                    elems[counnter] = new Element(item.Key, item.Value, WordCollection[counnter]);
                    counnter++;
                }
            }

            return elems;
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
                    elems[counnter] = new Element(item.Key, item.Value);
                    counnter++;
                }
            }

            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins, executeClear = ExecuteClear };
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
                       // menu.ExecuteClear(ExecuteClear);
                        menu.ExecuteSelected();
                        break;
                    default: return;
                }
            }
        }
    }
}
