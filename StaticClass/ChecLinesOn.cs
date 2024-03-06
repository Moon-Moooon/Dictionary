using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Slovar.Abstracts;

namespace Slovar.StaticClass
{
    public class ChecLinesOn
    {
        private static Stack<Func<string, string>> StackK = new();
        private static Word word { get; set; }
        public static Word Go(Word fromWord)
        {
            string[] lines = { fromWord.RusName, fromWord.PolName };
            
            List<string> reWrite = new List<string>() { };
            
            CraftStack();

            foreach (string line in lines)
            {
                string reLine= string.Empty;
                
                foreach (var fun in StackK)
                {
                     reLine = fun.Invoke(line);
                }
                
                reWrite.Add(reLine);
            }
            
            Word outputWord = new Word(fromWord.IDword, reWrite[0], reWrite[1]);

            return outputWord;
        }
        
        static void CraftStack()
        {
            StackK.Push(ManySpace);
            StackK.Push(EmptyLine);
        }
        
        public static string ManySpace(string line) // Недописан 
        {
            string pattern1 = @"\s+"; // если пробелов 1 и больше то заменяет на 1
            Regex regex = new Regex(pattern1);
            string outputWord = regex.Replace(line, " ");
            
            outputWord = outputWord.Trim();
            return outputWord;
        }

        public static string EmptyLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                MenuSet.ToMenu(line, word);
            }
            return line;
        }
    }
}