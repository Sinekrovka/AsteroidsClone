using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfigFile;
    [SerializeField] private int sizePool;
    private int randomSum;
    private int[] randomList;
    private Camera camera;
    private Vector2[] spawnsPoints;
    

    private void Awake()
    {
        camera = Camera.main;
        float width = camera.pixelWidth;
        float height = camera.pixelHeight;
        Vector2 bottomLeft = camera.ScreenToWorldPoint(new Vector2 (-10, -10));
        Vector2 bottomRight = camera.ScreenToWorldPoint(new Vector2 (width+10, -10));
        Vector2 topLeft = camera.ScreenToWorldPoint(new Vector2 (-10, height+10));
        Vector2 topRight = camera.ScreenToWorldPoint(new Vector2 (width+10, height+10));

        spawnsPoints = new[] {bottomLeft, bottomRight, topLeft, topRight};
        
        randomSum = 0;
        randomList = new int[enemyConfigFile.enemyPapamsList.Length];
        
        for (int j = 0; j < enemyConfigFile.enemyPapamsList.Length; ++j)
        {
            randomSum += enemyConfigFile.enemyPapamsList[j].rangeSpawn;
            randomList[j] = randomSum;
        }
        StartCoroutine(SpawnObjectInPool());
    }

    private IEnumerator SpawnObjectInPool()
    {
        for (int i = 0; i < sizePool; ++i)
        {
            int randomRange = Random.Range(1, randomSum);
            for (int j = 0; j < randomList.Length; ++j)
            {
                if (randomRange < randomList[j])
                {
                    Instantiate(enemyConfigFile.enemyPapamsList[j].prefabs, 
                        spawnsPoints[Random.Range(0, spawnsPoints.Length)], Quaternion.identity, null);
                    break;
                }
            }
        }
        yield return new WaitForSeconds(11f);
        StartCoroutine(SpawnObjectInPool());
        
    }
}
