using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public event EventHandler OnItemEquip;

    public event EventHandler OnItemUnequip;
    
    private List<Item> itemList;
    private List<Item> equippedItems;
    public Item lastRemoved;

    public Inventory()
    {
        itemList = new List<Item>();
        equippedItems = new List<Item>();
        
        AddItem(new Item{ itemType = Item.ItemType.Sword, amount = 1});
        AddItem(new Item{ itemType = Item.ItemType.Boots, amount = 1});
        AddItem(new Item{ itemType = Item.ItemType.Spear, amount = 1});
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (var inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (var inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void EquipItem(Item item)
    {
        bool itemAlreadyInInventory = false;
        foreach (var equipItem in equippedItems)
        {
            if (equipItem.itemType == item.itemType)
            {
                itemAlreadyInInventory = true;
                break;
            }
        }

        if (!itemAlreadyInInventory)
        {
            equippedItems.Add(item);
            RemoveItem(item);
        }


        OnItemEquip?.Invoke(this, EventArgs.Empty);
    }
    
    public void UnequipItem(Item item)
    {
      
        Item itemInInventory = null;
        foreach (var equipItem in equippedItems)
        {
            if (equipItem.itemType == item.itemType)
            {
                equipItem.amount -= item.amount;
                itemInInventory = equipItem;
                lastRemoved = item;
                break;
            }
        }

        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            equippedItems.Remove(item);
        }
        
        
        OnItemUnequip?.Invoke(this, EventArgs.Empty);
    }
    
    public List<Item> GetItemList()
    {
        return itemList;
    }

    public List<Item> GetEquippedList()
    {
        return equippedItems;
    }
}
