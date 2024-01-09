using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public class Word
    {
        public int IDword { get; set; }
        public string RusName { get; set;}
        public string PolName { get; set; }

        public Word(int IDword, string RusName, string PolName)
        {
            this.IDword = IDword;
            this.RusName = RusName;
            this.PolName = PolName;
        }

        public void Print()
        {
            Console.WriteLine($"ID{IDword} \n Русский вариант - {RusName} \n Польский вариант - {PolName}");
        }
    }
}
