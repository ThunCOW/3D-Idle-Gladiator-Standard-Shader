using System.Collections;
using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    public delegate void OnGoldChange(int ChangeAmount);
    public static OnGoldChange GoldChangeEvent;
    
    private static int _playerGold;
    public static int PlayerGold
    {
        get { return _playerGold; }
        set
        {
            _playerGold = value;
            GoldChangeEvent(value);
        }
    }
    
    public void RefreshCharacter()
    {
        Ragdoll.RagdollOff();

        Status.Health.CurrentValue = Status.Health.GetValue();

        Status.Armour.CurrentValue = Status.Armour.GetValue();
    }

    protected override void Death()
    {
        StartCoroutine(RefreshInTime(4.5f));
    }

    IEnumerator RefreshInTime(float time = 0)
    {
        yield return new WaitForSeconds(time);

        transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        BattleGladiatorManager.Instance.ResetBattle();

        yield return new WaitForFixedUpdate();

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
