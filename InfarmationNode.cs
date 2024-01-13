using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LearnMsSql
{

    // Не всегда может получить что подойдет Виртуальный метод
    public class BaseInfNode
    {
        public BaseInfNode(string Text) 
        { 
            this.Text = Text;
        }

        public string Text { get; set; } 
        public virtual void InvokeDeleg() { }
    }

    public class NodeEditWord : BaseInfNode
    {
        public Word word { get; set; }
        public Action<Word> metod { get; set; }
        public NodeEditWord(string Text, Word word, Action<Word> metod) : base(Text)
        {
            this.word = word;
            this.metod = metod;
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
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke();
        }
    }

    public class NodeSubMenu : NodeCommandHandler
    {
        public Word word { get; set; }
        public Action metod { get; set; }
        public NodeSubMenu(string Text, Word word, Action metod) : base(Text, metod)
        {
            this.metod = metod;
            this.word = word;
        }
    }
}
