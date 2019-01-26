using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboButton : MonoBehaviour {
    void Update() {
        if (Input.GetKey(KeyCode.T)) {
            Time.timeScale = 15;
        }
        else {
            Time.timeScale = 1;
        }
    }
}
