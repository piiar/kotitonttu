using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementManager movementManager;

    // Start is called before the first frame update
    void Awake()
    {
        movementManager = GetComponent<MovementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool interaction = Input.GetKeyDown(KeyCode.Space);
        Vector3 moveDirection = v * Vector3.forward + h * Vector3.right;

        movementManager.Move(moveDirection, interaction);
    }
}

// public class InputData
// {

//     public Vector3 move;
//     public Vector3 forward;
//     public Vector2 axis;
//     public bool interaction;


//     public InputData(Vector3 move, Vector3 forward, Vector2 axis, bool interaction = false)
//     {
//         this.move = move;
//         this.forward = forward;
//         this.axis = axis;
//         this.interaction = interaction;
//     }
// }