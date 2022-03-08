namespace Adventure_rpg
{



    public class Item //базовый класс предмета(любого)
    {
        public string name = "Default name";
        public string description = "Default description";
        public string type = string.Empty;
        public int maxSTACK = 1;
        public int buyPrice = 0;
        public int sellPrice = 0;

        public Item(string name, string description, int buyPrice, int sellPrice)
        {
            this.name = name;
            this.description = description;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
        }


    }
    public class Equipable : Item
    {
        public Equipable(string name, string description, int buyPrice, int sellPrice) : base(name, description, buyPrice, sellPrice)
        {
        }
    }

    public class Weapon : Equipable //класс оружия
    {
        public int damage = 0;
        public string canUse;

        public Weapon(string name, string description, string type, int damage, string canUse, int buyPrice, int sellPrice) : base(name, description, buyPrice, sellPrice)
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
        public Armor(string name, string description, int defencePoints, string type, int buyPrice, int sellPrice) : base(name, description, buyPrice, sellPrice)
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
        public Consumables(string name, string description, string type, int maxStack, int buyPrice, int sellPrice) : base(name, description, buyPrice, sellPrice)
        {
            this.maxSTACK = maxStack;
        }


    }

    class HealConsumables : Consumables
    {
        public int healValue;
        public HealConsumables(string name, string description, string type, int maxStack, int healValue, int buyPrice, int sellPrice) : base(name, description, type, maxStack, buyPrice, sellPrice)
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
