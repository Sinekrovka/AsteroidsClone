using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Config", menuName = "Config Files/Enemy Config", order = 51)]
public class EnemyConfig : ScriptableObject
{
    [Serializable]
    public struct enemyParams
    {
        public GameObject prefabs;
        [Range(1,100)]
        public int rangeSpawn;
    }

    [SerializeField] public enemyParams[] enemyPapamsList;
}
