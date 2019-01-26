using UnityEngine;
using System.Collections;

public class FurnitureCatAction : Action {

    public void Execute(GameObject actor, Item target) {
        if(target.itemType == ItemType.Furniture) {
            target.UpdateCountdownToDamage();
        }
    }
}
