using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public GameObject loadScreen;
    public Slider loadBar;

    public void NextScene()
    {
        PlayTransition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void PreviousScene()
    {
        PlayTransition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneByName(string sceneName)
    {
        PlayTransition();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);


    }

    public void LoadScreenByName(string sceneName)
    {
        StartCoroutine(PlayLoadScreen(sceneName));


    }

    public void AddSceneByName(string sceneName)
    {
        PlayTransition();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    IEnumerator PlayTransition()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
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
}
