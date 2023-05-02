using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld spawnItemWorld(Vector3 position, Item item)
    {
        var transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 pos, Item item)
    {
       var itemWorld = spawnItemWorld(pos + Vector3.right * 5f, item);
       itemWorld.GetComponent<Rigidbody>().AddForce(Vector3.right * 5f, ForceMode.Impulse);
       return itemWorld;
    }
    
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshProUGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(Item newItem)
    {
        this.item = newItem;
        spriteRenderer.sprite = newItem.GetSprite();
        if (item.amount > 1)
        {
            textMeshProUGUI.SetText(item.amount.ToString());
        }
        else
        {
            textMeshProUGUI.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
