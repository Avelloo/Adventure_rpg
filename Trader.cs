namespace Adventure_rpg
{
    public class Trader
    {
        public bool firstTier = false;
        public bool secondTier = false;
        public bool thirdTier = false;

        InventorySystem firstTierTraderInventory = new InventorySystem(ItemList.traderFirstTier.Count);
        InventorySystem secondTierTraderInventory = new InventorySystem(ItemList.traderSecondTier.Count);
        InventorySystem thirdTierTraderInventory = new InventorySystem(ItemList.traderThirdTier.Count);

        public void AddItemsToTrader()
        {
            foreach(KeyValuePair<string,Item> i in ItemList.traderFirstTier)
            {
                if(i.Value.type == "Еда" || i.Value.type == "Целебное зелье")
                {
                    firstTierTraderInventory.addItemToInventory(i.Value,15);
                }
                else
                {
                    firstTierTraderInventory.addItemToInventory(i.Value, 1);
                }
            }
            foreach (KeyValuePair<string, Item> i in ItemList.traderSecondTier)
            {
                if (i.Value.type == "Еда" || i.Value.type == "Целебное зелье")
                {
                    secondTierTraderInventory.addItemToInventory(i.Value, 15);
                }
                else
                {
                    secondTierTraderInventory.addItemToInventory(i.Value, 1);
                }
            }
            foreach (KeyValuePair<string, Item> i in ItemList.traderThirdTier)
            {
                if (i.Value.type == "Еда" || i.Value.type == "Целебное зелье")
                {
                    thirdTierTraderInventory.addItemToInventory(i.Value, 15);
                }
                else
                {
                    thirdTierTraderInventory.addItemToInventory(i.Value, 1);
                }
            }
        }

        public void CheckLvL(Game currentGame)
        {
            if(currentGame.Character.CharLVL >= 1)
            {
                firstTier = true;
            }
            if(currentGame.Character.CharLVL >= 4)
            {
                secondTier = true;
            }
            if(currentGame.Character.CharLVL >= 7)
            {
                thirdTier = true;
            }
        }

        public void DisplayItems(Game currentGame, ArmorAndWeapon armorAndWeapon)
        {
            CheckLvL(currentGame);
            string[] options = new string[1];
            if (firstTier)
            {
                Array.Resize(ref options, options.Length + 1);
                options[options.Length - 1] = "Начальные товары";
            }

            if (secondTier)
            {
                Array.Resize(ref options, options.Length + 1);
                options[options.Length - 1] = "Продвинутые товары";
            }
            if (thirdTier)
            {
                Array.Resize(ref options, options.Length + 1);
                options[options.Length - 1] = "Лучшие товары";
            }
            Array.Resize(ref options, options.Length + 2);
            options[options.Length - 1] = "Назад";

            switch (systemInterface.DrawMenuAndReturnAction(options))
            {

            }
            

        }






    }
}
