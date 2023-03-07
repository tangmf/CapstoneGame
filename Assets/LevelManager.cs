using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform content;
    public List<SceneItem> levels;
    public GameObject levelContainer;
    // Start is called before the first frame update
    void Start()
    {
        Clear();
        foreach(SceneItem level in levels)
        {
            GameObject newObj = Instantiate(levelContainer, new Vector3(0,0,0), Quaternion.identity);
            newObj.transform.parent = content;
            newObj.GetComponent<ScoreDisplay>().SetUp(level);
            RectTransform rect = newObj.transform as RectTransform;

            rect.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        foreach (Transform x in content)
        {
            Destroy(x.gameObject);
        }
    }
}
