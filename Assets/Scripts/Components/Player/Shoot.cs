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
    }

    private void Update()
    {
        thisShoot.position += thisShoot.up * Time.deltaTime * speed;
    }
}
