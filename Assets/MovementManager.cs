using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    Vector3 movement;
    bool interaction;

    float rotationSpeed = 12f;
    float moveSpeed = 6f;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    public void Move(Vector3 moveDirection, bool interaction)
    {
        ApplyRotationTo(moveDirection);

        // Move the controller
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void ApplyRotationTo(Vector3 targetPosition)
    {
        Vector3 repositioning = targetPosition;
        if (repositioning != Vector3.zero)
        {
            repositioning.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(repositioning, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
