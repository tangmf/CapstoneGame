using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject transition;

    public GameObject loadScreen;
    public Slider loadBar;

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneByName(string sceneName)
    {

        StartCoroutine(PlayTransition(sceneName));

    }

    public void LoadScreenByName(string sceneName)
    {
        StartCoroutine(PlayLoadScreen(sceneName));


    }

    public void AddSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    IEnumerator PlayTransition(string sceneName)
    {
        if(transition != null)
        {
            GameObject newTransition = Instantiate(transition);
            //newTransition.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(0.25f);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
    }

    IEnumerator PlayLoadScreen(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressVal = Mathf.Clamp01(operation.progress / 0.9f);

            loadBar.value = progressVal;
            yield return null;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
