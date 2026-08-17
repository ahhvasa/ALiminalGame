using UnityEngine;

public class LevelPortal_ActivationOnPlayerReachHeight : MonoBehaviour
{
    public Transform topPoint;
    public Player player;
    public LevelPortal levelPortal;

    public void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void FixedUpdate()
    {
        Debug.Log($"LEVEL PORTAL HEIGHT -> players = {player.transform.position.y}, top = {topPoint.transform.position.y} ({player.transform.position.y > topPoint.transform.position.y})");

        if(player.transform.position.y > topPoint.transform.position.y)
        {
            Debug.Log("LEVEL PORTAL HEIGHT -> TRY LOADING LEVEL");
            levelPortal.LoadLevel();
        }
    }
}
