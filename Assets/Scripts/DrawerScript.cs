using System;
using UnityEngine;

public class DrawerScript : MonoBehaviour {
    public GameObject RepairItem;

    public void Start() {
        
    }

    internal void ItemAction(GameObject actor, Item item) {
        var repairGameObject = GameObject.Instantiate(RepairItem);
        repairGameObject.transform.position = actor.transform.position;
        var repairItem = repairGameObject.GetComponent<Item>();

        var player = actor.GetComponent<Player>();
        player.SetCarriedItem(repairItem);
    }
}