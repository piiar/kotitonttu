using UnityEngine;
using System.Collections;

public class FoodbowlCatAction : Action {

    public void Execute(GameObject actor, GameObject target) {
        Debug.Log("FoodbowlCatAction " + target.name);
        // cat eats food in bowl
        // - if bowl is empty, do what?

    }
}
