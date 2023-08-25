using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIStatus PlayerStatus;
    public UIStatus EnemyStatus;

    private void Awake()
    {
        Instance = this;
    }
}
