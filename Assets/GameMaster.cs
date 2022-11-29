using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject gameStartScreen;

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
        player.transform.position = lastCheckPointPos;


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
        yield return new WaitForSeconds(1.0f);

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

        StartCoroutine(WrapUp(type));

    }

    IEnumerator WrapUp(string type)
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1.0f);
        GameObject newPopUp = Instantiate(popUp);
        yield return new WaitForSeconds(1.0f);
        Destroy(newPopUp);


        Time.timeScale = 1.0f;
        if (type == "LOSE")
        {
            GameObject newGameOverScreen = Instantiate(gameOverScreen);
        }
        else
        {
            Instantiate(winScreen);
        }


    }


}
