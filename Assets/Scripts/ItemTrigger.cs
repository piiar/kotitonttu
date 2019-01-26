using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour {
    public Item LastItem { get; private set; }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Item")) {
            print("enter " + other.name);
            Item item = other.gameObject.GetComponent<Item>();
            if (item && (item.isCarryable || item.isFixable || item.isUsable)) {
                LastItem = item;
            }
            else {
                LastItem = null;
            }
            print("LastItem: " + (LastItem ? LastItem.name : "null"));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Item")) {
            print("exit " + other.name);
            LastItem = null;
        }
    }
}
