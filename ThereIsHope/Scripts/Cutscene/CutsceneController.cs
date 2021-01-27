using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{

    public VideoPlayer cutscene;
    public int nextScene;

    int waitTimer = 60;
    int waitCounter = 0;
    bool canCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck == true)
        {
            if (cutscene.isPlaying == false)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        if (waitCounter < waitTimer)
        {
            waitCounter++;
        }
        else
        {
            canCheck = true;
        }
    }
}
