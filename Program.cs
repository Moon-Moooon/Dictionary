using System.Text.RegularExpressions;
using Slovar.Abstracts;
using Slovar.UserJson;
using System.Reflection;
using System.Text;
using Slovar.Entity;

namespace Slovar
{
    
    /// 31Проблема передачи положения курсора, небыло выведено правило для его перемечения!
    ///  Если установить норму опредления положения курсора дла авто считывания, можно лучше сделать формирования билдера настроик менюшки
    // 32Зачем при создании меню я каждый раз создаю новый объект -- можно конфигурировать уже статический объект тоже с меню
    /// 34Можно вообще постараться убрать параметр строк, сделать так чтоб все автоматом считалось

    /// <summary>
    /// 1.Вместо множества типов можно формировать сущность которая будет создаваться через билдер
    /// N кол во аргументов
    /// Вариардик делегат
    /// </summary>
    
    ///Какие есть .json
    /// ListDictionarityes.json
    /// ListLengs.json
    /// 
    internal class Program
    {
        static void Main(string[] args)
        {
            // Дописать класс с проверкой вхожных строк на повторы запятых и тп
            Json.CraftLenguagesJson();
            Settings.WriteSetupDictionary();
        }
        
    }

}