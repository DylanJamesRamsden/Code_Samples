using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Area1LevelManager : MonoBehaviour
{
    [Header ("Dialogue Interaction variables")]
    public bool inDialogueInteraction = false;

    [Header("Storyline Variables")]
    public bool JackFirstInteraction = true;

    public bool FindingLunchTriggered = false;
    public bool FoundLunch = false;
    public bool JackSecondInteraction = false;

    [Header("Lunch Finding Varaibles")]
    public int numberOfLunchBoxes = 0;

    [Header("Diary:")]
    public GameObject Diary;
    Diary_Controller dController;

    public bool isAlive = true;
    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        dController = Diary.GetComponent<Diary_Controller>();
    }

    void isPlayerDead()
    {
        if (isAlive == false)
        {
            GameManager.instance.populatePlayerDecisions();

            SceneManager.LoadScene(2);
            GameManager.instance.menuMusic.Play();
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

    // Update is called once per frame
    void Update()
    {
        LunchCollectionController();

        isPlayerDead();
        isLevelComplete();
    }

    void LunchCollectionController()
    {
        if (numberOfLunchBoxes == 5) 
        {
            JackSecondInteraction = true;

            JackFirstInteraction = false;

            FoundLunch = true;

            dController.objective2complete = true;
            dController.objective3Active = true;
        }
    }

    public void foundLunchBox()
    {
        numberOfLunchBoxes++;

        dController.foundLunchBox();
    }

    void objectiveHandler()
    {
        
    }

    void ChapterHandler()
    {

    }
}
