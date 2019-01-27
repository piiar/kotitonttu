using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public bool isLevelComplete = false;
    public static double Score;
    public static double ScoreMax;
    public static int NumDamagedGoals;
    public static int NumDestroyedObjects;
    public static int Health;
    public static int HealthMax;
    public static float StarsEarned = 0f;
    public ProgressIndicator Stars;

    private List<Item> items;
    float degree = 90f;
    public Transform secondsTransform;

    float elapsedLevelTime = 0f;
    const float levelTime = 120f;

    // Start is called before the first frame update
    void Start() {
        Score = 0;
        ScoreMax = 0;
        NumDamagedGoals = 0;

        items = new List<Item>();
        foreach (var item in GameObject.FindObjectsOfType<Item>()) {
            if (item.isScoreable) {
                items.Add(item);
                ScoreMax += item.maxValue * levelTime;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if(UIManager.instance.isPaused || isLevelComplete)
        {
            return;
        }
        int tickScore = 0;
        int tickNumDamagedGoals = 0;
        int tickDestroyedObjects = 0;
        foreach (var item in items) {
            if (item.isScoreable)
            {
                tickScore += (int)item.currentValue;
            }

            if (item.currentValue < item.maxValue) {
                tickNumDamagedGoals++;
            }
            if (item.currentValue <= 0) {
                tickDestroyedObjects++;
            }
        }
        Health = tickScore;

        Score += tickScore * Time.deltaTime;

        NumDamagedGoals = tickNumDamagedGoals;
        NumDestroyedObjects = tickDestroyedObjects;
        if (Score / ScoreMax >= 0.95f)
        {
            StarsEarned = 1f;
        }
        else if (Score / ScoreMax >= 0.80f)
        {
            StarsEarned = 0.66f;
        }
        else if (Score / ScoreMax >= 0.6f)
        {
            StarsEarned = 0.33f;
        }
        else
        {
            StarsEarned = 0f;
        }
        Stars.UpdateFillAmount(StarsEarned);
        if (elapsedLevelTime <= levelTime){
            elapsedLevelTime += Time.deltaTime;
            secondsTransform.localRotation =
                Quaternion.Euler(0f, 0f, (-360f / levelTime * elapsedLevelTime) + 90f);
        }
        else
        {
            isLevelComplete = true;
        }
    }
}
