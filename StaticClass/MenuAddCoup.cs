using System.Text;
using Slovar.Lenguages; 
using Slovar.Abstracts; 
using Slovar.Entity;
using Slovar.UserJson;

namespace Slovar.StaticClass;

public class MenuAddCoup
{
    public static void AddWord()
    {
        string firstWord;
        string secondWord;

        #region

        Console.WriteLine(
            $"Для добавления нового слова в словарь вам необходимо ввести слова на {Settings.SetupDictionary[0]} и на {Settings.SetupDictionary[1]}");
        Console.WriteLine($"Введите слово на {Settings.SetupDictionary[0]}");
        firstWord = MyConsole.MyReadLine();
        ReviewLengs.Go(firstWord, Settings.SetupDictionary[0]);
        
        Console.WriteLine("");
        Console.WriteLine($"Введите слово на {Settings.SetupDictionary[1]}");
        secondWord = MyConsole.MyReadLine();
        ReviewLengs.Go(secondWord, Settings.SetupDictionary[1]);

        Console.WriteLine("");
        Console.WriteLine($"Добавить новую пару слов: '{firstWord}' с переводом '{secondWord}'?");
            
        List<KeyValuePair<string, string>> kvp = new List<KeyValuePair<string, string>>()
        {
           new(Settings.SetupDictionary[0],firstWord),
           new(Settings.SetupDictionary[1],secondWord)
        };
        
        //Console.Clear();
        
        MyDictionary md = fraftMyDictionary(kvp); 
        
        List<BaseInfNode> list = new List<BaseInfNode>()
        {
            new NodeAction<MyDictionary>("1.Да",md, addCoupInDb),
            new NodeAction("2.Нет",MenuSet.ShowMenu)
        };

        NewStartMenu menu = new(list, new MenuSettingBullider()
            .NumberOfLinsUp(6)
            .Build());

        #endregion
    }
    
    private static MyDictionary fraftMyDictionary(List<KeyValuePair<string, string>> dicc)
    {
        MyDictionary m = new MyDictionary();
        
        ListLengs LL = Json.GetListLenguages();
        
        for (int i = 0; i < dicc.Count; i++)
        {
            m.words.Add(initWordsItem(dicc[i].Value,dicc[i].Key, LL ));
        }

        return m;
    }
    
    private static Words initWordsItem(string wordName, string leng, ListLengs LL)
    {
        StringBuilder sb = new StringBuilder($"{LL.pathToLengs}{leng}, {LL.AssembleName}");
        
        var str = sb.ToString();
        
        Type tt = Type.GetType(str, true);
            
        var b = Activator.CreateInstance(tt);
        
        if (b is Words item)
        {
            item.WordName = wordName;
            return item;
        }
        
        return new Words();
       
    }
    
    public static void addCoupInDb(MyDictionary dic)
    {
        ApiRepost<Words> a = new ApiRepost<Words>();
        a.AddDictionarity(dic);
        MenuSet.ShowMenu();
    }
}