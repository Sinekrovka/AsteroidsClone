
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerGame : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Transform healtLabel;
    [SerializeField] private Image energyField;
    [SerializeField] private TextMeshProUGUI coordinats;

    private int scoreGame;
    private int healtCount;
    
    public static UIControllerGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        scoreGame = 0;
        healtCount = healtLabel.childCount;
        for (int i = 0; i < healtCount; ++i)
        {
            healtLabel.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        energyField.fillAmount = 1;
        textScore.text = "Score: " + scoreGame;

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
        else if(healtCount<=0)
        {
            EndGame();
        }
    }

    public void NoneHealt()
    {
        for (int i = 0; i < healtLabel.childCount; ++i)
        {
            healtLabel.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
    }

    public void EndGame()
    {
        EndState.Instance.SaveScoreData(scoreGame);
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

    public void GetCoordinats(Transform player)
    {
        string x = player.position.x.ToString();
        string y = player.position.y.ToString();
        string angle = player.eulerAngles.z.ToString();

        if (x.Length > 4)
        {
            x = x.Substring(0, 4);
        }

        if (y.Length > 4)
        {
            y = y.Substring(0, 4);
        }

        if (angle.Length > 4)
        {
            angle = angle.Substring(0, 4);
        }
        
        coordinats.text = $"X: {x}, Y: {y} \n {angle}°";
    }
}