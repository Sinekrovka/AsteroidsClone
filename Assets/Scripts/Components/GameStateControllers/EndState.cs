using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndState : MonoBehaviour
{
    [SerializeField] private Color currentResultColor;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject textScoreField;
    [SerializeField] private Transform scoreContent;
    [SerializeField] private Transform _player;
    public static EndState Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveScoreData(int currentScore)
    {
        endGame.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        for (int i = 0; i < enemies.Length; ++i)
        {
            Destroy(enemies[i]);
        }
        Dictionary<int, string> dictScore = new Dictionary<int, string>();
        XElement root = new XElement("root");
        if (File.Exists(Application.dataPath + "/SavesScore.xml"))
        {
            root = XDocument.Parse(File.ReadAllText(Application.dataPath+"/SavesScore.xml")).Element("root");
            foreach (var elem in root.Elements())
            {
                dictScore.Add(int.Parse(elem.Attribute("Score").Value), elem.Attribute("Time").Value);
            }
        }
        dictScore.Add(currentScore, System.DateTime.Now.ToString());
        
        var sortedDict = new SortedDictionary<int, string>(dictScore);
        root = new XElement("root");
        int index = 0;

        foreach (var elem in sortedDict)
        {
            XElement scoreStroke = new XElement("stroke");
            XAttribute score = new XAttribute("Score", elem.Key);
            XAttribute time = new XAttribute("Time", elem.Value);
            scoreStroke.Add(score);
            scoreStroke.Add(time);
            root.Add(scoreStroke);

            index += 1;
            GameObject scoreItem = Instantiate(textScoreField, scoreContent);
            if (elem.Key.Equals(currentScore))
            {
                scoreItem.GetComponent<Image>().color = currentResultColor;
            }

            scoreItem.GetComponentInChildren<TextMeshProUGUI>().text = $"{index}.   Score: {elem.Key}   ------------------------    {elem.Value}";
        }
        
        XDocument saveDoc = new XDocument(root);
        File.WriteAllText(Application.dataPath+"/SavesScore.xml", saveDoc.ToString());
    }

    public void RestartGame()
    {
        _player.position = Vector3.zero;
        endGame.SetActive(true);
        List<GameObject> tableField = new List<GameObject>();
        int count = scoreContent.childCount;
        for (int i = 0; i < count; i++)
        {
            tableField.Add(scoreContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < tableField.Count; ++i)
        {
            Destroy(tableField[i]);
        }
    }
}
