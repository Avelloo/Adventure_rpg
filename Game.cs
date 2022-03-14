using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class Game
    {
        private Character mainCharacter = new Character();
        Trader trader = new Trader();
        internal Character Character { get => mainCharacter; set => mainCharacter = value; }

        public void Gameplay()
        {
            
            mainCharacter.CreateCharacter();
            AddStartItems(mainCharacter);
            ChooseAction();

            
            
        }

        public void AddStartItems(Character character)
        {
            switch (character.Proffesion)
            {
                case "Воин":
                    systemInterface.AddToInventory(mainCharacter.characterInventory, "oldSword", 1, "Не хватает места!");
                    break;
                case "Маг":
                    systemInterface.AddToInventory(mainCharacter.characterInventory, "oldStaff", 1, "Не хватает места!");
                    break;
                case "Лучник":
                    systemInterface.AddToInventory(mainCharacter.characterInventory, "oldBow", 1, "Не хватает места!");
                    
                    break;
                case "Странник":
                    Console.Clear();
                    Console.WriteLine("Ваш класс странник. Можете выбрать стартовое оружие:");
                    switch (systemInterface.DrawMenuAndReturnAction(new string[] {"Меч","Лук","Посох"}))
                    {
                        case "Меч":
                            systemInterface.AddToInventory(mainCharacter.characterInventory, "oldSword", 1, "Не хватает места!");
                            break;
                        case "Лук":
                            systemInterface.AddToInventory(mainCharacter.characterInventory, "oldBow", 1, "Не хватает места!");
                            break;
                        case "Посох":
                            systemInterface.AddToInventory(mainCharacter.characterInventory, "oldStaff", 1, "Не хватает места!");
                            break;

                    }
                    break;
            }
            systemInterface.AddToInventory(mainCharacter.characterInventory, "tornShirt", 1, "Не хватает места!");
            systemInterface.AddToInventory(mainCharacter.characterInventory, "dirtyPants", 1, "Не хватает места!");
            systemInterface.AddToInventory(mainCharacter.characterInventory, "apple", 5, "Не хватает места!");
            trader.AddItemsToTrader();
        }
        public void ChooseAction()
        {
            mainCharacter.RecalculateStats(mainCharacter.ArmorAndWeapon);
            Console.Clear();

            systemInterface.DisplayShortCharInfo(mainCharacter);

            Console.WriteLine(" Выберите, что хотите сделать:");
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Идти в бой", "Зайти к торговцу","О персонаже","Сохранить игру","","","Выйти из игры" }))
            {
                case "Идти в бой":
                    Console.Clear();
                    Console.WriteLine("Ещё не сделано");
                    ChooseAction();
                    break;
                case "Зайти к торговцу":
                    Console.Clear();
                    TraderOptions();
                    break;
                case "О персонаже":
                    Console.Clear();
                    mainCharacter.Greetings(this);
                    ChooseAction();
                    break;
                case "Сохранить игру":
                    Console.Clear();
                    Console.WriteLine("Ещё не сделано");
                    ChooseAction();
                    break;
                case "Выйти из игры":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    ChooseAction();
                    break;
            }
        }


        public void TraderOptions()
        {
            Console.Clear();
            Console.WriteLine();
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Купить предметы", "Продать предметы", "", "Назад" }))
            {
                case "Купить предметы":
                    Console.Clear();
                    trader.DisplayItems(this, mainCharacter.ArmorAndWeapon);
                    break;
                case "Продать предметы":
                    Console.Clear();
                    trader.SellItems(this, mainCharacter.ArmorAndWeapon);
                    break;
                case "Назад":
                    ChooseAction();
                    break;
                default:
                    TraderOptions();
                    break;


            }
        }
    }

}
