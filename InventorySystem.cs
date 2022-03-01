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
            Console.WriteLine("removeItemFromInventory еще не сделано");
        }

        public InventoryCell GetInventoryCell(int id)
        {
            if(Inventory.ElementAtOrDefault(id) != null)
            {
                return Inventory.ElementAt(id);
            }
            else
            {
                throw new Exception("Нет данных в этой ячейке инвентаря!!");
            }
        }
        public bool IsCellExist(int id)
        {
            return Inventory.ElementAtOrDefault(id) != null ? true : false;
        }

        public int GetMaxSlots()
        {
            return maxInventorySlots;
        }
    }

    public class InventoryCell
    {
        public int Quantity { get; private set; }
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
    } //класс ячейки
}
