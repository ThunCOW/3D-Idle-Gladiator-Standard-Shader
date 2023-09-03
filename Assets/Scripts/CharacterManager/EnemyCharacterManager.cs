using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterManager : CharacterManager
{
    public void RemoveEnemy()
    {
        BattleStatPanel.Instance.RemoveEvents(this);
        BattleStatus.Instance.RemoveEvents(this);

        StartCoroutine(DestroyInTime());
    }

    protected override void Death()
    {
        BattleManager.Characters[Gladiator.Player].Attributes.LVL.CurrentEXP++;
        //PlayerCharacterManager.PlayerGold += Random.Range();

        BattleStatPanel.Instance.RemoveEvents(this);
        BattleStatus.Instance.RemoveEvents(this);

        StartCoroutine(DestroyInTime(4.5f));
    }

    IEnumerator DestroyInTime(float time = 0)
    {
        yield return new WaitForSeconds(time);

        BattleManager.Characters[GladiatorType] = null;

        Destroy(gameObject);
    }
}
