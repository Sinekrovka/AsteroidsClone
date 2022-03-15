using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject endGameUI;

    public static GameState Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        gameUI.SetActive(true);
        endGameUI.SetActive(false);
    }

    private void OnDisable()
    {
        gameUI.SetActive(false);
        restart.SetActive(true);
        endGameUI.SetActive(true);
    }
}
