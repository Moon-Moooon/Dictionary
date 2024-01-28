using LearnMsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public class Word
    {
        public int IDword { get; private set; }
        public string RusName { get; private set; }
        public string PolName { get; private set; }

        public Word(int IDword, string RusName, string PolName)
        {
            this.IDword = IDword;
            this.RusName = RusName;
            this.PolName = PolName;
        }

        public void Print()
        {
            Console.WriteLine($"Русский вариант - Польский вариант \n{RusName} - {PolName}");
        }

        public void GetName(out string RusName, out string PolName )
        {
            PolName = this.PolName;
            RusName = this.RusName;
        }

    }
}


