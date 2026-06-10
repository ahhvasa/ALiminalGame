using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteraction : MonoBehaviour
{
    public Player player;
    public float maxActivationDistance = 1;
    public void Interact()
    {

        if (SceneSearchService.TryFindNearest<InteractableObjectFlag>(player.transform.position, maxActivationDistance, out InteractableObjectFlag target))
        {
            if (target.TryGetComponent<IPlayerInteractableObject>(out var interactable))
            {
                interactable.Interact(player);
            }
        }
    }
}
