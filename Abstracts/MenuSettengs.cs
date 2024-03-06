using System;

// Класс фабрика
// Таттерн фабрика

namespace Slovar.Abstracts
{
    public class MenuSettings
    {
        internal int posXX = Console.CursorLeft;
        internal int posYY = Console.CursorTop;
        internal int NumberOfLinsUp { get; set; } = 0;
        internal bool ExecuteClear { get; set; } = true;
        internal bool TabElemens { get; set; } = false;
    }

    class MenuSettingBullider
    {
        MenuSettings setting { get; }

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
}
