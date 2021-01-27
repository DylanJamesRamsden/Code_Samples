using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Area3_Manager : MonoBehaviour
{

    public bool inDialogueInteraction = false;
    public bool inComputerInteraction = false;
    public bool inObjectiveInteraction = false;

    public bool isComplete = false;
    public bool isAlive = true;

    public int levelTimer = 600;
    int levelTimerDecreaseFrames = 60;
    int levelTimerDecreaseCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelComplete();
        isDead();
    }

    void FixedUpdate()
    {
        runLevelTimer();
    }

    void levelComplete()
    {
        if (isComplete == true)
        {
            GameManager.instance.populatePlayerDecisions();
            GameManager.instance.level += 1;
            GameManager.instance.isLevelDone = true;

            SceneManager.LoadScene(8);
            GameManager.instance.menuMusic.Play();
        }
    }

    void isDead()
    {
        if (isAlive == false)
        {
            GameManager.instance.populatePlayerDecisions();

            SceneManager.LoadScene(2);
            GameManager.instance.menuMusic.Play();
        }
    }

    void runLevelTimer()
    {
        if (levelTimerDecreaseCounter < levelTimerDecreaseFrames)
        {
            levelTimerDecreaseCounter++;
        }
        else
        {
            levelTimer--;
            levelTimerDecreaseCounter = 0;
        }

        if (levelTimer == 0)
        {
            isAlive = false;
        }
    }
}
