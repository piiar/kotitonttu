using UnityEngine;
using System.Collections;

public class FoodbowlPlayerAction : Action {

    public void Execute(GameObject actor, Item target) {
        if (target.itemType == ItemType.Foodbowl && target.currentValue < target.maxValue) {
            Player player = actor.GetComponent<Player>();
            Item carriedItem = player.GetCarriedItem();
            if (carriedItem && carriedItem.itemType == ItemType.Food) {
                Debug.Log("Player fills foodbowl");
                target.HandleFixing();
                carriedItem.gameObject.transform.parent = null;
                // TODO find fridge location
                carriedItem.gameObject.transform.position = new Vector3(3.97f, 0.78f, -9.14f);
                player.SetCarriedItem(null);
            }
        }
    }
}
