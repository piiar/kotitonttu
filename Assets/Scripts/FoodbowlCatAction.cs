﻿using UnityEngine;
using System.Collections;

public class FoodbowlCatAction : Action {

    public void Execute(GameObject actor, GameObject target) {
        Debug.Log("Cat eats food!");
        // - if bowl is empty, do what?
    }
}
