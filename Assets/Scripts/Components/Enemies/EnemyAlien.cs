using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        if (healt > 0)
        {
            healtContainer.GetChild(healt).gameObject.SetActive(false);
            alien.DOShakePosition(0.1f, 0.1f);
        }
        else
        {
            healtContainer.GetChild(0).gameObject.SetActive(false);
            GameObject destFX = Instantiate(fxDestroy, alien.position, Quaternion.identity);
            Destroy(destFX, 1.5f);
            UIControllerGame.Instance.AddScore(15);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(alien.position, player.position) >= distance)
        {
            alien.position = Vector3.MoveTowards(alien.position, player.position, Time.deltaTime * speed);
        }
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
        GameObject shoots;
        Vector3 pos = player.transform.position;
        pos.z = 0;
        shoots = Instantiate(alienShoot, alien.position, Quaternion.identity, alien);
        shoots.transform.LookAt(pos, Vector3.forward);
        if (pos.x > alien.position.x)
        {
            shoots.transform.rotation = Quaternion.Euler(0,0,shoots.transform.eulerAngles.x*-1 -90);
        }
        else
        {
            shoots.transform.rotation = Quaternion.Euler(0,0,shoots.transform.eulerAngles.x +90);
        }
        
        Destroy(shoots, 3f);
    }

    public void FatalDamage()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            other.GetComponent<IEnemy>().FatalDamage();
        }
    }
}
