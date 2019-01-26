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

    private ItemTrigger itemFinder;
    private new Rigidbody rigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        itemFinder = GetComponentInChildren<ItemTrigger>();
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

    private void HandleInteraction() {
        Item item = itemFinder.LastItem;
        if (item) {
            Action action = null;
            switch (item.itemType) {
                case ItemType.Foodbowl:
                    action = new FoodbowlPlayerAction();
                    break;
                case ItemType.Dishtable:
                    action = new DishtableAction();
                    break;
                case ItemType.Fridge:
                    action = new FridgeAction();
                    break;
                case ItemType.Drawer:
                    var drawer = item.GetComponent<DrawerScript>();
                    action = new ActionInstance(drawer.ItemAction);
                    break;
                case ItemType.Furniture:
                    break;
            }

            if (action != null) {
                action.Execute(this.gameObject, item);
            }
            else if (!carriedObject && item.isCarryable) {
                // Pick up
                PickupItem(item);

            }
        }
        if (!item && carriedObject != null) {
            // Drop
            StartCoroutine(togglePickupAnimation(() => {
                animator.SetLayerWeight(1, 0f);
                carriedObject.transform.SetParent(null);
                carriedObject.GetComponent<Item>().DroppedDown();
                carriedObject = null;
            }));
        }
    }

    IEnumerator togglePickupAnimation(System.Action callback) {
        animator.SetTrigger(pickupHash);
        yield return new WaitForSeconds(0.5f);
        callback();
    }

    private void PickupItem(Item item) {
        StartCoroutine(togglePickupAnimation(() => {
            animator.SetLayerWeight(1, 1f);
            item.gameObject.transform.SetParent(transform);
            carriedObject = item.gameObject;
            item.PickedUp();
        }));
    }

    public Item GetCarriedItem() {
        if (carriedObject) {
            return carriedObject.GetComponent<Item>();
        }
        return null;
    }

    public void SetCarriedItem(Item item) {
        if (!item) {
            carriedObject = null;
        }
        else if (!carriedObject) {
            PickupItem(item);
        }
    }
}
