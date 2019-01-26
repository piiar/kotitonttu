using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public static double Score;
    public static double ScoreMax;
    public static int NumDamagedGoals;
    public static int NumDestroyedObjects;
    public static int Health;
    public static int HealthMax;

    private List<Item> items;
    
    // Start is called before the first frame update
    void Start() {
        Score = 0;
        ScoreMax = 0;
        NumDamagedGoals = 0;

        items = new List<Item>();
        foreach (var item in GameObject.FindObjectsOfType<Item>()) {
            if (item.isScoreable) {
                items.Add(item);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        int tickScore = 0;
        int tickScoreMax = 0;
        int tickNumDamagedGoals = 0;
        int tickDestroyedObjects = 0;
        foreach (var item in items) {
            if (item.isScoreable)
            {
                tickScore += (int)item.currentValue;
                tickScoreMax += (int)item.maxValue;
            }

            if (item.currentValue < item.maxValue) {
                tickNumDamagedGoals++;
            }
            if (item.currentValue <= 0) {
                tickDestroyedObjects++;
            }
        }
        Health = tickScore;
        HealthMax = tickScoreMax;

        Score += tickScore * Time.deltaTime;
        ScoreMax += tickScoreMax * Time.deltaTime;

        NumDamagedGoals = tickNumDamagedGoals;
        NumDestroyedObjects = tickDestroyedObjects;
    }
}
