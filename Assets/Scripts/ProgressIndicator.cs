using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {
    public RectTransform rect;

    // Update is called once per frame
    public void UpdateBar(float percent) {
        rect.localScale = new Vector3(percent, 1, 1);
    }
}