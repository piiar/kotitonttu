using UnityEngine;
using System.Collections;

public class FoodbowPlayerAction : Action {

    public void Execute(GameObject actor, Item target) {
        Debug.Log("Player fills foodbowl");
        // TODO check if player carries food
        if (target.itemType == ItemType.Foodbowl && target.currentValue == 0) {
            target.HandleFixing();
        }

    }
}
