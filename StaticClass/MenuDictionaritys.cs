using Newtonsoft.Json;
using Slovar.Abstracts;
using Slovar.UserJson;

namespace Slovar.StaticClass;

public class MenuDictionaritys
{
    private SetDictionarites? setDic;

    private List<string> listLengs;
    
    // Нужно для того, если мы захотим снова начать созадавать слорб будем 
    private List<string> listLengsFull;

    // Число образованных пар у каждого языка
    private Dictionary<string, byte> LengAndNumCoup ;

    private string[] coup = new string [2];

    private readonly byte CuontLenguages;
    
    public MenuDictionaritys()
    {
        setDic = Json.GetSetDictionary();
        listLengsFull = Json.GetListLenguages();
        LengAndNumCoup = setDic.LengAndNumCoup;
        CuontLenguages = (byte)listLengsFull.Count;
        forStartWork();

    }

    public static void Go()
    {
        MenuDictionaritys a = new MenuDictionaritys();
    }
    
    private void forStartWork()
    {
        listLengs = lengsCanCreateCoup();
        MyDictionarity();
    }
    
    //1
    private void MyDictionarity()
    {
        List<BaseInfNode> list = new List<BaseInfNode>()
        {
            new NodeAction("1.Добавить словарь", addNewDictionarity),
        };
        
        list.AddRange(CraftListDictionarity());
        
        NewStartMenu start = new NewStartMenu(list);
    }

    //2
    private  List<BaseInfNode> CraftListDictionarity()
    {
        List<BaseInfNode> list = new();

        foreach (var val in setDic.ListDictionarites)
        {
            list.Add(new NodeAction($"Словарь {val[0]}-{val[1]}", () => { SubMenuForDicti(val); }));
        }

        return list;
    }

    //3
    private  void addNewDictionarity()
    {
        List<BaseInfNode> list = new();

        if (listLengs.Count == 0) {HaveMaxCountDicti();}
        
        foreach (string leng in listLengs)
        {
            list.Add(new NodeAction<string>($"{leng}", leng, select));
        }
        
        NewStartMenu start = new NewStartMenu(list);
    }
    
    private void HaveMaxCountDicti()
    {
        List<BaseInfNode> l = new List<BaseInfNode>()
        {
            new NodeAction("1.Все возможные словари созданы", MyDictionarity)
        };

        NewStartMenu n = new NewStartMenu(l);
    }
    
    // Очень не красиво, но пока нет времени сразу написать красиво.
    private void select(string leng)
    {
        if (coup[0] == null)
        {
            listLengs.Remove(leng);
            coup[0] = leng;
            coupDontHaveWith(leng);
            addNewDictionarity();
        }
        else
        {
            coup[1] = leng;
            setDic.ListDictionarites.Add(new []{coup[0], coup[1]});
            
            //Добавляем инкриминатор того что у нас добавилась пара
            incrementNumCoup(true, coup);
            coup[0] = null; coup[1] = null;
            forStartWork();
            
        }
    }

    private void coupDontHaveWith(string lengName)
    {
        var outListLengs = listLengs;

        foreach (var value in setDic.ListDictionarites)
        {
            var b = value.Except(new[] { lengName }).ToList();
            if (b.Count < 2) outListLengs.Remove(b[0]);
        }
        
        listLengs = outListLengs;
    }

    private void SubMenuForDicti(string[] dicti)
    {
        // 
        List<BaseInfNode> list = new List<BaseInfNode>()
        {
            new NodeAction<string[]>("1.Установить словарь", dicti, setDicti),
            new NodeAction<string[]>("2.Удолить словарь", dicti, DeleteDicti)
        };

        
        NewStartMenu s = new NewStartMenu(list);
    }

    private void setDicti(string[] dicti)
    {
        Settings.SetupDictionary = dicti;
        setDic.SetIsNow = dicti;
        Json.SaveSetDictionary(setDic);
        MenuSet.ShowMenu();
    }

    // Также необходимо удаление слов и пар в БД при удалении словоря
    private void DeleteDicti(string[] dicti)
    {
        // удоляют пару в свойстве класса
        setDic.ListDictionarites = setDic.ListDictionarites
            .Where(i => !(i
                .Contains(dicti[0]) && i
                .Contains(dicti[1])))
            .ToList();
        
        // -1 к тому что у языков есть пара
        incrementNumCoup(false, dicti);
        
        forStartWork();
    }

    private void incrementNumCoup(bool sum, string [] cp)
    {
        for (int i = 0; i < cp.Length; i++)
        {
            if (sum) {LengAndNumCoup[cp[i]]++;}
            else{LengAndNumCoup[cp[i]]--;}
        }
    }

    private List<string> lengsCanCreateCoup()
    {
        var t =  LengAndNumCoup
                // -1 потому, что количество возможных уникальных пар равно числу языков - 1 
            .Where(l => l.Value < CuontLenguages - 1)
            .Select( c => c.Key)
            .ToList();
        
        return t; 
    }
}
