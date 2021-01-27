using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        hidePauseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hidePauseMenu()
    {
        pMenuObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void showPauseMenu()
    {
        pMenuObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void RestartGameClick()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktopClick()
    {
        Application.Quit();
    }
}
