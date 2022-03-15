using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfigFile;
    [SerializeField] private int sizePool;
    private GameObject[] pool;
    private int randomSum;
    private int[] randomList;

    private void Awake()
    {
        pool = new GameObject[sizePool];
        randomSum = 0;
        randomList = new int[enemyConfigFile.enemyPapamsList.Length];
        for (int j = 0; j < enemyConfigFile.enemyPapamsList.Length; ++j)
        {
            randomSum += enemyConfigFile.enemyPapamsList[j].rangeSpawn;
            randomList[j] = randomSum;
        }
        SpawnObjectInPool();
    }

    private void SpawnObjectInPool()
    {
        for (int i = 0; i < sizePool; ++i)
        {
            int randomRange = Random.Range(1, randomSum);
            for (int j = 0; j < randomList.Length; ++j)
            {
                if (randomRange < randomList[j])
                {
                    pool[i] = Instantiate(enemyConfigFile.enemyPapamsList[j].prefabs);
                    pool[i].SetActive(false);
                    break;
                }
            }
        }
    }
}
