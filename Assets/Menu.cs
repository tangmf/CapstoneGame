using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

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
}
