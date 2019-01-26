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
    public int maxValue;
    public int currentValue;
}
