﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnMsSql
{
     // Можно сделать очередь, кужда можно будет или через спец метод подключать внешние проверки... хотя это не совсем контролируемое внешнее воздействие 
     // Очередб доступная сдесь

    // Нкжны тесты
    public class ChecLinesOn
    {
        private static Stack<queueNode> stack = new Stack<queueNode>();
        private static Word word { get; set; }

        public static Word Go(Word word)
        {
            ChecLinesOn.word = word;
            int IDWord;
            string rusName;
            string polName;

            word.GetInfo(out IDWord, out rusName, out polName);

            string[] lines = new string[] { rusName, polName };

            List<string> reWrite = new List<string>() { };

            foreach (string line in lines)
            {

                CraftStack(line);

                foreach (queueNode node in stack)
                {
                    string reLine = node.InvokeDeleg();
                    reWrite.Add(reLine);
                }
                stack.Clear();

            }

            Word outputWord = new Word(IDWord, reWrite[0], reWrite[1]);

            return outputWord;
        }

        static void CraftStack(string line)
        {
            queueSpacr metod1 = new(line, ManySpace);
            stack.Push(metod1);
            queueEmptyLine metod2 = new(line, EmptyLine);
            stack.Push(metod2);
        }

        public static string ManySpace(string line) // Недописан 
        {
            string pattern1 = @"\s+"; // если пробелов 1 и больше заменяет на 1
            Regex regex = new Regex(pattern1);
            string outputWord = regex.Replace(line, " ");

            return outputWord;
        }
        
        public static string EmptyLine(string line)
        {
            string[] pattern = new string[] { " ", "" };

            for (int i = 0;i < pattern.Length ;i++)
            {
                if (Regex.IsMatch(line, pattern[i]))
                {
                    ToMenu(line);
                }
            }

            static void ToMenu(string line)
            {
                Console.WriteLine($"Слово -  '{line}'  пустое");

                List<BaseInfNode> list = new List<BaseInfNode>();
                NodeEditWord link1 = new("1.Редактирвоать пару еще раз", word, RedactionWord.Redaction); // Проблема!
                list.Add(link1);
                NodeCommandHandler link2 = new("2.Главное меню", Program.ShowMenu);
                list.Add(link2);

                MenuHistori.Add(new(list, new MenuSettingDefolt(1))); // мб и не надо тут для истории

                NewStartMenu menu = new(list, new MenuSettingDefolt(1));
            }

            return pattern[0];
        }

        //-----------------------------------------------------------------
        // 

        class queueNode1<T> // - По сути это интерфейс -- главное поэксперементирвать имеет ли он встроенную ковариантность в листах для передачи
        {
            public Func<T, T> func { get; private set; }

            public string word;
        }
        // Выше набросок не относится к логике 
        //------------------------------------------------------------------------------------------------------

        class queueNode // Хороший вариент для тринорвки испоьзования интерфесов -- ибо тут только 1 метод вся суть в сонтике
        {
            public virtual string InvokeDeleg() 
            { 
                string empty = "Empty";
                return empty;
            }
        }

        class queueSpacr : queueNode
        {
            public Func <string, string> func {  get; private set; }
            public string line { get; private set; }
            public queueSpacr(string line, Func<string, string > func)
            {
                this.func = func;
                this.line = line;
            }

            public override string InvokeDeleg() 
            {
                string outPut = func.Invoke(line);

                return outPut;
            }
        }

        class queueEmptyLine : queueNode
        {
            public string line { get; private set; }
            public Func<string, string> func { get; private set; }

            public queueEmptyLine(string line, Func<string, string> func) 
            {
                this.line = line;
                this.func = func;
            }
            public override string InvokeDeleg()
            {
                string outPut = func.Invoke(line);

                return line;
            }
        }

    }
}

