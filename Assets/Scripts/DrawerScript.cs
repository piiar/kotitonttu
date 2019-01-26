using System;
using UnityEngine;

public class DrawerScript : MonoBehaviour {
    public GameObject RepairItem;

    public void Start() {
        
    }

    internal void PlayerAction(Player player, Item item) {
        if (player.GetCarriedItem() != null) {
            return;
        }

        var repairGameObject = GameObject.Instantiate(RepairItem);
        repairGameObject.transform.position = player.CarryItemPosition;
        var repairItem = repairGameObject.GetComponent<Item>();

        player.SetCarriedItem(repairItem);
    }
}