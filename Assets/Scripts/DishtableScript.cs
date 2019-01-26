using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DishtableScript : MonoBehaviour {
    public GameObject WaterObject;
    public GameObject WaterObjectForPlayer;

    // Y coords for water object
    public float WaterLevelLow;
    public float WaterLevelHigh;

    // T = [0,1]
    // Value should go from 0 to 1, and back to 0
    public AnimationCurve WaterLevelCurve;

    // Use these for development
    public WaterOverride Override;
    [Range(0f, 1f)]
    public float OverrideAnimationState;
    public bool WantAnimate;

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
            updateAnimationState();

            setWaterLevel(animationState);
        }
    }

    private float updateAnimationState() {
        if (Override == WaterOverride.Time) {
            return OverrideAnimationState;
        }

        animationState += Time.deltaTime;

        if (animationState > 1) {
            animationState = 0;
            animating = false;
        }

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
        if (animating) {
            return;
        }

        animationState = 0;
        animating = true;
    }

    internal void PlayerAction(Player player, Item item) {
        if (player.GetCarriedItem() != null) {
            return;
        }

        Animate();

        var waterGameObject = GameObject.Instantiate(WaterObjectForPlayer);
        waterGameObject.transform.position = player
                .gameObject.transform.position;
        var waterItem = waterGameObject.GetComponent<Item>();

        player.SetCarriedItem(waterItem);
    }
}

public enum WaterOverride {
    None,
    Low,
    High,
    Time
}
