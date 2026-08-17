using UnityEngine;

public class WinZone : MonoBehaviour
{
    public SoundData winSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Win");
            SoundManager.PlaySound(winSound, FindObjectOfType<Player>().soundPlayer);
            WinScreen.Instance.Activate();
        }
    }


}
