using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_rpg
{
    public class InventorySystem
    {
        
        public int maxInventorySlots = 10;
        public readonly List<InventoryCell> Inventory = new List<InventoryCell>();

        public void addItemToInventory(Item item, int amount)
        {
            while(amount > 0)
            {
                //если уже есть предмет с данным ID и у него еще есть место до полного стака
                if (Inventory.Exists(x => (x.thisItem.name == item.name) && (x.Quantity < item.maxSTACK)))
                {
                    // получаем его Объект в списке
                    InventoryCell currentCell = Inventory.First(x => (x.thisItem.name == item.name) && (x.Quantity < item.maxSTACK));
                    //сколько ещё можно туда добавить(в ячейку currentCell)
                    int maxAmountToAdd = item.maxSTACK - currentCell.Quantity;
                    //сколько добавить -> сколько запросили ИЛИ максимум сколько можно добавить в данную ячейку
                    int amountToAdd = Math.Min(amount, maxAmountToAdd);

                    currentCell.AddToCell(amountToAdd);

                    //вычитаем. если вычли столько, сколько хотели добавить, будет 0 и while закончится
                    amount -= amountToAdd;

                }
                else
                {
                    //Проверяем есть ли свободные слоты
                    if(Inventory.Count < maxInventorySlots)
                    {
                        //Создаем ячейку с нужным предметом, но без количества. Так как количество, которое
                        //мы хотим добавить всё ещё больше 0, то запустится цикл if (с 19 строчки)
                        Inventory.Add(new InventoryCell(item, 0));
                    }
                    // если нет свободных ячеек
                    else
                    {
                        throw new Exception("инвентарь полон, но до конца не допилен))");
                    }
                }
            }
            
        }

        public void removeItemFromInventory(Item item, int amount)
        {
            int deletedAmount = amount;
            while (deletedAmount > 0)
                {                    
                    if (Inventory.Exists(x => x.thisItem.name == item.name && x.Quantity > 0))
                    {
                        InventoryCell currentCell = Inventory.Last(x => (x.thisItem.name == item.name && x.Quantity > 0));
                        if (amount > currentCell.Quantity)
                        {
                            deletedAmount -= currentCell.Quantity;
                            currentCell.Quantity = 0;
                        }                        
                        else if(amount < currentCell.thisItem.maxSTACK)
                        {
                            deletedAmount -= currentCell.Quantity;
                            currentCell.Quantity -= amount;                         

                        }
                        
                    }
                    if(Inventory.Exists(x => x.thisItem.name == item.name && x.Quantity == 0))
                    {
                        InventoryCell currentCell = Inventory.First(x => (x.thisItem.name == item.name && x.Quantity == 0));
                        Inventory.Remove(currentCell);
                    }
                    amount = deletedAmount;
                }
            
            
            
            
        }


        public InventoryCell GetInventoryCell(int id)
        {
            if(Inventory.ElementAtOrDefault(id) != null)
            {
                return Inventory.ElementAt(id);
            }
            else
            {
                throw new Exception("Нет данных в этой ячейке инвентаря!");
            }
        }
        public object GetItemType(int id)
        {
            if (Inventory.ElementAtOrDefault(id) != null)
            {
                return Inventory.ElementAt(id);
            }
            else
            {
                throw new Exception("Нет данных в этой ячейке инвентаря!");
            }
        }
        public void StackInventory()
        {

        }
        public bool IsCellExist(int id)
        {
            return Inventory.ElementAtOrDefault(id) != null ? true : false;
        }

        public int CountItem(Item item)
        {
            int count = 0;

            for (int i = 0; i < GetMaxSlots(); i++)
            {

                if (IsCellExist(i))
                {
                    InventoryCell thisCell = GetInventoryCell(i);
                    if (thisCell.thisItem.name == item.name && thisCell.Quantity > 0)
                    {
                        count += thisCell.Quantity;
                    }

                }
            }

            //Console.WriteLine("ПРЕДМЕТ: " + item.name);
            //Console.WriteLine("НАСЧИТАЛ: " + count);
            return count;

        }

        public bool isCapableOfDeleting(Item item, int amount)//Проверка, можно ли удалить нужное колво предметов
        {
            return amount <= CountItem(item) ? true : false;
        } 
        public bool isCapableOfAdding(Item item, int amount)//Проверка, вместится ли нужное количество предметов данного типа 
        {
           
            int maxHold = 0;
            for (int i = 0; i < GetMaxSlots(); i++)
            {
                
                if (IsCellExist(i))
                {
                    InventoryCell thisCell = GetInventoryCell(i);
                    if (thisCell.thisItem.name == item.name && thisCell.Quantity < item.maxSTACK)
                    {
                        maxHold += item.maxSTACK - thisCell.Quantity;
                    }                  
                  
                    
                }
                else
                {
                    maxHold += item.maxSTACK;  
                }



            }
            //Console.WriteLine("ПРЕДМЕТ:" + item.name);
            //Console.WriteLine("ХОЧУ ХРАНИТЬ:" + amount);
            //Console.WriteLine("ГОТОВ ХРАНИТЬ:" + maxHold);

            return maxHold >= amount ? true : false;
        } 
        public int GetMaxSlots()
        {
            return maxInventorySlots;
        }

        
    }

    public class InventoryCell //класс ячейки
    {
        public int Quantity { get; set; }
        public Item thisItem { get; private set; }

        public InventoryCell(Item thisItem, int quantity)
        {
            this.thisItem = thisItem;
            this.Quantity = quantity;
        }
       
        public void AddToCell(int amountToAdd)
        {
            Quantity += amountToAdd;
        }
        public void RemoveFromCell(int amountToRemove)
        {
            Quantity -= amountToRemove;
        }
    } 

}
