using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {
    public Transform goal;
    private NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update() {
        if (goal.parent == null) { // target is not being carried
            agent.destination = goal.position;
        }
    }
}
