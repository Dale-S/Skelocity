using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ItemInfo itemToDisplay;

    private Image icon;
    
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();

        DisplayItems();
    }

    void DisplayItems()
    {
        if (itemToDisplay != null)
        {
            icon.enabled = true;
            icon.sprite = itemToDisplay.icon;
        }
        else
        {
            icon.enabled = false;
        }
    }
}
