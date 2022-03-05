using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    internal class Game
    {
        private Character mainCharacter = new Character();
       
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
                    systemInterface.AddToInventory(mainCharacter.inventory, "oldSword", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "tornShirt", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "dirtyPants", 1, "Не хватает места!");
                    break;
                case "Маг":
                    systemInterface.AddToInventory(mainCharacter.inventory, "oldStaff", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "tornShirt", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "dirtyPants", 1, "Не хватает места!");
                    break;
                case "Лучник":
                    systemInterface.AddToInventory(mainCharacter.inventory, "oldBow", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "tornShirt", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "dirtyPants", 1, "Не хватает места!");
                    break;
                case "Странник":
                    Console.Clear();
                    Console.WriteLine("Ваш класс странник. Можете выбрать стартовое оружие:");
                    switch (systemInterface.DrawMenuAndReturnAction(new string[] {"Меч","Лук","Посох"}))
                    {
                        case "Меч":
                            systemInterface.AddToInventory(mainCharacter.inventory, "oldSword", 1, "Не хватает места!");
                            break;
                        case "Лук":
                            systemInterface.AddToInventory(mainCharacter.inventory, "oldBow", 1, "Не хватает места!");
                            break;
                        case "Посох":
                            systemInterface.AddToInventory(mainCharacter.inventory, "oldStaff", 1, "Не хватает места!");
                            break;

                    }
                    systemInterface.AddToInventory(mainCharacter.inventory, "apple", 5, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "tornShirt", 1, "Не хватает места!");
                    systemInterface.AddToInventory(mainCharacter.inventory, "dirtyPants", 1, "Не хватает места!");
                    break;
            }
        }
        public void ChooseAction()
        {
            Console.Clear();
            Console.WriteLine("Выберите, что хотите сделать:");
            switch (systemInterface.DrawMenuAndReturnAction(new string[] { "Идти в бой", "Зайти к торговцу","О персонаже","Сохранить игру","","","Выйти из игры" }))
            {
                case "Идти в бой":
                    Console.Clear();
                    Console.WriteLine("Ещё не сделано");
                    ChooseAction();
                    break;
                case "Зайти к торговцу":
                    Console.Clear();
                    Console.WriteLine("Ещё не сделано");
                    ChooseAction();
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
    }

}
