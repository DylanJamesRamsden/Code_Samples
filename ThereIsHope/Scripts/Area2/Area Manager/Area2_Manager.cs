using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Area2_Manager : MonoBehaviour
{

    public bool inDialogueInteraction = false;

    public bool inDemonGame = false;
    public bool demonGameFinished = false;
    public int todHealth;

    public Area2_DialogueCanvasManager dcManager;

    public Area2_DiaryController a2DiaryController;

    public bool isComplete = false;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        demonGame();

        isLevelComplete();

        isDead();
    }

    void demonGame()
    {
        if (demonGameFinished == true)
        {
            a2DiaryController.objective6complete = true;
            a2DiaryController.objective7Active = true;

        }
    }

    void isLevelComplete()
    {
        if (isComplete == true)
        {
            GameManager.instance.populatePlayerDecisions();
            GameManager.instance.level += 1;
            GameManager.instance.isLevelDone = true;

            SceneManager.LoadScene(3);
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
}
