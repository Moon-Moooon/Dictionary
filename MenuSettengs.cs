using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMsSql
{
    public abstract class MenuSettengs
    {
        public int posX { get; set; }
        public int posY { get; set; } 
        public int NumberOfLinsUP { get; set; }

        public bool ExecuteClear { get; set; }
        public abstract void SetCursorElem();
        public MenuSettengs(int numberoflinsup, bool ExecuteClear)
        {
            this.posX = Console.CursorLeft;
            this.posY = Console.CursorTop;
            this.NumberOfLinsUP = numberoflinsup;
            this.ExecuteClear = ExecuteClear;
        }
    }

    public class MenuSettingDefolt : MenuSettengs
    {

        public MenuSettingDefolt() : this(0) { }
        public MenuSettingDefolt(int NumberOfLinsUP) : this(NumberOfLinsUP, true) { }
        public MenuSettingDefolt(int NumberOfLinsUP, bool ExecuteClear) : base(NumberOfLinsUP, ExecuteClear) 
        {
        
        }
        public override void SetCursorElem()
        {
            Console.SetCursorPosition(posX, posY);
        }
    }

    public class MenuSettingSunMenu : MenuSettengs
    {
        public MenuSettingSunMenu() : this(0, false) { }
        public MenuSettingSunMenu(int NumberOfLinsUP) :this(NumberOfLinsUP, false) { }
        public MenuSettingSunMenu(int NumberOfLinsUP, bool ExecuteClear) : base(NumberOfLinsUP, ExecuteClear)
        {
                        
        }

        public override void SetCursorElem()
        {
            Console.SetCursorPosition(4, posY);
        }
    }
}
