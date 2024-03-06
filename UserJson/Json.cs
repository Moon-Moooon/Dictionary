using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Slovar.Abstracts;
using Slovar.Entity;

namespace Slovar.UserJson;

public static class Json
{
    // недописан
    public static SetDictionarites GetSetDictionarity()
    {
        var file = File.ReadAllText("ListDictionarityes.json");

        SetDictionarites set = JsonConvert.DeserializeObject<SetDictionarites>(file);

        return set;
    }

    public static void SaveSetDictionarity(SetDictionarites set)
    {
        var file = JsonConvert.SerializeObject(set);
        
        File.WriteAllText("ListDictionarityes.json",file);
    }
    
    // При запуске программы нужно вызывать
    public static void CraftLenguagesJson()
    {
        ListLengs lengs = new ListLengs()
        {
            Lengs = getListLengs().Select(i => i.Name).ToList()
        };
        
        var obj = JsonConvert.SerializeObject(lengs);
        
        File.WriteAllText("ListLengs.json", obj);
    }

    public static IEnumerable<Type> getListLengs()
    {
        var Sborkaa = Assembly.Load("DbContextAndAPI")
            .GetTypes()
            .Where(t => t.GetCustomAttribute<IsLanguageAttribute>() != null);

        return Sborkaa;
    }
}