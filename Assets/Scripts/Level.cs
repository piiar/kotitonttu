using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour {
    private Creature[] cats;
    private ScoreScript scoreScript;

    // Start is called before the first frame update
    void Start() {
        scoreScript = GetComponent<ScoreScript>();
        cats = FindObjectsOfType<Creature>();

        // Spawn extra cats
        Quaternion rot = cats[0].transform.rotation;
        Vector3 pos = cats[0].transform.position;
        Vector3 incr = new Vector3(.5f, 0, 0);
        for (int i = 0; i < LevelManager.CurrentLevel.ExtraCats; i++) {
            pos += incr;
            Instantiate(cats[0], pos, rot);
        }



        //int bowlsProcessed = 0;
        //foreach (var item in FindObjectsOfType<Item>()) {
        //    if (item.itemType == ItemType.Foodbowl) {
        //        var bowl = item.GetComponent<BowlScript>()
        //        bowlsProcessed++;
        //    }
        //}


        // :D
        // Disable too many extra bowls
        FindObjectsOfType<Item>()
            .Where(i => i.itemType == ItemType.Foodbowl)
            .Where(i => i.GetComponent<BowlScript>().IsExtra)
            .Skip(LevelManager.CurrentLevel.ExtraBowls)
            .ForEach(i => i.gameObject.SetActive(false));
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

public static class LinqEx {
    // Who is this not built-in??
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
        foreach (T element in source) {
            action(element);
        }
    }
}