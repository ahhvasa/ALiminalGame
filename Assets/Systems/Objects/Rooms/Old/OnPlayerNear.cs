using UnityEngine;

public abstract class OnPlayerNear : MonoBehaviour
{
    public Player player;
    public float maxDistance;

    public void Start()
    {
        SceneSearchService.TryFindNearest<Player>(transform.position, 99999, out player);
    }

    public void FixedUpdate()
    {
        Activate(Vector3.Distance(transform.position, player.transform.position) < maxDistance);
    }

    public abstract void Activate(bool playerClose);
}
