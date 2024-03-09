using Azure;
using Slovar.Abstracts;

namespace Slovar.UserJson;

public class SetDictionarites
{
    public string[] SetIsNow = new string[2];
    public List<string[]> ListDictionarites { get; set; } = new();
    public Dictionary<string, byte> LengAndNumCoup { get; set; } = new();

}