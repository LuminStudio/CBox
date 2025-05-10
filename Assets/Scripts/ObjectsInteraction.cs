using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteraction : MonoBehaviour
{
    public Animator playerAnimation;
    public FixedJoint interactionJoint;
    public int maxInteractionDistance = 5;
    public GameObject interactionObject;
    public bool isInteraction;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * maxInteractionDistance, Color.red);

            if (Physics.Raycast(ray, out hit, maxInteractionDistance))
            {
                if (hit.collider.gameObject.GetComponent<Interactable>() && !isInteraction && interactionObject == null)
                {
                    interactionObject = hit.collider.gameObject;
                    playerAnimation.SetBool("isInteractionEnabled", true);
                    interactionJoint.connectedBody = interactionObject.GetComponent<Rigidbody>();
                    isInteraction = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isInteraction == true && interactionObject != null)
        {
            interactionObject = null;
            isInteraction = false;
            playerAnimation.SetBool("isInteractionEnabled", false);
            interactionJoint.connectedBody = null;
        }
    }
}
