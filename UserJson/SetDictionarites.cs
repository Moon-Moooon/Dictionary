using Azure;
using Slovar.Abstracts;

namespace Slovar.UserJson;

public class SetDictionarites
{
    public string[] SetIsNow = new string[2];

    // public string[] SetIsNow
    // {
    //     get { return setIsNow; }
    //     
    //     set
    //     {
    //         if (value.Length == 2)
    //         {
    //             setIsNow = value;
    //         }
    //
    //         throw new Exception("String[] SetIsNow != 2! ");
    //     }
    // }

    public List<string[]> ListDictionarites { get; set; } = new();

}