using Slovar.StaticClass;
using Slovar.UserJson;

namespace Slovar;

public static class Settings
{
    private static string[] _SetupDictionary;

    public static string[] SetupDictionary
    {
        get { return _SetupDictionary; }
        set
        {
            if (CheckOnBad(value)) { throw new Exception("String[] SetIsNow != 2 ");}
        
            _SetupDictionary = value;

            var setDic = Json.GetSetDictionary();
            setDic.SetIsNow = value;
            Json.SaveSetDictionary(setDic);
        }
    }

    public static void WriteSetupDictionary()
    {
        var jsonFile = File.Exists("ListDictionarityes.json");
        
        switch (jsonFile)
        {
            case true:
                var sett = Json.GetSetDictionary();
                
                CheckLengAndNumCoup(sett);
                    
                if (CheckOnBad(sett.SetIsNow))
                {
                    MenuDictionaritys.Go();
                }
                
                _SetupDictionary = sett.SetIsNow;
                MenuSet.ShowMenu();
                break;

            case false:
                SetDictionarites set = new();
                Json.SaveSetDictionary(set);
                CheckLengAndNumCoup(set);
                MenuDictionaritys.Go();
                break;
        }
    }

    private static bool CheckOnBad(string[] dicti)
    {
        if (dicti is null ||
            (dicti.Length != 2) ||
            dicti[0] == null ||
            dicti[1] == null)
        {
            return true;
        }

        return false;
    }

    private static void CheckLengAndNumCoup(SetDictionarites set )
    {
        var listLengs = Json.GetListLenguages().Lengs;
        
        var t = set.LengAndNumCoup;

        for (int i = 0; i < listLengs.Count; i++)
        {
            if (!(set.LengAndNumCoup.ContainsKey(listLengs[i])))  
            {
                set.LengAndNumCoup.Add(listLengs[i],0);
            }
        }
        
        
        Json.SaveSetDictionary(set);
    }
}