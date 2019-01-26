using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Furniture = 0,
    CatTree = 1,
    CardboardBox = 2,
    RubberDuck = 3
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

    private void HandleDamage()
    {
        if(this.currentValue > 0)
        {
            this.currentValue -= this.damageFactor;
            if (this.currentValue < 0)
            {
                this.currentValue = 0;
            }
        }
    }

    private void HandleFixing()
    {
        if (this.currentValue < this.maxValue)
        {
            this.currentValue += this.fixFactor;
            if (this.currentValue > this.maxValue)
            {
                this.currentValue = this.maxValue;
            }
        }
    }
}