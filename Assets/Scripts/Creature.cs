using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {
    public Transform goal;
    private NavMeshAgent agent;
    private float timeUntilGoalChange = 10f;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        RandomizeGoal();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update() {
        timeUntilGoalChange -= Time.deltaTime;
        if (timeUntilGoalChange <= 0f) {
            timeUntilGoalChange = Random.Range(10f, 15f);
            RandomizeGoal();
        }
        if (goal.parent == null) { // target is not being carried
            agent.destination = goal.position;
        }
    }

    private void RandomizeGoal() {
        Item[] items = FindObjectsOfType<Item>().Where(item => item.isGoal).ToArray();
        int index = Random.Range(0, items.Length);
        goal = items[index].gameObject.transform;
    }
}
