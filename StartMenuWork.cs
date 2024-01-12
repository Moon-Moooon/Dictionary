using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LearnMsSql.setStructDelegate;
using static System.Collections.Specialized.BitVector32;

namespace LearnMsSql
{

    /// <summary>
    ///  2Написание первого метода
    ///  3Забыл ка кработает сдвиг меню, надо хорошо закоментирвоать и сделать сдвиг - сдвигом в сторону
    ///  4Вспомнить как вообще координаты сохраняются у базового меню - вообще ничего не помню
    ///  5.Желательна полная очистка файла с мню
    ///  
    // При вызове конструтора родителя приходим к реализации Родительского СТардующегоконструктора а не наследника
    // Можно попробовать стек  

    public class MenuBase<T> // Проще просто наследоваться и не делать его обстрактным
    {
        public static int size { get; set; }
        public int NumberOfLins { get; set; } // Это указывается только в тех случаях когда будет вызов подМеню
        public byte ExecuteClear { get; set; } // Это булева означает при Тру = чистится консоль при переходе к команде, фолс = выполняется особый сдвиг меню ивысов под меню для редлактирования словаря 
        public Dictionary<string, T> Dic { get; set; } // Это обязхательная констурки без нею нет перехода по меню 

        // public Element[] elems = new Element[size]; // При выполнение и переходе в этот клас перед инициализацией кеонструтора отрабатывает создание путого массива, что вызывает ошибки
        public MenuBase(Dictionary<string, T> Diction) : this(Diction, 0) { }
        public MenuBase(Dictionary<string, T> Diction, byte ExecuteClear) : this(Diction, 0, ExecuteClear) { }
        public MenuBase(Dictionary<string, T> Diction, int NumberOfLins, byte ExecuteClear)
        {
            this.ExecuteClear = ExecuteClear;
            Dic = Diction;
            size = Dic.Count;
            this.NumberOfLins = NumberOfLins;
            menuStart();  //Не все параметры успевают установится, поэтому надо вписывать этот метод в конструтор производный 
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
            
            return  elems;
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

    public class MenuDelegVoid : MenuBase<CommandHandler>
    {
        public List<Word> WordCollection { get; set; } // В обоих наследниках есть это свойство !!!!
        public MenuDelegVoid(Dictionary<string, CommandHandler> Diction, byte ExecuteClear) : base(Diction, ExecuteClear) { }
        public MenuDelegVoid(Dictionary<string, CommandHandler> Diction, int NumberOfLins, byte ExecuteClear) : base(Diction, NumberOfLins, ExecuteClear) { }
        public MenuDelegVoid(Dictionary<string, CommandHandler> Diction, byte ExecuteClear, List<Word> WordCollection) : this(Diction, 0, ExecuteClear, WordCollection) { }
        public MenuDelegVoid(Dictionary<string, CommandHandler> Diction, int NumberOfLins, byte ExecuteClear, List<Word> WordCollection) : base(Diction, NumberOfLins, ExecuteClear)
        {
            this.WordCollection = WordCollection;   
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
     
    public class TestMenuStruct : MenuBase<setStructDelegate>
    {
        public TestMenuStruct BackMenu { get; set; }
        public List<Word> WordCollection { get; set; }
        public TestMenuStruct(Dictionary<string, setStructDelegate> Diction, byte ExecuteClear) :base(Diction, ExecuteClear) { }
        public TestMenuStruct(Dictionary<string, setStructDelegate> Diction, byte ExecuteClear, int NumberOfLins) : base(Diction, NumberOfLins, ExecuteClear) { }
        public TestMenuStruct(Dictionary<string, setStructDelegate> Diction, byte ExecuteClear, int NumberOfLins, List<Word> WordCollection) : base(Diction, NumberOfLins, ExecuteClear)
        {
            this.WordCollection = WordCollection;
            // base.ExecuteClear = 0;  //ТУт передолвать
            menuStart();
        }

        // Проверку на наличие 
        public override void menuStart()
        {
            
            Element[] elems = FillingElmens();

            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins, ExecuteClear = ExecuteClear};
            SelectMenu(ref menu);
        }

        //public Element[] ChoiserElem()
        //{
        //    if(WordCollection == null)
        //    {
        //        return 
        //    }

        //    return 
        //}
           

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

        //public Element[] FillingElmensWord()
        //{
        //    Element[] elems = new Element[size];
        //    int counnter = 0;
        //    if (counnter < size)
        //    {
        //        foreach (var item in this.Dic) // Плохо понимаю почему тут указано this вроде надо же указывать base раз это свойство из радительского класса - т.к. это наследник, то свойство Dic является и так его свойством!
        //        {
        //            elems[counnter] = new Element(item.Key, item.Value, WordCollection[counnter]);
        //            counnter++;
        //        }
        //    }
        //    return elems;
        //}


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
        public Dictionary<string, CommandHandler> Dic { get; set; }
        public List<Word> WordCollection { get; set; }
        public StartMenuWork(Dictionary<string, CommandHandler> Diction) : this(Diction, true) { }
        public StartMenuWork(Dictionary<string, CommandHandler> Diction, bool ExecuteClear) : this(Diction, true, 0, null) { }
        public StartMenuWork(Dictionary<string, CommandHandler> Diction, bool ExecuteClear, int NumberOfLins, List<Word> WordCollection) // перегрузка не продумана
        {
            this.WordCollection = WordCollection;
            this.NumberOfLins = NumberOfLins;
            this.Dic = Diction;
            this.ExecuteClear = ExecuteClear;
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

            Menu menu = new Menu(elems) { NumberOfLinsUP = NumberOfLins }; // ExecuteClear = ExecuteClear
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
