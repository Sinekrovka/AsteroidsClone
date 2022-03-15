using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeteor : MonoBehaviour, IEnemy
{
    [SerializeField] private float speed;
    [SerializeField] private float angularSpeed;
    [SerializeField] private GameObject fxDestroy;
    [SerializeField] private Transform containerChilds;
    
    private Vector3 direction;
    private Transform meteor;
    
    private void Awake()
    {
        direction = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0);
        meteor = transform;
    }

    private void Update()
    {
        meteor.position += direction * speed * Time.deltaTime;
        Vector3 rotation = meteor.eulerAngles + Vector3.forward * angularSpeed * Time.deltaTime;
        meteor.rotation = Quaternion.Euler(rotation);
    }

    public void Damage()
    {
        if (containerChilds != null)
        {
            int childs = containerChilds.childCount;
            for (int i = 0; i < childs; ++i)
            {
                Transform child = containerChilds.GetChild(i);
                child.SetParent(null);
                child.gameObject.SetActive(true);
            }
        }

        GameObject particle = Instantiate(fxDestroy, meteor.position, Quaternion.identity);
        Destroy(particle, 1.5f);
        Destroy(gameObject);
    }
}
