using UnityEngine;
using System.Collections;

public interface Action {
    void Execute(GameObject actor, GameObject target);
}
