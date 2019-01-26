using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Furniture = 0,
    CatTree = 1,
    CardboardBox = 2,
    Foodbowl = 3,
    Food = 4,
    Litterbox = 5,
    Dishtable = 6,
    Fridge = 7,
    Drawer = 8,
    Repair = 9,
    SleepingSpot = 10,
    Water = 11
}

public class Item : MonoBehaviour {
    public ItemType itemType;
    public bool isScoreable;
    public bool isPickedUp = false;
    public bool isGoal;
    public bool isCarryable;
    public bool isFixable;
    public bool isUsable;
    public float maxValue;
    public float currentValue;
    public int damageFactor = 1;
    public int fixFactor = 1;
    public double timeToDamage = 3f;
    public double countdownToDamage = 3f;

    public void UpdateCountdownToDamage() {
        if (isPickedUp) {
            return;
        }

        double deltaTime = Time.deltaTime;

        countdownToDamage -= deltaTime;
        if (countdownToDamage < 0f) {
            HandleDamage();
            countdownToDamage = timeToDamage;
        }
    }

    public void PreventDamage() {
        Debug.Log("Damage prevented!");
        countdownToDamage = timeToDamage;
    }

    public void HandleDamage() {
        if (currentValue > 0) {
            currentValue -= damageFactor;
            Debug.Log("Item damaged, value left: " + currentValue + "/" + maxValue);
            UIManager.instance.UpdateProgessIndicator(transform.name, currentValue / maxValue);
            if (currentValue < 0) {
                Debug.Log("Item destroyed");
                currentValue = 0;
            }
        }
    }

    public void HandleFixing() {
        if (currentValue < maxValue) {
            currentValue += fixFactor;
            Debug.Log("Item fixed, value left: " + currentValue + "/" + maxValue);
            if (currentValue > maxValue) {
                currentValue = maxValue;
            }
            UIManager.instance.UpdateProgessIndicator(transform.name, currentValue / maxValue);
        }
    }

    public void PickedUp() {
        PreventDamage();
        isPickedUp = true;
    }

    public void DroppedDown() {
        isPickedUp = false;
    }

    internal void PlayerRepairAction(Player player, Item item) {
        if (currentValue == 0 || currentValue == maxValue) {
            return;
        }
        if (fixFactor == 0) {
            return;
        }
        if (!player.HasItem(ItemType.Repair)) {
            return;
        }

        HandleFixing();
        GameObject.Destroy(player.GetCarriedItem().gameObject);
        player.SetCarriedItem(null);
    }
}