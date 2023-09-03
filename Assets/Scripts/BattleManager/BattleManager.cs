using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gladiator
{
    Player,
    Enemy
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [Header("********* Editor Interaction **********")]
    public bool PauseGameOnAttack;
    public GameObject BloodFXPrefab;

    public static Dictionary<Gladiator, CharacterManager> Characters;

    private bool isAttacking;
    
    private void Awake()
    {
        Instance = this;

        Characters = new Dictionary<Gladiator, CharacterManager>();
        Characters.Add(Gladiator.Player, null);
        Characters.Add(Gladiator.Enemy, null);
    }

    void Start()
    {
        //StartCoroutine(AttackRoutine(Gladiator.Player));
        //StartCoroutine(AttackRoutine(Gladiator.Enemy));

        //StartCoroutine(BattleLogic());
    }

    public void StartBattle()
    {
        StopAllCoroutines();

        StartCoroutine(BattleLogic(1.5f));
    }

    private IEnumerator AttackRoutine(Gladiator character)
    {
        yield return new WaitForSeconds(Random.Range(1, 5));

        float wait = Random.Range(Characters[character].Attributes.Speed.GetValue(), Characters[character].Attributes.Speed.GetValue() * 2);
        while (wait > 0)
        {
            wait -= Time.deltaTime;
        }

        if(isAttacking) yield return new WaitUntil(() => !isAttacking);

        CharacterManager Defender = Characters[character].name.Equals(Characters[Gladiator.Player].name) ? Characters[Gladiator.Enemy] : Characters[Gladiator.Player];
        Attack(Characters[character], Defender);
    }

    private IEnumerator BattleLogic(float delay = 0)
    {
        if (delay == 0)
            yield return new WaitForSeconds(Random.Range(0.25f, 0.85f));
        else
            yield return new WaitForSeconds(delay);

        float playerAttackChance = Random.Range(0, BattleStatus.Instance.Player.AttackSpeed);
        float enemyAttackChance = Random.Range(0, BattleStatus.Instance.Enemy.AttackSpeed);

        Gladiator attacker = playerAttackChance > enemyAttackChance ? Gladiator.Player : Gladiator.Enemy;
        Gladiator defender = attacker == Gladiator.Player ? Gladiator.Enemy : Gladiator.Player;

        Attack(Characters[attacker], Characters[defender]);
    }
    private void Attack(CharacterManager Attacker, CharacterManager Defender)
    {
        //float HitChance = Mathf.Clamp(Attacker.Attributes.HitBonus + (Attacker.Attributes.Attack.GetValue() - Defender.Attributes.Defense.GetValue()), 10, 90);
        BattleStatus.BattleStatusInfo battleStatusInfo = Attacker is PlayerCharacterManager ? BattleStatus.Instance.Player : BattleStatus.Instance.Enemy;

        int HitChance = battleStatusInfo.HitChance;

        // Success
        if (Random.Range(0, 101) < HitChance)
        {
            int HighMidLow = Random.Range(0, 3);    // 0 = high 1 = mid 2 = low

            Attacker.CharacterActionManager.AttackAction(true, (HitRegion)HighMidLow);

            StartCoroutine(WaitForAnimation(Attacker, Defender));
        }
        // Fail
        else
        {
            int HighMidLow = Random.Range(0, 3);    // 0 = high 1 = mid 2 = low

            HitRegion HitRegion =
            Attacker.CharacterActionManager.AttackAction(false, (HitRegion)HighMidLow);
            Defender.CharacterActionManager.BlockAction(HitRegion);

            StartCoroutine(WaitForAnimation(Attacker, Defender));
        }
    }

    IEnumerator WaitForAnimation(CharacterManager Attacker, CharacterManager Defender)
    {
        isAttacking = true;

        yield return new WaitForFixedUpdate();

        // GetCurrentAnimatorClipInfo(0)[0].clip.name       Returns     Armature_Character Sword Idle A 
        // GetCurrentAnimatorStateInfo(0).IsName("Idle")    Returns     True

        // Wait until both character leaves "Idle" state and acts
        yield return new WaitUntil(() => (
        !Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
        !Defender.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
        ));

        if (Defender.Status.Health.CurrentValue < 0)
        {
            StopAllCoroutines();
        }
        //if (Defender.Animator.GetCurrentAnimatorStateInfo(0).IsName())

        // Wait until both character goes back to "Idle" state and ready for action
        yield return new WaitUntil(() => (
        Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
        Defender.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
        ));

        isAttacking = false;

        StartCoroutine(BattleLogic());
        //StartCoroutine(AttackRoutine(Attacker.GladiatorType));
    }
}
