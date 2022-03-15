using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfigFile;
    [SerializeField] private int sizePool;

    private void Awake()
    {
        SpawnObjectInPool();
    }

    private void SpawnObjectInPool()
    {
        
    }
}
