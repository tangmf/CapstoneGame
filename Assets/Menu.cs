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
        Time.timeScale = 1.0f;

    }

    public void LoadScreenByName(string sceneName)
    {
        loadScreen.SetActive(true);
        loadBar.value = 0;
        
        StartCoroutine(WaitBeforeLoadScreen(0.5f, sceneName));
        Time.timeScale = 1.0f;


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
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        while (!operation.isDone) 
        {
            float progressVal = Mathf.Clamp01(operation.progress / 0.9f);
            loadBar.value = progressVal;
            if(operation.progress >= 0.9f)
            {
                // Always wait 1 second before going next
                yield return new WaitForSeconds(1.0f);
                operation.allowSceneActivation = true;
            }
            
            yield return null;

        }
        yield return new WaitForEndOfFrame();
        yield return null;

    }

    IEnumerator WaitBeforeLoadScreen(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(PlayLoadScreen(sceneName));
    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
