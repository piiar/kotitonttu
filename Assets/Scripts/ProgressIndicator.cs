using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {

    private float max;
    private float current = 1f;

    public RectTransform rect;

    // Update is called once per frame
    public void UpdateBar(float percent) {
        current *= percent;
        //rect.rect.Set(0, 0, current, rect.rect.height);
        rect.localScale = new Vector3(current, 1, 1);
    }
}