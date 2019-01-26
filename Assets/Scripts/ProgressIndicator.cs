using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {
    private int max = 100;
    private int current = 50;

    // Update is called once per frame
    void Update() {
        GetComponent<Image>().fillAmount = FillAmount();
    }

    public void SetMax(int max) {
        this.max = max;
    }

    private float FillAmount() {
        return current / max;
    }
}