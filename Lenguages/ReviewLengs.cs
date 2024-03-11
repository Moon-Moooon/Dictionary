using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Slovar.Abstracts;
using Slovar.StaticClass;

namespace Slovar.Lenguages;

// Требует полного теста
public class ReviewLengs
{
    private string punctuation = "/.,?!";
    //string russia = " абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    //string pollend = " ąćęłńóśźżabcdefghijklmnopqrstuvwxyz";
    //string english = " abcdefghijklmnopqrstuvwxyz";
    
    private string inPut { get; }
    private string leng { get; }
    
    private ReviewLengs(string inPut, string leng)
    {
        this.inPut = inPut;
        this.leng = leng;
        check(inPut, craftPattern());
        
    }

    private Dictionary<string, string> lengsPattern = new Dictionary<string, string>()
    {
        //{ "punctuation", "/.,?!" },
        { "Russia", " абвгдеёжзийклмнопрстуфхцчшщъыьэюя" },
        { "Poland", " ąćęłńóśźżabcdefghijklmnopqrstuvwxyz" },
        { "English", " abcdefghijklmnopqrstuvwxyz" }
    };

    public static void Go(string inPut, string leng)
    {
        ReviewLengs r = new ReviewLengs(inPut, leng);
    }

    private string craftPattern()
    {
        // Стринг Билдер
        StringBuilder b = new StringBuilder(lengsPattern[leng] + punctuation);
        //var o = lengsPattern[leng] + punctuation;
        return b.ToString();
    }
    
    public void check(string input, string pattern)
    {

        pattern = @$"^[ {pattern}]+$";
        
        if (!(Regex.IsMatch(input, pattern)))
        {
            Asc();
        }
    }

    private void Asc()
    {
        Console.Clear();
        
        Console.WriteLine($"Неккоректо введено слово {inPut} на {leng}");
        Console.WriteLine("Пропробоватиь еще раз?");
        List<BaseInfNode> l = new List<BaseInfNode>()
        {
            new NodeAction<string, string>("1.Да", inPut, leng, Go),
            new NodeAction("2.Нет", MenuSet.ShowMenu)
        };

        NewStartMenu n = new NewStartMenu(l);
    }
    
    // public static void ExeminationRusWord(string rusWord)
    // {
    //     string word = rusWord.ToLower();
    //     char[] bak = new char[1];
    //     int i = 0;
    //     int j = 1;
    //
    //     foreach (char item in word) // 274 слишком хардкод
    //     {
    //         bak[i] = item;
    //
    //         byte[] wordAsByteMass = Encoding.Default.GetBytes(bak);
    //
    //         int f = wordAsByteMass.Length;
    //
    //         if (wordAsByteMass.Length < 2)
    //         {
    //             if (wordAsByteMass[i] != 32)
    //             {
    //                 Console.Clear();
    //                 MenuSet.getAfterBedCHois(item);
    //             }
    //         }
    //         else
    //         {
    //             if (!(wordAsByteMass[i] >= 208 && wordAsByteMass[i] <= 209 &&
    //                   (176 <= wordAsByteMass[j] && wordAsByteMass[j] <= 191 ||
    //                    wordAsByteMass[j] >= 128 && wordAsByteMass[j] <= 143 ||
    //                    wordAsByteMass[j] == 145)))
    //             {
    //                 Console.Clear();
    //                 MenuSet.getAfterBedCHois(item);
    //             }
    //         }
    //     }
    // }


    // // Очень плохой метод
    // public static void ExeminationPolWord(string item)
    // {
    //     item = item.ToLower();
    //     string str = "ąćęłńóśźż abcdefghijklmnopqrstuvwxyz";
    //     int n = 0;
    //     bool bl = false;
    //     while (bl == false)
    //     {
    //         for (int j = 0; j < item.Length; j++)
    //         {
    //             for (int i = 0; i < str.Length; i++)
    //             {
    //                 if (item[j] != str[i])
    //                 {
    //                     n++;
    //                 }
    //                 else
    //                 {
    //                     j++;
    //
    //                     i = -1;
    //
    //                     n = 0;
    //                 }
    //
    //                 if (n == str.Length || n < i)
    //                 {
    //                     Console.WriteLine($" Не верный символ {item[j]}");
    //
    //                     MenuSet.getAfterBedCHois(item[j]);
    //                 }
    //
    //                 if (j == item.Length)
    //                 {
    //                     bl = true;
    //
    //                     i = str.Length;
    //                 }
    //             }
    //         }
    //     }
    // }
}