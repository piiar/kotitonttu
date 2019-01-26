using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public static double Score;
    public static double ScoreMax;
    public static int NumDamagedGoals;

    private List<Item> items;
    
    // Start is called before the first frame update
    void Start() {
        Score = 0;
        ScoreMax = 0;
        NumDamagedGoals = 0;

        items = new List<Item>();
        foreach (var item in GameObject.FindObjectsOfType<Item>()) {
            if (item.isGoal) {
                items.Add(item);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        double tickScore = 0;
        double tickScoreMax = 0;
        int tickNumDamagedGoals = 0;
        foreach (var item in items) {
            double itemScore = item.currentValue / item.maxValue;
            tickScore += itemScore;
            tickScoreMax += 1;

            if (item.currentValue < item.maxValue) {
                tickNumDamagedGoals++;
            }
        }

        Score += tickScore * Time.deltaTime;
        ScoreMax += tickScoreMax * Time.deltaTime;
        NumDamagedGoals = tickNumDamagedGoals;
    }
}
