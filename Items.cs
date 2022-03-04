﻿namespace Adventure_rpg
{



    public class Item //базовый класс предмета(любого)
    {
        public string name = "Default name";
        public string description = "Default description";
        public string type = string.Empty;
        public int maxSTACK = 1;

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }


    }
    public class Equipable : Item
    {
        public Equipable(string name, string description) : base(name, description)
        {
        }
    }

    public class Weapon : Equipable //класс оружия
    {
        public int damage = 0;
        public string canUse;

        public Weapon(string name, string description, string type, int damage, string canUse) : base(name, description)
        {

            this.damage = damage;
            this.type = type;
            this.maxSTACK = 1;
            this.canUse = canUse;
        }

        public static int GetWeaponDamage(Weapon weapon)
        {
            return weapon.damage;
        }
        public static string GetWeaponClass(Weapon weapon)
        {
            return weapon.canUse;
        }

    }

    public class Armor : Equipable
    {
        public int defencePoints;
        public string armorPart;
        public Armor(string name, string description, int defencePoints, string type) : base(name, description)
        {
            this.defencePoints = defencePoints;
            this.type = type;
        }
        public static int GetArmorDefence(Armor armor)
        {
            return armor.defencePoints;
        }
    }


    class Consumables : Item //класс применяемых предметов (зельки-хуельки)
    {
        public Consumables(string name, string description, string type, int maxStack) : base(name, description)
        {
            this.maxSTACK = maxStack;
        }


    }

    class HealConsumables : Consumables
    {
        public int healValue;
        public HealConsumables(string name, string description, string type, int maxStack, int healValue) : base(name, description, type, maxStack)
        {
            this.maxSTACK = maxStack;
            this.healValue = healValue;
            this.type = type.ToString();
        }
        public int Action()
        {
            return healValue;
        }
        public static int GetFoodHealInfo(HealConsumables healItem)
        {
            return healItem.healValue;
        }
    }










}
