using CharacterFeature;
using EquipItemEditor;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterStatus Status;
    [HideInInspector] public UIStatus StatusUI;
    [HideInInspector] public CharacterAttributes Attributes;
    [HideInInspector] public CharacterActionManager CharacterActionManager;
    [HideInInspector] public EquipmentManager EquipmentManager;
    [HideInInspector] public RagdollManager Ragdoll;
    [HideInInspector] public BodyParts BodyParts;
    [HideInInspector] public CharacterFeatureManager CharacterFeatureManager;

    [HideInInspector] public AudioSource AudioSource;
    [HideInInspector] public Animator Animator;

    [HideInInspector] public Gladiator GladiatorType;

    private void Awake()
    {
        Status = GetComponent<CharacterStatus>();
        Attributes = GetComponent<CharacterAttributes>();
        CharacterActionManager = GetComponent<CharacterActionManager>();
        EquipmentManager = GetComponent<EquipmentManager>();
        Ragdoll = GetComponent<RagdollManager>();
        BodyParts = GetComponentInChildren<BodyParts>();
        CharacterFeatureManager = GetComponent<CharacterFeatureManager>();

        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponentInChildren<Animator>();

        if (CompareTag("Player"))
        {
            GladiatorType = Gladiator.Player;
            BattleManager.Characters[Gladiator.Player] = this;
        }
        else
        {
            GladiatorType = Gladiator.Enemy;
            BattleManager.Characters[Gladiator.Enemy] = this;
        }

        StatusUI = CompareTag("Player") ? UIStatusManager.Instance.PlayerStatus : UIStatusManager.Instance.EnemyStatus;
        StatusUI.characterManager = this;

        Status.OnDeath += Death;
    }

    protected virtual void Death() {}
}
