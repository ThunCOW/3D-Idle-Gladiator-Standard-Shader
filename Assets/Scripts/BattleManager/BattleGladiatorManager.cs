using CharacterFeature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGladiatorManager : MonoBehaviour
{
    public static BattleGladiatorManager Instance;

    public GameObject EnemyPrefab;

    [Space]
    public RandomEquipmentByLevelScriptableObject RandomEnemyEquipmentSO;

    [Space]
    public CharacterFeaturesScriptableObjects CharacterFeaturesScriptableObjects;

    [Header("********* Temp BS ********")]
    public ItemScriptableObject MyrdionShortShoes;

    private void Awake()
    {
        CharacterFeaturesScriptableObjects.RandomizeCharacter(BattleManager.Characters[Gladiator.Player]);
    }
    private void Start()
    {
        Instance = this;

        RandomEnemyEquipmentSO.Init();

        StartCoroutine(SpawnEnemy(0));

    }

    public void ResetBattle()
    {
        StopAllCoroutines();

        (BattleManager.Characters[Gladiator.Enemy] as EnemyCharacterManager).RemoveEnemy();

        StartCoroutine(SpawnEnemy(0));
    }

    IEnumerator SpawnEnemy(float delay = .5f)
    {
        yield return new WaitUntil(() => BattleManager.Characters[Gladiator.Enemy] == null);

        yield return new WaitForSeconds(delay);

        (BattleManager.Characters[Gladiator.Player] as PlayerCharacterManager).RefreshCharacter();

        GameObject enemyTemp = Instantiate(EnemyPrefab);

        yield return new WaitForEndOfFrame();

        CharacterManager enemyCharacterManager = enemyTemp.GetComponent<CharacterManager>();

        CharacterFeaturesScriptableObjects.RandomizeCharacter(enemyCharacterManager);

        RandomizeItems(enemyCharacterManager);

        StartCoroutine(SpawnEnemy());

        BattleManager.Instance.StartBattle();
    }

    private void RandomizeItems(CharacterManager enemyCharacterManager)
    {
        int playerLVL = BattleManager.Characters[Gladiator.Player].Attributes.LVL.LVL;

        List<ItemScriptableObject> itemList = RandomEnemyEquipmentSO.GetEquipment(playerLVL);

        foreach (ItemScriptableObject item in itemList)
        {
            enemyCharacterManager.EquipmentManager.EquipItem(item.Clone() as ItemScriptableObject);
        }
    }
}
