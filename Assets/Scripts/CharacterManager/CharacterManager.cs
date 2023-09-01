using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterStatus Status;
    [HideInInspector] public UIStatus StatusUI;
    [HideInInspector] public CharacterAttributes Attributes;
    [HideInInspector] public CharacterActionManager CharacterActionManager;
    [HideInInspector] public EquipmentManager EquipmentManager;
    [HideInInspector] public RagdollManager Ragdoll;

    [HideInInspector] public AudioSource AudioSource;
    [HideInInspector] public Animator Animator;

    private void Awake()
    {
        Status = GetComponent<CharacterStatus>();
        Attributes = GetComponent<CharacterAttributes>();
        CharacterActionManager = GetComponent<CharacterActionManager>();
        EquipmentManager = GetComponent<EquipmentManager>();
        Ragdoll = GetComponent<RagdollManager>();

        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponentInChildren<Animator>();

        if (CompareTag("Player"))
            BattleManager.Characters[Gladiator.Player] = this;
        else
            BattleManager.Characters[Gladiator.Enemy] = this;

        StatusUI = CompareTag("Player") ? UIStatusManager.Instance.PlayerStatus : UIStatusManager.Instance.EnemyStatus;
        StatusUI.characterManager = this;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
