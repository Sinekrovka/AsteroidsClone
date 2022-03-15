using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private Transform healtLabel;
    [SerializeField] private Image energyField;
    
    public static UIController Instance;
    
    private int scoreGame;
    private int healtCount;
    private void Awake()
    {
        Instance = this;
        scoreGame = 0;
        healtCount = healtLabel.childCount;
    }

    public void GetShoot()
    {
        if (energyField.fillAmount > 0)
        {
            energyField.fillAmount -= 0.02f;
        }
    }

    public void GetHealt()
    {
        healtCount -= 1;
        if (healtCount >= 0)
        {
            healtLabel.GetChild(healtCount).GetComponent<Image>().color = Color.gray;
        }
        else
        {
            /*конец игры*/
        }
    }

    public void AddScore(int score)
    {
        scoreGame += score;
        textScore.text = "Score: " + scoreGame;
    }

    private void Update()
    {
        if (energyField.fillAmount < 1)
        {
            energyField.fillAmount += 0.01f;
        }
    }
}
