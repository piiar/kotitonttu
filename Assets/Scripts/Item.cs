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
    Fridge = 7
}

public class Item : MonoBehaviour {
    public ItemType itemType;
    public bool isPickedUp = false;
    public bool isGoal;
    public bool isCarryable;
    public bool isFixable;
    public bool isUsable;
    public int maxValue;
    public int currentValue;
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
        }
    }

    public void PickedUp() {
        PreventDamage();
        isPickedUp = true;
    }

    public void DroppedDown() {
        isPickedUp = false;
    }
}