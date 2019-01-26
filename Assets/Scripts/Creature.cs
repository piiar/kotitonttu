using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {

    private readonly int speedHash = Animator.StringToHash("Speed");

    public Transform goal;
    private Transform previousGoal;
    private NavMeshAgent agent;
    private Animator animator;
    private float timeUntilGoalChange = 10f;

    void Awake() {
        animator = GetComponentInChildren<Animator>();
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

        animator.SetFloat(speedHash, Mathf.Min(agent.velocity.magnitude, 1f));
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
        Action action = null;
        switch (goalObject.GetComponent<Item>().itemType) {
            case ItemType.CardboardBox:
                break;
            case ItemType.Furniture:
                action = new FurnitureCatAction();
                break;
            case ItemType.Foodbowl:
                action = new FoodbowlCatAction();
                break;
        }
        if (action != null) {
            action.Execute(this.gameObject, goalObject);
        }
    }
}
