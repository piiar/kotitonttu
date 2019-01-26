using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Furniture = 0,
    CatTree = 1,
    CardboardBox = 2,
    Foodbowl = 3,
    Food = 4,
    Litterbox = 5

}

public class Item : MonoBehaviour {
    public ItemType itemType;
    public bool isGoal;
    public bool isCarryable;
    public bool isFixable;
    public int maxValue;
    public int currentValue;
    public int damageFactor;
    public int fixFactor;
    public double timeToResetDamageCountdown = 1f;
    public double timeToDamage;
    public double countdownToDamage;

    public void updateCountdownToDamage() {
        double deltaTime = Time.deltaTime;

        // pet has been away too long from the item, reset damage countdown
        if (deltaTime >= timeToResetDamageCountdown) {
            Debug.Log("Damage prevented");
            countdownToDamage = timeToDamage;
        }

        countdownToDamage -= deltaTime;
        Debug.Log("Countdown to damage " + countdownToDamage);
        if (countdownToDamage < 0f) {
            HandleDamage();
            countdownToDamage = timeToDamage;
        }
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
}