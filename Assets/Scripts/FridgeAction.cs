using UnityEngine;
using System.Collections;
using System.Linq;

public class FridgeAction : Action {
    public GameObject foodPrefab;

    public void Execute(GameObject actor, Item target) {
        Debug.Log("FridgeAction");
        if (target.itemType == ItemType.Fridge) {
            Item[] foods = Object.FindObjectsOfType<Item>()
                .Where(item => item.itemType == ItemType.Food).ToArray();
            if (foods.Length > 0) {
                Player player = actor.GetComponent<Player>();
                player.SetCarriedItem(foods[0]);
            }
        }
    }
}
