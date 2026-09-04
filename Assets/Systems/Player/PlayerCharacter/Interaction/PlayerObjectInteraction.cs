using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectInteraction : MonoBehaviour
{
    public Player player;
    public float maxActivationDistance = 5;

    public GameObject interactableObjectLabel;

    public InteractableObjectFlag currentObjectInFront;


    public void Update()
    {
        if (InputProvider.Interact())
        {
            Interact();
        }
        HiglightObject();
    }

    public void FixedUpdate()
    {
        GetObjectInfront(out currentObjectInFront);
    }

    public void HiglightObject()
    {
        if (currentObjectInFront != null)
        {
            interactableObjectLabel.transform.position = currentObjectInFront.transform.position + Vector3.up * currentObjectInFront.playerActivationLabelHeight;
            interactableObjectLabel.SetActive(true);
        }
        else
        {
            interactableObjectLabel.SetActive(false);
        }
    }

    public void Interact()
    {
        if (GetInteractableObject(out var interactableObject))
        {
            interactableObject.Interact(player);
        }
    }

    public bool GetObjectInfront(out InteractableObjectFlag target)
    {
        target = null;

        List<InteractableObjectFlag> objects = SceneSearchService.FindAllObjectsInCircleZone< InteractableObjectFlag >(player.transform.position, maxActivationDistance);
        

        Vector3 lookDirection = player.transform.forward;
        float bestDot = -1f;
        float bestDistanceSqr = float.MaxValue;

        foreach (var obj in objects)
        {
            if (obj.active == false) { continue; }
            if (obj.visibleObject != null) 
            { 
                if (obj.visibleObject.CurrentAlpha <= 0.5f) { continue; }
            }


            Vector3 directionToObject = obj.transform.position - player.transform.position;

            if (directionToObject.magnitude > obj.objectActivationDistance) { continue; }

            float distanceSqr = directionToObject.sqrMagnitude;

            directionToObject.Normalize();


            float dot = Vector3.Dot(lookDirection, directionToObject);

            if (dot > bestDot || (Mathf.Approximately(dot, bestDot) && distanceSqr < bestDistanceSqr))
            {
                bestDot = dot;
                bestDistanceSqr = distanceSqr;
                target = obj;
            }
        }

        if (target != null) { return true; }

        return false;
    }


    public bool GetInteractableObject(out IPlayerInteractableObject interactableObject)
    {
        interactableObject = null;
        if (currentObjectInFront != null)
        {
            if (currentObjectInFront.TryGetComponent<IPlayerInteractableObject>(out interactableObject))
            {
                return true;
            }
        }
        return false;
    }
}
