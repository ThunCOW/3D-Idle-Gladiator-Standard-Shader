using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGladiatorSpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public RandomEquipmentByLevelScriptableObject RandomEnemyEquipmentSO;

    private void Start()
    {
        RandomEnemyEquipmentSO.Init();

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(() => BattleManager.Characters[Gladiator.Enemy] == null);

        yield return new WaitForSeconds(1.5f);

        (BattleManager.Characters[Gladiator.Player] as PlayerCharacterManager).RefreshCharacter();

        int playerLVL = BattleManager.Characters[Gladiator.Player].Attributes.LVL.LVL;

        List<ItemScriptableObject> itemList = RandomEnemyEquipmentSO.GetEquipment(playerLVL);

        GameObject enemyTemp = Instantiate(EnemyPrefab);

        CharacterManager enemyCharacterManager = enemyTemp.GetComponent<CharacterManager>();

        yield return new WaitForEndOfFrame();

        foreach (ItemScriptableObject item in itemList)
        {
            enemyCharacterManager.EquipmentManager.EquipItem(item.Clone() as ItemScriptableObject);
        }

        StartCoroutine(SpawnEnemy());
    }
}
