using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {
    public RectTransform baseRect;
    public RectTransform healthRect;
    private Image bar;

    void Awake() {
        bar = healthRect.GetComponent<Image>();
        baseRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void UpdateBar(float percent) {
        if (percent == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        healthRect.localScale = new Vector3(percent, 1, 1);
    }

    public void UpdateBarPos(Vector3 position)
    {
        baseRect.anchoredPosition = position;
    }

    public void UpdateFillAmount(float fill)
    {
        bar.fillAmount = fill;
    }
}