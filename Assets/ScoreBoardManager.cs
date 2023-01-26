using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        foreach (ScoreData data in datas.scoreDatas)
        {
            if (data.sceneName == scene)
            {
                GameObject newObj = Instantiate(scoreContainer);
                newObj.transform.parent = content;
                newObj.GetComponent<ScoreContainer>().Set(data.grade, data.score, 1);
            }
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
