using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : MonoBehaviour
{
    [SerializeField] private GameObject GameState;
    [SerializeField] private GameObject startUI;
    public void StartGame()
    {
        GameState.SetActive(true);
        startUI.SetActive(false);
    }
}
