using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    CatTree = 0,
    CardboardBox = 1,
    RubberDuck = 2
}

public class Item : MonoBehaviour {
    public ItemType itemType;
    public bool isGoal;
    public bool isCarryable;
}
