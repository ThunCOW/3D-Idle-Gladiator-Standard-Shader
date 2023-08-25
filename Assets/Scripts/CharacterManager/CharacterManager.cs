using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterManager Target;

    [HideInInspector] public CharacterStatus Status;
    [HideInInspector] public UIStatus StatusUI;
    [HideInInspector] public CharacterAttributes Attributes;
    [HideInInspector] public CharacterActionManager CharacterActionManager;
    [HideInInspector] public EquipmentManager EquipmentManager;

    [HideInInspector] public AudioSource AudioSource;
    [HideInInspector] public Animator Animator;

    private void Awake()
    {
        Status = GetComponent<CharacterStatus>();
        Attributes = GetComponent<CharacterAttributes>();
        CharacterActionManager = GetComponent<CharacterActionManager>();
        EquipmentManager = GetComponent<EquipmentManager>();

        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponentInChildren<Animator>();

        if (CompareTag("Player"))
            BattleManager.Characters[Gladiator.Player] = this;
        else
            BattleManager.Characters[Gladiator.Enemy] = this;

        StatusUI = CompareTag("Player") ? UIManager.Instance.PlayerStatus : UIManager.Instance.EnemyStatus;
    }

    private void Start()
    {
        Target = CompareTag("Player") ? BattleManager.Characters[Gladiator.Enemy] : BattleManager.Characters[Gladiator.Player];
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
