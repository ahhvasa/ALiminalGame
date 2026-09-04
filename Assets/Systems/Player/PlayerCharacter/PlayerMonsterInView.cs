using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonsterInView : MonoBehaviour
{
    public Player player;

    public SoundData monsterDetected;
    public SoundData monsterNear;
    public SoundData justEscapedMonster;

    [SerializeField] private float justEscapedDuration = 3f;

    private Sound activeMonsterDetected;
    private Sound activeMonsterNear;
    private Sound activeJustEscapedMonster;

    [SerializeField] private List<Creature> monsters = new();

    private Coroutine justEscapedCoroutine;

    public void AddMonster(Creature monster)
    {
        if (monsters.Contains(monster))
            return;

        bool wasEmpty = monsters.Count == 0;

        monsters.Add(monster);

        if (wasEmpty)
            OnFirstMonsterDetected();
    }

    public void RemoveMonster(Creature monster)
    {
        if (!monsters.Remove(monster))
            return;

        if (monsters.Count == 0)
            OnLastMonsterLost();
    }

    public void OnDisable()
    {
        activeMonsterDetected?.DestroySmoothly();
        activeMonsterNear?.DestroySmoothly();
        activeJustEscapedMonster?.DestroySmoothly();
    }

    private void OnFirstMonsterDetected()
    {
        if (justEscapedCoroutine != null)
        {
            StopCoroutine(justEscapedCoroutine);
            justEscapedCoroutine = null;
        }

        activeJustEscapedMonster?.DestroySmoothly();

        activeMonsterDetected?.DestroySmoothly();
        activeMonsterDetected = SoundManager.PlaySound(monsterDetected, player.soundPlayer);

        activeMonsterNear?.DestroySmoothly();
        activeMonsterNear = SoundManager.PlaySound(monsterNear, player.soundPlayer);
    }

    private void OnLastMonsterLost()
    {
        activeMonsterNear?.DestroySmoothly();

        activeJustEscapedMonster?.DestroySmoothly();
        activeJustEscapedMonster = SoundManager.PlaySound(justEscapedMonster, player.soundPlayer);

        justEscapedCoroutine = StartCoroutine(StopJustEscapedAfterDelay());
    }

    private IEnumerator StopJustEscapedAfterDelay()
    {
        yield return new WaitForSeconds(justEscapedDuration);

        activeJustEscapedMonster?.DestroySmoothly();
        justEscapedCoroutine = null;
    }
}