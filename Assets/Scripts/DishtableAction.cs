using UnityEngine;
using System.Collections;

public class DishtableAction : Action {
    public void Execute(GameObject actor, Item target) {
        Debug.Log("Dishtable target: " + target);
        if (target.itemType == ItemType.Dishtable) {
            target.GetComponent<DishtableScript>().Animate();
        }
    }
}
