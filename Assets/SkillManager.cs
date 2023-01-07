using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float coolDownTime = 5.0f;
    public Slider coolDownSlider;
    // Start is called before the first frame update
    void Start()
    {
        coolDownSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextActionTime)
        {
            coolDownSlider.value = (nextActionTime - Time.time) / coolDownTime * coolDownSlider.maxValue;
        }
        else
        {
            coolDownSlider.value = 0;
        }

        if (Input.GetKeyDown("q"))
        {
            if (Time.time >= nextActionTime)
            {
                StartCoroutine(Buff());
                nextActionTime += coolDownTime;
            }
            else
            {
                
            }
        }
    }

    IEnumerator Buff()
    {
        this.GetComponent<PlayerMovement>().playerMoveSpeed += 5;
        this.GetComponent<PlayerMovement>().playerJumpSpeed += 10;
        yield return new WaitForSeconds(5.0f);
        this.GetComponent<PlayerMovement>().playerMoveSpeed -= 5;
        this.GetComponent<PlayerMovement>().playerJumpSpeed -= 10;
    }
}
