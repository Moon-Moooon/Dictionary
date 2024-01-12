using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    // дженериком при инициализации можно указывать тип данных
    internal class InfarmationNode<T>
    {
        public InfarmationNode(string Text) 
        { 
            this.Text = Text;
        }
         string Text { get; set; }  // - Обязательное поле текста
        public T Command { get; set; } // - Тут должен хранится название исполняемого типа

        public void Execute() 
        {
            Console.WriteLine("ИСполнение команды");
        }
    }
}
