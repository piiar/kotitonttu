using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    Vector3 movement;
    bool interaction;

    float rotationSpeed = 12f;
    float moveSpeed = 6f;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Move(Vector3 moveDirection, bool interaction)
    {
        ApplyRotationTo(moveDirection);

        // Move the controller
        var movement = moveDirection * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    private void ApplyRotationTo(Vector3 targetPosition)
    {
        Vector3 repositioning = targetPosition;
        if (repositioning != Vector3.zero)
        {
            repositioning.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(repositioning, Vector3.up);
            rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}
