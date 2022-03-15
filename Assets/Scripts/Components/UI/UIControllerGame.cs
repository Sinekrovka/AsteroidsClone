
using UnityEngine;
using UnityEngine.UI;

public class UIControllerGame : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Transform healtLabel;
    [SerializeField] private Image energyField;

    private int scoreGame;
    private int healtCount;
    
    public static UIControllerGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreGame = 0;
        healtCount = healtLabel.childCount;
    }

    public bool GetShoot()
    {
        if (energyField.fillAmount - 0.1f >=0)
        {
            energyField.fillAmount -= 0.1f;
            return true;
        }

        return false;
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
            energyField.fillAmount += 0.001f;
        }
    }
}