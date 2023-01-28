using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreBoardManager : MonoBehaviour
{
    public GameObject scoreContainer;
    public Transform content;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowScores(string level, ScoreDataList datas, string scene)
    {
        levelText.text = level;
        ClearScores();
        List<ScoreData> newList = new List<ScoreData>();

        foreach (ScoreData data in datas.scoreDatas)
        {
            if (data.sceneName == scene)
            {
                newList.Add(data);
            }
        }

        if (newList.Count > 0)
        {
            newList = newList.OrderBy(x => x.score).ToList();
            newList.Reverse();
        }
        int index = 1;
        foreach (ScoreData data in newList)
        {
            
            GameObject newObj = Instantiate(scoreContainer);
            newObj.transform.parent = content;
            newObj.GetComponent<ScoreContainer>().Set(data.grade, data.score, index);
            index++;
        }
    }

    public void ClearScores()
    {
        foreach (Transform x in content)
        {
            Destroy(x.gameObject);
        }
    }

}
