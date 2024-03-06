using Newtonsoft.Json;
using Slovar.Abstracts;
using Slovar.UserJson;

namespace Slovar.StaticClass;


// Прототип
public class MenuDictionaritys
{
    private static SetDictionarites? setDic; 

    static MenuDictionaritys()
    {
        setDic = Json.GetSetDictionarity();
    }
    
    //2
    private static List<BaseInfNode> CraftListDictionarity()
    {
        List<BaseInfNode> list = new();

        SetDictionarites set = Json.GetSetDictionarity();

        foreach (var val in set.ListDictionarites)
        {
            list.Add(new NodeAction($"1.Словарь {val[0]}-{val[1]}", () =>
            {
                SubMenuForDicti(val);
            }));
        }
        
        return list;
    }
    
    //1
    public static void MyDictionarity()
    {
        //SetDictionarites setdic = Json.GetSetDictionarity();
        
        List<BaseInfNode> list = new List<BaseInfNode>()
        {
            new NodeAction("1.Добавить словарь", addNewDictionarity),
            // а тут должны выводитиься список уже имеющихся словарей
        };

        list.AddRange(CraftListDictionarity());

        
        NewStartMenu start = new NewStartMenu(list);
    }
    
    // 3
    public static void addNewDictionarity()
    {
        var nameLengs = JsonConvert.DeserializeObject<ListLengs>(File.ReadAllText("ListLengs.json")).Lengs;
        
        List<BaseInfNode> list = new(1);

        string[] mas = new string[2];
        
        foreach (string leng in nameLengs)
        {
            list.Add(new NodeAction($"{leng}" ,()=>
            {
                
                nameLengs.Remove(leng);
                mas[0] = leng;
                list.Clear();
                nameLengs = coupDontHaveWith(leng);            
                next();
                
            }));
        }
        
        
        NewStartMenu start = new NewStartMenu(list);
        
         void next()
        {
            // Анализ наличевствования пар с выбранным языком, что бы не порождать лишние пары
            foreach (string leng in nameLengs)
            {
                // на месте ноды метод выбора для связывания
                list.Add(new NodeAction($"{leng}", () =>
                {
                    mas[1] = leng;
                    setDic.ListDictionarites.Add(mas);
                    MyDictionarity();
                }));
            }
            
            NewStartMenu startt = new NewStartMenu(list);
        }
         
        
        List<string> coupDontHaveWith(string lengName)
        {
            var outListLengs = nameLengs;
            
            foreach (var value in setDic.ListDictionarites )
            {
                var b = value.Except(new []{lengName}).ToList();
                if (b.Count < 2) outListLengs.Remove(b[0]);
            }

            return outListLengs;
        }
    }

    public List<string> coupDontHaveWith(string lengName)
    {
        var outListLengs = nameLengs;
            
        foreach (var value in setDic.ListDictionarites )
        {
            var b = value.Except(new []{lengName}).ToList();
            if (b.Count < 2) outListLengs.Remove(b[0]);
        }

        return outListLengs;
    }
    
    
    private static void SubMenuForDicti(string[] dicti )
    {
        // 
        List<BaseInfNode> list = new List<BaseInfNode>()
        {
            new NodeAction("1.Установить словарь",()=>
            {
                Settings.SetupDictionary = dicti;
                Json.SaveSetDictionarity(setDic);
                setDic = null;
                MenuSet.ShowMenu();
            }),
            new NodeAction("2.Удолить словарь" , () =>
            {
                DeleteDicti(dicti);
                MyDictionarity();
            })
        };

        NewStartMenu s = new NewStartMenu(list);
    }
        
    // Также необходимо удаление слов и пар в БД при удалении словоря
    private static void DeleteDicti(string[] dicti )
    {
        SetDictionarites file = Json.GetSetDictionarity();
        // Требует теста
        
        var rlist = file.ListDictionarites.Where(i => !(i.Contains( dicti[0]) && i.Contains( dicti[1])) );
        Json.SaveSetDictionarity(file);
        MyDictionarity(); 
    }
    
}