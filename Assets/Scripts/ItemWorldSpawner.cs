using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
   public Item item;

   private void Start()
   {
      // Debug.Log("Item Spawned");
      // Debug.Log(item.amount);
      ItemWorld.spawnItemWorld(this.transform.position, item);
      gameObject.SetActive(false);
      Destroy(gameObject);
   }
}
