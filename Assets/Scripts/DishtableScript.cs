using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DishtableScript : MonoBehaviour {
    public GameObject WaterObject;

    public float WaterLevelLow;
    public float WaterLevelHigh;

    public AnimationCurve WaterLevelCurve;

    public bool WantAnimate;

    public WaterOverride Override;
    [Range(0f, 1f)]
    public float OverrideAnimationState;

    // From 0 to 1
    private float animationState;
    private bool animating;
    
    // Start is called before the first frame update
    void Start() {
        setWaterLevel(0);
    }

    // Update is called once per frame
    void Update() {
        if (Override == WaterOverride.Low) {
            setWaterLevel(0);
            return;
        }
        if (Override == WaterOverride.High) {
            setWaterLevel(1);
            return;
        }

        if (WantAnimate) {
            Animate();
            WantAnimate = false;
        }

        if (animating) {
            updateLevelForFrame();

            setWaterLevel(animationState);
        }
    }

    private float updateLevelForFrame() {
        if (Override == WaterOverride.Time) {
            return OverrideAnimationState;
        }

        animationState += Time.deltaTime;
        animationState = Mathf.Clamp01(animationState);
        return animationState;
    }

    private void setWaterLevel(float state) {
        float curveLevel = WaterLevelCurve.Evaluate(state);
        float waterLevel = Mathf.Lerp(WaterLevelLow, WaterLevelHigh, curveLevel);

        WaterObject.transform.position = new Vector3(
            WaterObject.transform.position.x,
            waterLevel,
            WaterObject.transform.position.z);
    }

    public void Animate() {
        animationState = 0;
        animating = true;
    }
}

public enum WaterOverride {
    None,
    Low,
    High,
    Time
}
