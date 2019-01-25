using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {
    Vector3 movement;

    float rotationSpeed = 12f;
    float moveSpeed = 6f;

    GameObject carriedObject = null;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Move(Vector3 moveDirection, bool interaction) {
        ApplyRotationTo(moveDirection);

        // Move the controller
        var movement = moveDirection * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);

        HandleInteraction(interaction);
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
            if (item) {
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
            }
            else {
                // Pick up
                GameObject obj = FindNearestObject(transform.position);
                if (obj) {
                    obj.transform.SetParent(transform);
                    carriedObject = obj;
                }
            }
        }
    }
}
