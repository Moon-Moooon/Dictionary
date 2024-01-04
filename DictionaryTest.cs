using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public class DictionaryTest
    {
        public static void Test()
        {
            GetDelegate GetDelegate = new();
            Dictionary<string, GetDelegate> Links = new Dictionary<string, GetDelegate>()
            {

                {"1.Редактировать", GetDelegate },
                {"2.Удолить пару", GetDelegate},
                {"3.Вернуться к словорю", GetDelegate},
                {"4.Вернутсья к главному меню",GetDelegate }
            };

        }

        
    }
}
