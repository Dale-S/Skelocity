using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquippedUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    public Transform bootEquipSlot;
    private PlayerMovement player;

    private void Start()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        bootEquipSlot = itemSlotContainer.Find("BootEquipSlot");
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }
    
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnEquippedItemsChanged += Inventory_OnEquippedListChanged;
        RefreshInventoryItems();
    }

    

    private void Inventory_OnEquippedListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems()
    {
        // foreach (Transform child in itemSlotContainer)
        // {
        //     // Debug.Log(child.name);
        //     if (child == itemSlotTemplate) continue;
        //     Destroy(child.gameObject);
        // }
        
        foreach (var item in inventory.GetEquippedList())
        {
            Transform slot;
            
            switch (item.itemType)
            {
                default:
                case Item.ItemType.Boots:
                    Debug.Log("Entered");
                    slot = bootEquipSlot;
                    break;
                    
            }

            slot.GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UnequipItem(item);
                inventory.AddItem(item);
            };

            slot.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                inventory.UnequipItem(item);
                ItemWorld.DropItem(player.GetPosition(), item);
            };

            Image image = slot.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            
            
            // var itemSlot = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            // itemSlot.gameObject.SetActive(true);
            //
            // itemSlot.GetComponent<Button_UI>().ClickFunc = () =>
            // {
            //     
            // };
            // itemSlot.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            // {
            //     inventory.RemoveItem(item);
            //     ItemWorld.DropItem(player.GetPosition(), item);
            // };
            //
            // Image image = itemSlot.Find("Image").GetComponent<Image>();
            // image.sprite = item.GetSprite();
            //
            // TextMeshProUGUI uiText = itemSlot.Find("Text").GetComponent<TextMeshProUGUI>();
            // if (item.amount > 1)
            // {
            //     uiText.SetText(item.amount.ToString());
            // }
            // else
            // {
            //     uiText.SetText("");
            // }

        }
    }
}
