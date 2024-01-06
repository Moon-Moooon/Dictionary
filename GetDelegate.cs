using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public struct setStructDelegate
    {
        public delegate void CommandHandler();

        public delegate void EditWord(Word word);

        public EditWord editWord;

        public CommandHandler CH;

        public void InvokeDeleg( Word word) //Тест
        {
            editWord?.Invoke(word);
            CH?.Invoke();
        }

    }
}
