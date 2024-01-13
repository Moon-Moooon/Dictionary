using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LearnMsSql
{
    // дженериком при инициализации можно указывать тип данных
    // Можно попробовать сделать Интерфейс с ЛямдаВыражением

    // Не всегда может получить что подойдет Виртуальный метод
    public class BaseInfNode
    {
        public Type Type { get; set; }
        public BaseInfNode(string Text) 
        { 
            this.Text = Text;
        }

        public string Text { get; set; }  // - Обязательное поле текста
        public virtual void InvokeDeleg() { } // Стоит ли плодить столько разных вертуалок ?


    }

    public class NodeEditWord : BaseInfNode
    {
        public Word word { get; set; }
        public Action<Word> metod { get; set; }
        public NodeEditWord(string Text, Word word, Action<Word> metod) : base(Text)
        {
            this.word = word;
            this.metod = metod;
            base.Type = typeof(NodeEditWord);
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke(word);
        }
    }

    public class NodeCommandHandler : BaseInfNode
    {
        public Action metod { get; set; }
        public NodeCommandHandler(string Text, Action metod) : base(Text)
        {
            this.metod = metod;
            base.Type = typeof(NodeCommandHandler);
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke();
        }
    }
}
