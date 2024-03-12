using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Slovar.Abstracts;
using Slovar.Entity;

namespace Slovar.UserJson;

public static class Json
{
    // недописан
    public static SetDictionarites GetSetDictionary()
    {
        var file = File.ReadAllText("ListDictionarityes.json");

        SetDictionarites set = JsonConvert.DeserializeObject<SetDictionarites>(file);

        return set;
    }

    public static void SaveSetDictionary(SetDictionarites set)
    {
        var file = JsonConvert.SerializeObject(set);
        
        File.WriteAllText("ListDictionarityes.json",file);
    }
    
    public static ListLengs GetListLenguages()
    {
        var file = File.ReadAllText("ListLengs.json");

        ListLengs set = JsonConvert.DeserializeObject<ListLengs>(file);

        return set;
    }
    
    // При запуске программы нужно вызывать
    public static void CraftLenguagesJson()
    {
        ListLengs lengs = new ListLengs()
        {
            Lengs = getListLengs().Select(i => i.Name).ToList()
        };
        
        Assembly a =  Assembly.Load("DbContextAndAPI");

        lengs.AssembleName = a.GetName().Name;
        lengs.pathToLengs = getPathToClassLengs(a); 
        
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

    public static string getPathToClassLengs(Assembly a)
    {
        var b = a.GetTypes()
            .First(t => t
                .GetCustomAttribute<IsLanguageAttribute>() != null).FullName
            .Split(".");

        StringBuilder sb = new StringBuilder();
            
        for (int i = 0; i < b.Length - 1; i++)
        {
            sb.Append(b[i]).Append(".");
        }

         string pathToLengs = sb.ToString();

         return pathToLengs;
    }
}