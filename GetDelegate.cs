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
    public class GetDelegate
    {
        public delegate void CommandHandler();

        public delegate Word WordDelegate(out string UpDate);

        public delegate void EditWord(Word word);

    }
    

    interface Isetstruct<T>
    {

    }

    public struct setStructDelegate
    {
        public delegate void CommandHandler();

        public delegate void EditWord(Word word);

        public delegate T GenericEditWord<T>(Word word);

        public delegate Dictionary<string, setStructDelegate> BackMenu();

        public delegate void UpWord(string rusName, string polName, int IDWord);

        public BackMenu menu;

        public EditWord editWord;

        public GenericEditWord<string> GenericeditWord;

        public CommandHandler CH;

        public void InvokeDeleg( Word word) //слишком много надо передовать параметров при вызове. - Идиотизм 
        {
            menu?.Invoke();
            editWord?.Invoke(word);
            CH?.Invoke();
            GenericeditWord?.Invoke(word);
            // Up
        }


        // 1.Обернуть реализацию струтуры в интерфейс с 1 методом 
        // В идеале надо добится что при создании объекта (пока что !) структуры и инициализации поля 1 или n полей - в объекте хранилось бы только заполненные поля и не было бы пустых

    }
}
