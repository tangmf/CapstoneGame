using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    public GameObject player;
    public GameObject playerContainer;
    public float respawnTime = 2.0f;

    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject popUp;

    public GameObject boss;
    public Transform bossSpawnPos;

    GameObject instantiatedPlayer;

    public bool gameEnded;

    public GameObject gameStartScreen;

    int score = 0;

    float winTime = 0.0f;
    float winHealthPoints = 0.0f;

    public TextMeshProUGUI timer;

    ProfileMaster pm;

    public float difficultyMultiplier = 1.0f;
    public string levelType = "Boss";
    public GameObject upgradePopup;
    public float upgradeCD = 60f;
    public float nextUpgradeTime = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            GameOver("WIN");
        }

        if (Input.GetKeyDown("l"))
        {
            GameOver("LOSE");
        }
        timer.text = ((int)(Time.timeSinceLevelLoad)).ToString();
        if (levelType == "Survival" && !gameEnded)
        {
            if (Time.timeSinceLevelLoad >= nextUpgradeTime)
            {
                ShowUpgradeScreen();
            }
        }





    }

    void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        */
        pm = GameObject.FindWithTag("PM").GetComponent<ProfileMaster>();
        if (pm != null)
        {
            Debug.Log("PM OK");
        }
        else
        {
            Debug.Log("PM ERROR");
        }
        if (GameObject.FindGameObjectWithTag("WorldSpawn") != null)
        {
            transform.position = new Vector3(0, 0, 0);
            Respawn();
            StartCoroutine(GameStart());
        }

    }

    // used by pause menu
    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Destroy(gameObject);
    }

    public void Respawn()
    {
        Transform target = GameObject.FindGameObjectWithTag("WorldSpawn").GetComponent<Transform>();
        lastCheckPointPos = target.position;
        Debug.Log("Spawn point set at: " + target.position.x.ToString() + "," + target.position.y.ToString());
        Instantiate(pm.currChar.playerModel, lastCheckPointPos, Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        timer = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
        player.transform.position = lastCheckPointPos;
        SetPlayerUpgrades(player);
    }

    public void WaitForRespawn()
    {
        StartCoroutine(WaitToRespawn());

    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        player.GetComponent<PlayerMovement>().animator.Rebind();
        player.GetComponent<PlayerMovement>().animator.Update(0f);
        player.GetComponent<PlayerMovement>().enabled = !player.GetComponent<PlayerMovement>().enabled;
        player.GetComponent<HealthManager>().Heal((int)player.GetComponent<HealthManager>().healthBar.maxValue);
        Respawn();
    }

    IEnumerator GameStart()
    {
        GameObject newGameStartScreen = Instantiate(gameStartScreen);
        yield return new WaitForSeconds(3.0f);
        if (boss != null)
        {
            Instantiate(boss, bossSpawnPos.position, Quaternion.identity);
        }


    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    //After adding this method to the delegate, this method will be called every time
    //that a new scene is loaded. You can then compare the scene loaded to your desired
    //scenes and do actions according to the scene loaded.
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene != null) //use your desired check here to compare your scene
            if (GameObject.FindGameObjectWithTag("WorldSpawn") != null)
                player.transform.position = GameObject.FindGameObjectWithTag("WorldSpawn").GetComponent<Transform>().position;
    }


    public void GameOver(string type)
    {
        if (!gameEnded)
        {
            gameEnded = true;
            winHealthPoints = player.GetComponent<HealthManager>().healthPoints;
            winTime = Time.timeSinceLevelLoad;
            CalculateScore();
            StartCoroutine(WrapUp(type));
        }


    }

    public void CalculateScore()
    {
        if(levelType == "Boss")
        {
            score = (int)(winHealthPoints * 10 - winTime);
        }
        else if(levelType == "Survival")
        {
            score = (int)(winTime);
        }
        
    }

    IEnumerator WrapUp(string type)
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1.0f);
        GameObject newPopUp = Instantiate(popUp);
        yield return new WaitForSeconds(1.0f);
        Destroy(newPopUp);


        Time.timeScale = 1.0f;
        GameObject newGameOverScreen = Instantiate(gameOverScreen);
        char grade = 'F';
        if(levelType == "Boss")
        {
            if (type == "WIN")
            {
                if (score >= 1900)
                {
                    grade = 'S';
                }
                else if (score >= 1000)
                {
                    grade = 'A';
                }
                else if (score >= 700)
                {
                    grade = 'B';
                }
                else if (score >= 300)
                {
                    grade = 'C';
                }
                else
                {
                    if (score <= 100)
                    {
                        score = 100;
                    }
                    grade = 'D';
                }

            }
            else
            {
                score = 0;
            }
        }
        else if(levelType == "Survival")
        {
            type = "WIN";
            if (score >= 300)
            {
                grade = 'S';
            }
            else if (score >= 180)
            {
                grade = 'A';
            }
            else if (score >= 120)
            {
                grade = 'B';
            }
            else if (score >= 60)
            {
                grade = 'C';
            }
            else
            {
                grade = 'D';
            }
        }
        
        int rewardAmt = CalculateReward(score, grade, winTime, winHealthPoints);
        pm.AddCoins(rewardAmt);
        newGameOverScreen.GetComponent<GameOverManager>().GameOver(type, score, grade, winTime, winHealthPoints, pm.currChar.name, rewardAmt);
        /*
        if (type == "LOSE")
        {
            GameObject newGameOverScreen = Instantiate(gameOverScreen);
        }
        else
        {
            Instantiate(winScreen);
        }
        */


    }

    public int CalculateReward(int score, char grade, float winTime, float healthLeft)
    {
        if (levelType == "Boss" && difficultyMultiplier != 0)
        {
            if (grade == 'S')
            {
                return 20;
            }
            else if (grade == 'A')
            {
                return 8;
            }
            else if (grade == 'B')
            {
                return 6;
            }
            else if (grade == 'C')
            {
                return 4;
            }
            else if (grade == 'D')
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        else if (levelType == "Survival" && difficultyMultiplier != 0)
        {
            return 10 * (int)winTime / 60;
        }
        else
        {
            return 0;
        }
    }

    public void ShowUpgradeScreen()
    {
        Instantiate(upgradePopup);
        Time.timeScale = 0f;
        nextUpgradeTime += upgradeCD;

    }

    void SetPlayerUpgrades(GameObject player)
    {
        player.GetComponent<PlayerBehaviour>().damage = PlayerPrefs.GetInt("Damage");
        player.GetComponent<HealthManager>().healthPoints = PlayerPrefs.GetInt("Health");
        player.GetComponent<PlayerMovement>().playerMoveSpeed = PlayerPrefs.GetInt("Speed");

        Debug.Log(player.GetComponent<PlayerBehaviour>().damage);
        Debug.Log(player.GetComponent<HealthManager>().healthPoints);
        Debug.Log(player.GetComponent<PlayerMovement>().playerMoveSpeed);
    }
}