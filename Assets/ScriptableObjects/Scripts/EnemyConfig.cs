using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Config", menuName = "Config Files/Enemy Config", order = 51)]
public class EnemyConfig : ScriptableObject
{
    [Serializable]
    public struct enemyParams
    {
        public int healt;
        public GameObject prefabs;
        public float speedMove;
        public float speedAngle;
        [Range(1,100)]
        public int rangeSpawn;
    }

    [SerializeField] public enemyParams[] enemyPapamsList;
}
