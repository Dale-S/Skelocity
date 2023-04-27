using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ItemInfo itemToDisplay;

    public TextMeshProUGUI hoverText;

    public float textOffset = 0f;
    
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

    private void EnableToolTip()
    {
        Debug.Log("Mouse is over object");
        if (itemToDisplay != null)
        {
            var mousePos = Input.mousePosition;

            hoverText.text = itemToDisplay.description;
            hoverText.transform.position = new Vector3(mousePos.x + textOffset,
                mousePos.y, mousePos.z);
            hoverText.enabled = true;
        }
    }

    private void DisableToolTip()
    {
        hoverText.enabled = false;
    }
}
