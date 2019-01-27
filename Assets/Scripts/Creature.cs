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
    private GameObject goalTarget;

    void Awake() {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        RandomizeGoal();
        agent.SetDestination(goal.position);
    }

    // Update is called once per frame
    void Update() {
        if (agent.isStopped) {
            return;
        }
        timeUntilGoalChange -= Time.deltaTime;
        if (goal && goal.parent == null) { // target is not being carried
            goalTarget = null;
            agent.SetDestination(goal.position);

            UIManager.instance.UpdateThinkBubble(transform);
        }
        if (timeUntilGoalChange <= 0f) {
            goalTarget = null;
            timeUntilGoalChange = Random.Range(10f, 15f);
            RandomizeGoal();
        }
        else if (goal && !agent.pathPending && agent.remainingDistance < 1.5f) {
            // Reached goal
            goalTarget = goal.gameObject;
            DoAction(goalTarget);
            goal = null;
            timeUntilGoalChange = 5f;

            UIManager.instance.CloseThinkBubble(transform.name);
        }
        if (goalTarget) {
            DoRepeatingAction(goalTarget);
        }

        animator.SetFloat(speedHash, Mathf.Min(agent.velocity.magnitude, 1f));
    }

    private void RandomizeGoal() {
        Item[] items = FindObjectsOfType<Item>().Where(item => item.isGoal && item.currentValue > 0).ToArray();
        if (items.Length > 1) {
            do {
                int index = Random.Range(0, items.Length);
                goal = items[index].gameObject.transform;
            } while (goal == previousGoal);
        }
        else if (items.Length == 1) {
            goal = items[0].gameObject.transform;
        }
        else if (items.Length == 0) {
            goal = null;
        }
        previousGoal = goal;

        if (goal) {
            agent.SetDestination(goal.position);
            Debug.Log("Next goal: " + goal.gameObject.name);

            Item foundItem = goal.GetComponent<Item>();
            UIManager.instance.OpenThinkBubble(transform, foundItem.itemType);
        }
    }

    public void SetNewGoal() {
        goalTarget = null;
        timeUntilGoalChange = Random.Range(10f, 15f);
        RandomizeGoal();
    }

    private void DoAction(GameObject goalObject) {
        CreatureAction action = null;
        Item item = goalObject.GetComponent<Item>();
        if (item == null) {
            return;
        }
        switch (item.itemType) {
            case ItemType.CardboardBox:
                break;
            case ItemType.Furniture:
                action = new FurnitureCatAction().AsCreatureAction();
                break;
            case ItemType.Foodbowl:
                var bowl = item.GetComponent<BowlScript>();
                action = bowl.CatAction;
                break;
        }
        if (action != null) {
            action(this, item);
        }
    }

    private void DoRepeatingAction(GameObject goalObject) {
        Action action = null;
        Item item = goalObject.GetComponent<Item>();
        if (item == null) {
            return;
        }
        switch (item.itemType) {
            case ItemType.Furniture:
                action = new FurnitureCatAction();
                break;
        }
        if (action != null) {
            action.Execute(this.gameObject, item);
        }
    }

    public void Stop() {
        agent.isStopped = true;
    }
}
