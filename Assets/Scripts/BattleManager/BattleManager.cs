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
        StartCoroutine(AttackRoutine(Characters[Gladiator.Player]));
        StartCoroutine(AttackRoutine(Characters[Gladiator.Enemy]));
    }

    private IEnumerator AttackRoutine(CharacterManager Character)
    {
        yield return new WaitForSeconds(Random.Range(1, 5));

        float wait = Random.Range(Character.Attributes.Speed.GetValue(), Character.Attributes.Speed.GetValue() * 2);
        while (wait > 0)
        {
            yield return new WaitUntil(() => !isAttacking);     // if one of the characters is attacking, wait

            wait -= Time.deltaTime;
        }

        yield return new WaitUntil(() => !isAttacking);

        CharacterManager Defender = Character.name.Equals(Characters[Gladiator.Player].name) ? Characters[Gladiator.Enemy] : Characters[Gladiator.Player];
        Attack(Character, Defender);
    }

    private void Attack(CharacterManager Attacker, CharacterManager Defender)
    {
        //float HitChance = Mathf.Clamp(Attacker.Attributes.HitBonus + (Attacker.Attributes.Attack.GetValue() - Defender.Attributes.Defense.GetValue()), 10, 90);
        float HitChance = 50;

        // Success
        if (Random.Range(0, 101) < HitChance)
        {
            int HighMidLow = Random.Range(0, 3);    // 0 = high 1 = mid 2 = low

            Attacker.CharacterActionManager.Attack(true, (HitRegion)HighMidLow);

            StartCoroutine(WaitForAnimation(Attacker, Defender));
        }
        // Fail
        else
        {
            int HighMidLow = Random.Range(0, 3);    // 0 = high 1 = mid 2 = low

            HitRegion HitRegion =
            Attacker.CharacterActionManager.Attack(false, (HitRegion)HighMidLow);
            Defender.CharacterActionManager.Block(HitRegion);

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
        // Wait until both character goes back to "Idle" state and ready for action
        yield return new WaitUntil(() => (
        Attacker.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
        Defender.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
        ));

        StartCoroutine(AttackRoutine(Attacker));

        isAttacking = false;
    }
}
