using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform thisShoot;
    private void OnEnable()
    {
        thisShoot = transform;
        thisShoot.SetParent(null);
    }

    private void Update()
    {
        thisShoot.position += thisShoot.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shoot"))
        {
            other.GetComponent<IEnemy>().Damage();
            UIController.Instance.AddScore(15);
            Destroy(gameObject);
        }
    }
}
