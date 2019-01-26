using UnityEngine;
using System.Collections;

public class FoodbowlPlayerAction : Action {

    public void Execute(GameObject actor, Item target) {
        Debug.Log("Player fills foodbowl");
        if (target.itemType == ItemType.Foodbowl && target.currentValue == 0) {
            Player player = actor.GetComponent<Player>();
            Item carriedItem = player.GetCarriedItem();
            if (carriedItem.itemType == ItemType.Food) {
                target.HandleFixing();
                player.ResetFood();
            }
        }

    }
}
