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

            var setDic = Json.GetSetDictionarity();
            setDic.SetIsNow = value;
            Json.SaveSetDictionarity(setDic);
        }
    }

    public static void WriteSetupDictionary()
    {
        var jsonFile = File.Exists("ListDictionarityes.json");

        switch (jsonFile)
        {
            case true:
                var a = Json.GetSetDictionarity();

                if (CheckOnBad(a.SetIsNow))
                {
                    MenuDictionaritys.MyDictionarity();
                }
                        // снизу мб не надо
                _SetupDictionary = a.SetIsNow;
                MenuSet.ShowMenu();
                break;

            case false:
                SetDictionarites set = new();
                Json.SaveSetDictionarity(set);
                MenuDictionaritys.MyDictionarity();
                break;
        }
    }

    public static bool CheckOnBad(string[] dicti)
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
}