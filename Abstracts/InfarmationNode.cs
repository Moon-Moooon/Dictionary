using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Slovar.Abstracts
{
    // А вот надо ли 
    interface IBaseNode
    {
        public void InvokeDeleg();
    }

    public delegate void EditWord(Word word);

    public class BaseInfNode : IBaseNode
    {
        public BaseInfNode(string Text)
        {
            this.Text = Text;
        }

        public string Text { get; set; }
        public virtual void InvokeDeleg() { }
    }

    public class NodeAction : BaseInfNode
    {
        public Action metod { get; set; }
        public NodeAction(string Text, Action metod) : base(Text)
        {
            this.metod = metod;
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke();
        }
    }

    public class NodeMenuHistore //
    {
        public MenuSettings setting { get; set; }
        public List<BaseInfNode> list { get; }
        public NodeMenuHistore(List<BaseInfNode> list) : this(list, null) { }
        public NodeMenuHistore(List<BaseInfNode> list, MenuSettings setting)
        {
            this.list = list;
            this.setting = setting;
        }
    }

    public class NodeAction<T,K> : BaseInfNode
    {
        T argOne { get; }
        K argTo { get; }
        public Action<T, K> metod { get; }

        public NodeAction(string text, T argOne, K argTo, Action<T, K> metod) : base(text)
        {
            this.argOne = argOne;
            this.argTo = argTo;
            this.metod = metod;
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke(argOne, argTo);
        }

    }

    public class NodeAction<T> : BaseInfNode
    {
        
        private Action<T> metod;

        private T arg;
        
        public NodeAction(string text,T arg, Action<T> metod) : base(text)
        {
            this.metod = metod;
            this.arg = arg;
        }

        public override void InvokeDeleg()
        {
            metod?.Invoke(arg);
        }
    }

}
