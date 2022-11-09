using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInput : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {

            TogglePause();

        }

    }

    public void TogglePause()
    {
        if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            player.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            player.SetActive(false);
        }
    }
}
