using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// По приколу можно переписать в JSON
// Класс фабрика
// Таттерн фабрика

namespace LearnMsSql.MainFils
{
    public abstract class MenuSettengs
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public int NumberOfLinsUP { get; set; }
        public bool ExecuteClear { get; set; }
        public bool TabElemens { get; set; }
        public abstract void SetCursorMenu();

        public abstract void SetCursorElem(int numElements);
        public MenuSettengs(int numberoflinsup, bool ExecuteClear, bool TabElemens)
        {
            posX = Console.CursorLeft;
            posY = Console.CursorTop;
            NumberOfLinsUP = numberoflinsup;
            this.ExecuteClear = ExecuteClear;
            this.TabElemens = TabElemens;
        }
    }

    public class MenuSettingDefolt : MenuSettengs
    {
        public MenuSettingDefolt() : this(0) { }
        public MenuSettingDefolt(int NumberOfLinsUP) : this(NumberOfLinsUP, true, false) { }
        public MenuSettingDefolt(int NumberOfLinsUP, bool ExecuteClear, bool TabElemens) : base(NumberOfLinsUP, ExecuteClear, TabElemens)
        {

        }
        public override void SetCursorMenu()
        {
            Console.SetCursorPosition(posX, posY);
        }
        public override void SetCursorElem(int numElements)
        {
            if (TabElemens) Console.SetCursorPosition(4, posY + numElements);
            else Console.SetCursorPosition(0, posY + numElements);
        }
    }
    // меню переделан в билдер с дополнен метода благодаря расщиряющим методам класс MenuSettingMetods

    // MenuSettings menuset = new MenuSettingBuilder().Build();
    internal class MenuSettings
    {
        internal int posXX = Console.CursorLeft;
        internal int posYY = Console.CursorTop;
        internal int NumberOfLinsUp { get; set; } = 0;
        internal bool ExecuteClear { get; set; } = true;
        internal bool TabElemens { get; set; } = false;
    }
    static class MenuSettingMetods
    {
        public static void SetCursorMenu(this MenuSettings settengs)
        {
            Console.SetCursorPosition(settengs.posXX, settengs.posYY);
        }
        public static void SetCursorElem(this MenuSettings settengs, int numElements)
        {
            if (settengs.TabElemens) Console.SetCursorPosition(4, settengs.posYY + numElements);
            else Console.SetCursorPosition(0, settengs.posYY + numElements);
        }
    }

    class MenuSettingBullider
    {
        MenuSettings setting; // { get; set; }

        public MenuSettingBullider()
        {
            setting = new();
        }

        public MenuSettingBullider NumberOfLinsUp(int num)
        {
            setting.NumberOfLinsUp = num;
            return this;
        }

        public MenuSettingBullider ExecuteClear(bool execut)
        {
            setting.ExecuteClear = execut;
            return this;
        }

        public MenuSettingBullider TabElemens(bool tab)
        {
            setting.TabElemens = tab;
            return this;
        }

        public MenuSettings Build()
        {
            return setting;
        }
    }
}
