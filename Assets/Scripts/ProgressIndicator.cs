using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {

    private float max;
    private float current = 1f;

    public RectTransform baseRect;
    public RectTransform healthRect;

    void Awake() {
        baseRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void UpdateBar(float percent) {
        current *= percent;
        //rect.rect.Set(0, 0, current, rect.rect.height);
        healthRect.localScale = new Vector3(current, 1, 1);
    }

    public void UpdateBarPos(Vector3 position)
    {
        baseRect.anchoredPosition = position;
    }
}