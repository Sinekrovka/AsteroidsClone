using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAlien : MonoBehaviour, IEnemy
{
    private int healt;
    [SerializeField] private Transform healtContainer;
    [SerializeField] private float speed;
    [SerializeField] private GameObject fxDestroy;
    [SerializeField] private GameObject alienShoot;
    [SerializeField] private float distance;
    private Transform alien;
    private Transform player;

    private void Awake()
    {
        alien = transform;
        player = GameObject.Find("Player").transform;
        StartCoroutine(WaitShoot());
        healt = healtContainer.childCount;
    }

    public void Damage()
    {
        healt -= 1;
        healtContainer.GetChild(healt).gameObject.SetActive(false);
        if (healt <= 0)
        {
            GameObject destFX = Instantiate(fxDestroy, alien.position, Quaternion.identity);
            Destroy(destFX, 1.5f);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        alien.position = Vector3.MoveTowards(alien.position, player.position, distance) * Time.deltaTime * speed;
    }

    private IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShootQuqe());
        
    }

    private IEnumerator ShootQuqe()
    {
        for (int i = 0; i < 3; ++i)
        {
            GenerateShoot();
            yield return new WaitForSeconds(0.5f);
        }
        
        StartCoroutine(WaitShoot());
    }

    private void GenerateShoot()
    {
        GameObject shoots = new GameObject();
        Vector3 pos = player.transform.position;
        pos.z = 0;
        shoots = Instantiate(alienShoot, alien.position, Quaternion.identity, alien);
        shoots.transform.LookAt(pos);
        shoots.transform.rotation = Quaternion.Euler(0,0,shoots.transform.eulerAngles.z*-1);
        Destroy(shoots, 3f);
    }

}
