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
}
