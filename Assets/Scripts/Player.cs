using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int pickupHash = Animator.StringToHash("PickUp");

    Vector3 movement;

    float rotationSpeed = 12f;
    float moveSpeed = 6f;

    GameObject carriedObject = null;

    private new Rigidbody rigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void Move(Vector3 moveDirection, bool interaction) {
        ApplyRotationTo(moveDirection);

        // Move the controller
        var movement = moveDirection * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);

        HandleInteraction(interaction);

        animator.SetFloat(speedHash, Mathf.Min(moveDirection.magnitude, 1f));
    }

    private void ApplyRotationTo(Vector3 targetPosition) {
        Vector3 repositioning = targetPosition;
        if (repositioning != Vector3.zero) {
            repositioning.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(repositioning, Vector3.up);
            rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }

    private GameObject FindNearestObject(Vector3 center) {
        float radius = 2f;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        for (int i = 0; i < hitColliders.Length; i++) {
            Item item = hitColliders[i].gameObject.GetComponent<Item>();
            if (item && item.isCarryable) {
                return hitColliders[i].gameObject;
            }
        }
        return null;
    }

    private void HandleInteraction(bool interaction) {
        if (interaction) {
            if (carriedObject != null) {
                // Drop
                carriedObject.transform.SetParent(null);
                carriedObject = null;
                animator.SetBool(pickupHash, false);
            }
            else {
                // Pick up
                GameObject obj = FindNearestObject(transform.position);
                if (obj) {
                    obj.transform.SetParent(transform);
                    carriedObject = obj;

                    animator.SetBool(pickupHash, true);
                }
            }
        }
    }
}
