using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {
    public Transform goal;
    private Transform previousGoal;
    private NavMeshAgent agent;
    private float timeUntilGoalChange = 10f;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        RandomizeGoal();
        agent.SetDestination(goal.position);
    }

    // Update is called once per frame
    void Update() {
        timeUntilGoalChange -= Time.deltaTime;
        if (goal && goal.parent == null) { // target is not being carried
            agent.SetDestination(goal.position);
        }
        if (timeUntilGoalChange <= 0f) {
            timeUntilGoalChange = Random.Range(10f, 15f);
            RandomizeGoal();
        }
        else if (goal && !agent.pathPending && agent.remainingDistance < 1.5f) {
            // Reached goal
            DoAction(goal.gameObject);
            goal = null;
            timeUntilGoalChange = 5f;
        }
    }

    private void RandomizeGoal() {
        Item[] items = FindObjectsOfType<Item>().Where(item => item.isGoal).ToArray();
        do {
            int index = Random.Range(0, items.Length);
            goal = items[index].gameObject.transform;
        } while (goal == previousGoal);
        previousGoal = goal;
        agent.SetDestination(goal.position);
        Debug.Log("Next goal: " + goal.gameObject.name);
    }

    private void DoAction(GameObject goalObject) {
        Action action = new FoodbowlCatAction();
        action.Execute(this.gameObject, goalObject);
    }
}
