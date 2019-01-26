using UnityEngine;
using System.Collections;

public class FoodbowlCatAction : Action {

    public void Execute(GameObject actor, Item target) {
        if (target.itemType == ItemType.Foodbowl) {
            if (target.currentValue == target.maxValue) {
                Debug.Log("Cat eats food!");
                target.HandleDamage();
            }
            else {
                Debug.Log("Foodbowl is empty!");
                Creature cat = actor.GetComponent<Creature>();
                cat.SetNewGoal();
            }
        }
    }
}
