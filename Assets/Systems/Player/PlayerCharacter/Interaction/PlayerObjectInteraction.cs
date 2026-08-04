using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectInteraction : MonoBehaviour
{
    public Player player;
    float maxActivationDistance = 5;

    public GameObject interactableObjectLabel;

    public void Update()
    {
        if (InputProvider.Interact())
        {
            Interact();
        }
        //HiglightObject();
    }

    public void HiglightObject()
    {
        if (GetObjectInfront(out InteractableObjectFlag target))
        {
            interactableObjectLabel.transform.position = target.transform.position;
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
        if (SceneSearchService.TryFindNearest<InteractableObjectFlag>(player.transform.position, maxActivationDistance, out target))
        {
            if (Vector3.Distance(player.transform.position, target.transform.position) > target.objectActivationDistance) { return false; }

            return true;
        }
        return false;
    }


    public bool GetInteractableObject(out IPlayerInteractableObject interactableObject)
    {
        interactableObject = null;
        if (GetObjectInfront(out InteractableObjectFlag target))
        {
            if (target.TryGetComponent<IPlayerInteractableObject>(out interactableObject))
            {
                return true;
            }
        }
        return false;
    }
}
