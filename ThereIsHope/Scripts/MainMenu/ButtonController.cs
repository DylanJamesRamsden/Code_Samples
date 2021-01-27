using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    [Header ("Button Variables")]
    public Text button1;
    public Text button2;

    // Start is called before the first frame update
    void Start()
    {
        button2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hoverOver()
    {
        button1.enabled = false;
        button2.enabled = true;
    }

    public void hoverLeft()
    {
        button1.enabled = true;
        button2.enabled = false;
    }

    public void playClick()
    {
        SceneManager.LoadScene(4); //Change to 4
        GameManager.instance.menuMusic.Stop();
        GameManager.instance.pDecisions.resetQueue();
    }

    public void mainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void reTryClick()
    {
        if (GameManager.instance.isLevelDone == true)
        {
            GameManager.instance.level--;
            switch (GameManager.instance.level)
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    SceneManager.LoadScene(5);
                    break;
                case 3:
                    SceneManager.LoadScene(6);
                    break;
            }
        }
        else
        {
            switch (GameManager.instance.level)
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    SceneManager.LoadScene(5);
                    break;
                case 3:
                    SceneManager.LoadScene(6);
                    break;
            }
        }

        GameManager.instance.pDecisions.resetQueue();
        GameManager.instance.resetPlayerDecisions();
        GameManager.instance.menuMusic.Pause();
    }

    public void quitClick()
    {
        Application.Quit();
    }

    public void playerDecisionsClick()
    {
        SceneManager.LoadScene(3);
    }

    public void nextLevelClick()
    {
        switch (GameManager.instance.level)
        {
            case 2:
                SceneManager.LoadScene(7);
                break;
            case 3:
                SceneManager.LoadScene(6);
                break;
        }

        GameManager.instance.isLevelDone = false;

        GameManager.instance.pDecisions.resetQueue();
        GameManager.instance.resetPlayerDecisions();

        GameManager.instance.menuMusic.Stop();
    }
}
