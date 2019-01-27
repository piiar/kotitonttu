using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    private Creature[] cats;
    private ScoreScript scoreScript;

    // Start is called before the first frame update
    void Start() {
        scoreScript = GetComponent<ScoreScript>();
        cats = FindObjectsOfType<Creature>();
    }

    // Update is called once per frame
    void Update() {
        if (scoreScript.isLevelComplete) {
            for (int i = 0; i < cats.Length; i++) {
                cats[i].Stop();
            }
        }
    }
}
