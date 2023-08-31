using UnityEngine;

public class UIStatusManager : MonoBehaviour
{
    public static UIStatusManager Instance;

    public UIStatus PlayerStatus;
    public UIStatus EnemyStatus;

    private void Awake()
    {
        Instance = this;
    }
}
