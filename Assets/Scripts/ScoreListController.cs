using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreListController : MonoBehaviour
{
    public GameObject ContentPanel;
    public GameObject ListItemPrefab;
    public GameObject WarningWindow;
    List<Score> scores;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScoreList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayScoreList()
    {
        scores = Score.LoadScores();
        scores = Score.LoadScores();

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject newItem = Instantiate(ListItemPrefab) as GameObject;
            ScoreListItem itemController = newItem.GetComponent<ScoreListItem>();
            itemController.Rank.text = (i + 1).ToString();
            itemController.Score.text = scores[i].score.ToString();
            itemController.Time.text = scores[i].elapsedTime + "s";
            itemController.Size.text = (scores[i].size.x + " x " + scores[i].size.z).ToString();
            itemController.Date.text = scores[i].date.ToString();
            newItem.transform.parent = ContentPanel.transform;
            newItem.transform.localScale = Vector3.one;
        }
    }

    public void CallWarningWindow()
    {
        WarningWindow.SetActive(true);
    }
    public void CloseWarningWindow()
    {
        WarningWindow.SetActive(false);
    }

    public void ResetScores()
    {
        WarningWindow.SetActive(false);
        Score.ResetScore();
        Debug.Log("Scores have been reset!");
        foreach(Transform child in ContentPanel.transform)
        {
            Destroy(child.gameObject);
        }
        DisplayScoreList();
    }
}
