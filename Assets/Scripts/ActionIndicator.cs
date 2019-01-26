using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionIndicator : MonoBehaviour
{
    public Image mischief;
    public Image food;
    public Image zzz;

    public void SetAction(ItemType type) {
        switch(type) {
            case ItemType.Furniture:
                mischief.enabled = true;
                food.enabled = false;
                break;
            case ItemType.Foodbowl:
                food.enabled = true;
                mischief.enabled = false;
                break;
            // case ItemType.Foodbowl:
            //     indicator.sprite = food.sprite;
            //     break;
        }
    }

}
