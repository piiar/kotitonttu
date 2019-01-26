using UnityEngine;
using System.Collections;

public class FurnitureCatAction : Action {

    public void Execute(GameObject actor, GameObject target) {
        Debug.Log("Cat scratches furniture!");
    }
}
