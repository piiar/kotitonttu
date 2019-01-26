using UnityEngine;
using System.Collections;

public class FoodbowlPlayerAction : Action {

    public void Execute(GameObject actor, Item target) {
        Debug.Log("Player fills foodbowl");
        if (target.itemType == ItemType.Foodbowl && target.currentValue == 0) {
            Player player = actor.GetComponent<Player>();
            Item carriedItem = player.GetCarriedItem();
            if (carriedItem && carriedItem.itemType == ItemType.Food) {
                target.HandleFixing();
                carriedItem.gameObject.transform.parent = null;
                // TODO find fridge location
                carriedItem.gameObject.transform.position = new Vector3(3.97f, 0.78f, -9.14f);
                player.SetCarriedItem(null);
            }
        }
    }
}
