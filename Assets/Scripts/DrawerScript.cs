using System;
using UnityEngine;

public class DrawerScript : MonoBehaviour {
    public GameObject RepairItem;

    public void Start() {
        
    }

    internal void PlayerAction(Player player, Item item) {
        var repairGameObject = GameObject.Instantiate(RepairItem);
        repairGameObject.transform.position = player
                .gameObject.transform.position;
        var repairItem = repairGameObject.GetComponent<Item>();

        player.SetCarriedItem(repairItem);
    }
}