using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnMsSql.setStructDelegate;

namespace LearnMsSql
{
    // Необходимо вынести из класса делегаты, нет необходимости чтоб они были там + уменьшит строку вызова делегата 
    // Возмодно стоит вынести структуру за пределы этого файла ?

    public delegate void CommandHandler();

    // Видимо надо переписать в элементе параметр -! делегат не просто указатель --- можно кудато диструкцию элемента делать  Но зачем?
    // ed
    // прослойка которая может попросить Элемент - а затем е=
    public struct setStructDelegate
    {
        public delegate void CommandHandler();

        public delegate void EditWord(Word word);

        public delegate Dictionary<string, setStructDelegate> BackMenu();

        public BackMenu backMenu;

        public EditWord editWord;

        // public GenericEditWord<string> GenericeditWord;

        public CommandHandler CH;



        public void InvokeDeleg(Element element)
        {
            backMenu?.Invoke(); // Не понимаю где меню должно хранится 
            editWord?.Invoke(element.word);
            CH?.Invoke();
        }

        // 1.Обернуть реализацию струтуры в интерфейс с 1 методом 
        // В идеале надо добится что при создании объекта (пока что !) структуры и инициализации поля 1 или n полей - в объекте хранилось бы только заполненные поля и не было бы пустых

    }
}
