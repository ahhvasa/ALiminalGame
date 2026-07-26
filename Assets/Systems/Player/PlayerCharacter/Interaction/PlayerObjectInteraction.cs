using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectInteraction : MonoBehaviour
{
    public Player player;
    float maxActivationDistance = 5;


    public void Update()
    {
        if (InputProvider.Interact())
        {
            Interact();
        }
    }

    public void Interact()
    {

        if (SceneSearchService.TryFindNearest<InteractableObjectFlag>(player.transform.position, maxActivationDistance, out InteractableObjectFlag target))
        {
            if (Vector3.Distance(player.transform.position, target.transform.position) > target.objectActivationDistance) { return; }

            if (target.TryGetComponent<IPlayerInteractableObject>(out var interactable))
            {
                interactable.Interact(player);
            }
        }
    }
}
