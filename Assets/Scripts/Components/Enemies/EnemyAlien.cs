using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAlien : MonoBehaviour, IEnemy
{
    [SerializeField] private int healt;
    [SerializeField] private Transform healtContainer;
    //[SerializeField] private float speed;
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
        alien.position = Vector3.MoveTowards(alien.position, player.position, distance);
    }

    private IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShootQuqe());
        
    }

    private IEnumerator ShootQuqe()
    {
        GameObject[] shoots = new GameObject[3];
        for (int i = 0; i < shoots.Length; i++)
        {
            shoots[i] = Instantiate(alienShoot, alien.position, Quaternion.identity, alien);
            shoots[i].transform.LookAt(player.transform.position, Vector3.forward);
            shoots[i].transform.rotation = Quaternion.Euler(0,0,player.eulerAngles.z*-1);
            shoots[i].SetActive(false);
        }
        shoots[0].SetActive(true);
        Destroy(shoots[0], 3f);
        yield return new WaitForSeconds(0.2f);
        shoots[1].SetActive(true);
        Destroy(shoots[0], 3f);
        yield return new WaitForSeconds(0.2f);
        Destroy(shoots[0], 3f);
        shoots[2].SetActive(true);
        StartCoroutine(WaitShoot());
    }

}
