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

        if (interaction) {
            HandleInteraction();
        }

        // Prevent the player for spinning due to physics issues when moving stuff and colliding
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

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

    private Item FindNearestItem(Vector3 center) {
        float radius = 2f;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        for (int i = 0; i < hitColliders.Length; i++) {
            Item item = hitColliders[i].gameObject.GetComponent<Item>();
            if (item && (item.isCarryable || item.isFixable)) {
                return item;
            }
        }
        return null;
    }

    private void HandleInteraction() {
        if (carriedObject != null) {
            // Drop
            carriedObject.transform.SetParent(null);
            carriedObject = null;
        }
        else {
            // Pick up
            Item item = FindNearestItem(transform.position);
            if (item && item.isCarryable) {
                item.gameObject.transform.SetParent(transform);
                carriedObject = item.gameObject;
                animator.SetBool(pickupHash, false);
            }
            else if (item) {
                Action action = null;
                switch (item.itemType) {
                    case ItemType.Foodbowl:
                        action = new FoodbowPlayerAction();
                        break;
                    case ItemType.Furniture:
                        break;


                    animator.SetBool(pickupHash, true);
                }
                if (action != null) {
                    action.Execute(this.gameObject, item);
                }
            }
        }
    }
}
