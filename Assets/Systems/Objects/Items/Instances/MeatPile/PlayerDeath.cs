using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Player player;
    public GameObject playerView;

    public SoundData deathStinger;
    public SoundData deathMusic;

    public void Die()
    {
        InputHintManadger.Instance.ShowPanel(false);
        PlayerInventoryUI.Instance.ShowInventoryPanel(false);
        NoteManager.Instance.HideNote();

        SoundManager.PlaySound(deathStinger, player.soundPlayer);
        player.meatExplosion.Explode();

        SoundManager.Instance.GetComponent<LevelMusic>().StopMusic();
        SoundManager.PlaySound(deathMusic, player.soundPlayer);

        playerView.gameObject.SetActive(false);
        var perceivableObject = player.GetComponent<PerceivableObject>();
        perceivableObject.visibleObject.AIIgnore = true;

        player.playerMonsterInView.enabled = false;
        player.playerInventory.DropItem();
        player.GetComponent<FootstepAudio>().enabled = false;
        player.GetComponent<Collider>().enabled = false;

    }
}