using UnityEngine;
using System.Collections;

public class FoodbowPlayerAction : Action {

    public void Execute(GameObject actor, GameObject target) {
        Debug.Log("Player fills foodbowl");
        // if player carries food, fill the bowl
    }
}
