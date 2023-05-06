using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    //private PlayerMovement player;

    private void Start()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetPlayer(PlayerMovement player)
    {
        //this.player = player;
    }
    
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public void ChangeInventoryAlpha()
    {
        var uiCanvas = GetComponent<CanvasGroup>();
        var uiAlpha = uiCanvas.alpha;
        if (uiAlpha == 1f)
        {
            uiCanvas.alpha = 0f;
        } else if (uiAlpha == 0f)
        {
            uiCanvas.alpha = 1f;
        }
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            // Debug.Log(child.name);
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        
        foreach (var item in inventory.GetItemList())
        {
            var itemSlot = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlot.gameObject.SetActive(true);

            itemSlot.GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.EquipItem(item);
            };
            itemSlot.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                inventory.RemoveItem(item);
               // ItemWorld.DropItem(player.GetPosition(), item);
            };
            
            Image image = itemSlot.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlot.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
        }
    }
    
}
