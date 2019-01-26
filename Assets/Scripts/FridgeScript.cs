using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeScript : MonoBehaviour {
    public GameObject FoodObject;

    internal void PlayerAction(Player player, Item item) {
        if (player.GetCarriedItem() != null) {
            return;
        }

        var go = GameObject.Instantiate(FoodObject);
        go.transform.position = player.CarryItemPosition;
        var newItem = go.GetComponent<Item>();

        player.SetCarriedItem(newItem);
    }
}