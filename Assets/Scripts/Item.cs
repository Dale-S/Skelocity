using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
   public enum ItemType
   {
      Sword,
      Boots,
      Spear
   }

   public ItemType itemType;
   public int amount;

   public Sprite GetSprite()
   {
      switch (itemType)
      {
         default:
         case ItemType.Spear: return ItemAssets.Instance.spearSprite;
         case ItemType.Sword: return ItemAssets.Instance.swordSprite;
         case ItemType.Boots: return ItemAssets.Instance.bootsSprite;
      }
   }

   public bool IsStackable()
   {
      switch (itemType)
      {
         default:
         case ItemType.Boots:
         case ItemType.Spear:
         case ItemType.Sword:
            return false;
      }
   }
}
