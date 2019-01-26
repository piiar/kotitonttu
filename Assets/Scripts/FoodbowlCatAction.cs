using UnityEngine;
using System.Collections;

public class FoodbowlCatAction : Action {

    public void Execute(GameObject actor, Item target) {
        Debug.Log("Cat eats food!");
        if (target.itemType == ItemType.Foodbowl) {
            if (target.currentValue == target.maxValue) {
                target.HandleDamage();
            }
            else {
                // if bowl is empty, do what?
            }
        }
    }
}
