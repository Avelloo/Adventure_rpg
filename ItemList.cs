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
        static Weapon oldSword = new Weapon("Старый меч", "Не лучшее оружие, но всё ещё может оказаться полезным.", "Оружие", 3, "Воин",2,1);
        static Weapon oldStaff = new Weapon("Старый посох", "Не лучшее оружие, но всё ещё может оказаться полезным.", "Оружие", 3, "Маг",2,1);
        static Weapon oldBow = new Weapon("Старый лук", "Не лучшее оружие, но всё ещё может оказаться полезным.", "Оружие", 3, "Лучник",2,1);

        // 1 тир
        static Weapon steelSword = new Weapon("Стальной меч", "Добротно сделанный меч.", "Оружие", 6,"Воин",10,4);
        static Weapon crystalStaff = new Weapon("Посох с хрусталем","Посох с инкрустировнным в него куском хрусталя", "Оружие",6,"Маг",10,4);
        static Weapon scoutBow = new Weapon("Лук разведчика","Гибкий, но в то же время очень прочный лук","Оружие",6,"Лучник",10,4);
        // 2 тир
        static Weapon silverSword = new Weapon("Серебряный меч", "На рукояти нарисовано множество замысловатых рисунков...", "Оружие", 12, "Воин",20,11);
        static Weapon rubyStaff = new Weapon("Рубиновый посох","Красивый посох с рубином. Очень ярко светится.","Оружие",12,"Воин",20,11);
        static Weapon silverBow = new Weapon("Серебряный лук","Лук из серебра с невероятно прочной тетивой","Оружие",12,"Лучник",20,11);


        // Броня                              Название       Описание                 Защита  Часть тела
                //стартовая
        static Armor tornShirt = new Armor("Рваная рубаха", "Ну, хотя бы не холодно...", 1, "Нагрудник",2,1);
        static Armor dirtyPants = new Armor("Грязные штаны", "Нужно будет хотя бы отмыть их :)...", 1, "Штаны",2,1);
                //Кожаный сет
        static Armor leatherJacket = new Armor("Кожаный нагрудник","Сшит из нескольких лоскутов кожи.",3,"Нагрудник",6,3);         
        static Armor leatherPants = new Armor("Кожаные штаны", "Задница потеет как не в себя...", 2, "Штаны",5,2);
        static Armor leatherGloves = new Armor("Кожаные перчатки", "Стиляга =)", 2, "Перчатки",5,2);
        static Armor leatherHelmet = new Armor("Кожаный шлем", "Скорее напоминает кожаную шапочку", 2, "Шлем",5,2);
                //Железный сет
        static Armor ironBreastplate = new Armor("Железный нагрудник", "После дня в таком ваши плечи вам точно не скажут «Спасибо».", 5, "Нагрудник",12,6);
        static Armor ironPants = new Armor("Железные штаны", "Неудобно двигаться, зато надежно.", 4, "Штаны",10,5);
        static Armor ironGloves = new Armor("Железные перчатки", "Пальцы как деревянные... ", 3, "Перчатки",10,5);
        static Armor ironHelmet = new Armor("Железный шлем", "Разрез для глаз могли бы сделать и побольше..", 3, "Шлем",10,5);

                //Хилки                                    Название   Описание        Тип     Стак  Хил
        static HealConsumables apple = new HealConsumables("Яблоко", "Просто яблоко", "Еда", 15, 3,2,1);
        static HealConsumables smallHealPotion = new HealConsumables("Малое зелье лечения", "Сразу станет лучше", "Целебное зелье", 5,10,4,2);
        static HealConsumables mediumHealPotion = new HealConsumables("Среднее зелье лечения", "Сразу станет лучше", "Целебное зелье", 5, 20,8,4);
        static HealConsumables largeHealPotion = new HealConsumables("Большое зелье лечения", "Сразу станет лучше", "Целебное зелье", 5, 40,15,8);




        public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
        {

            {"oldSword",oldSword},
            {"oldStaff",oldStaff},
            {"oldBow",oldBow},
            {"steelSword", steelSword},
            {"crystalStaff", crystalStaff},
            {"scoutBow", scoutBow },
            {"rubyStaff", rubyStaff},
            {"silverBow",silverBow},
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

        public static Dictionary<string, Item> traderItemList = new Dictionary<string, Item>
        {
            {"steelSword", steelSword},
            {"crystalStaff", crystalStaff},
            {"scoutBow", scoutBow },
            {"rubyStaff", rubyStaff},
            {"silverBow",silverBow},
            {"silverSword",silverSword },
            {"leatherJacket", leatherJacket },
            {"leatherPants", leatherPants },
            {"leatherGloves", leatherGloves },
            {"leatherHelmet", leatherHelmet },
            {"ironBreastplate", ironBreastplate },
            {"ironPants", ironPants },
            {"ironGloves", ironGloves },
            {"ironHelmet", ironHelmet },
            {"apple", apple },
            {"smallHealPotion", smallHealPotion }
        };



    }
}
