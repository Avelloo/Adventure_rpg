using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class ItemList //Инициализация всех предметов в игре
    {
        /// <summary>
        /// Список всех игровых предметов.
        /// <summary>

        // Оружие                            Название      Описание                                            Тип предмета  ДМГ  Класс
        static Weapon oldSword = new Weapon("Старый меч", "Не лучшее оружие, но всё ещё может оказаться полезным.", "Оружие", 3, "Воин");
        static Weapon steelSword = new Weapon("Стальной меч", "Добротно сделанный меч.", "Оружие", 6,"Воин");
        static Weapon silverSword = new Weapon("Серебряный меч", "На рукояти нарисовано множество замысловатых рисунков...", "Оружие", 12, "Воин");

        // Броня                              Название       Описание                 Защита  Часть тела
                //стартовая
        static Armor tornShirt = new Armor("Рваная рубаха", "Ну, хотя бы не холодно...", 1, "Нагрудник");
        static Armor dirtyPants = new Armor("Грязные штаны", "Нужно будет хотя бы отмыть их :)...", 1, "Штаны");
                //Кожаный сет
        static Armor leatherJacket = new Armor("Кожаный нагрудник","Сшит из нескольких лоскутов кожи.",3,"Нагрудник");         
        static Armor leatherPants = new Armor("Кожаные штаны", "Задница потеет как не в себя...", 2, "Штаны");
        static Armor leatherGloves = new Armor("Кожаные перчатки", "Стиляга =)", 2, "Перчатки");
        static Armor leatherHelmet = new Armor("Кожаный шлем", "Скорее напоминает кожаную шапочку", 2, "Шлем");
                //Железный сет
        static Armor ironBreastplate = new Armor("Железный нагрудник", "После дня в таком ваши плечи вам точно не скажут «Спасибо».", 5, "Нагрудник");
        static Armor ironPants = new Armor("Железные штаны", "Неудобно двигаться, зато надежно.", 4, "Штаны");
        static Armor ironGloves = new Armor("Железные перчатки", "Пальцы как деревянные... ", 3, "Перчатки");
        static Armor ironHelmet = new Armor("Железный шлем", "Разрез для глаз могли бы сделать и побольше..", 3, "Шлем");

                //Хилки                                    Название   Описание        Тип     Стак  Хил
        static HealConsumables apple = new HealConsumables("Яблоко", "Просто яблоко", "Еда", 15, 3);
        static HealConsumables smallHealPotion = new HealConsumables("Малое зелье лечения", "Сразу станет лучше", "Целебное зелье", 5, 10);
        static HealConsumables mediumHealPotion = new HealConsumables("Среднее зелье лечения", "Сразу станет лучше", "Целебное зелье", 5, 10);
        static HealConsumables largeHealPotion = new HealConsumables("Большое зелье лечения", "Сразу станет лучше", "Целебное зелье", 5, 10);




        public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
        {

            {"oldSword",oldSword},
            {"steelSword", steelSword},
            {"silverSword",silverSword },
            {"tornShirt", tornShirt},
            {"dirtyPants", dirtyPants },
            {"leatherJacket", leatherJacket },
            {"leatherPants", leatherPants },
            {"leatherGloves", leatherGloves },
            {"leatherHelmet", leatherHelmet },
            {"ironBreastplate", ironBreastplate },
            {"ironPants", ironPants },
            {"ironGloves", ironGloves },
            {"ironHelmet", ironHelmet },
            {"apple", apple },
            {"smallHealPotion", smallHealPotion },
            {"mediumHealPotion", mediumHealPotion },
            {"largeHealPotion", largeHealPotion }
        };



    }
}
