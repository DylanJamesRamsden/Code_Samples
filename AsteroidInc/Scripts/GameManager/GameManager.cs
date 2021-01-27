using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Player Teams:")]
    public string p1Team;
    public string p2Team;

    [Header("Audio Variables")]
    public AudioSource gameAudio;
    public bool mutedAudio = false;

    public bool isAIgame = false;
    public string AIgameType = "";
    public string AIDifficulty = "";

    // Start is called before the first frame update
    void Start()
    {
        p1Team = "USA";
        p2Team = "USSR";

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playStopMusic();
    }

    void playStopMusic()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mutedAudio == false)
            {
                mutedAudio = true;
                gameAudio.Pause();
            }
            else if (mutedAudio == true)
            {
                mutedAudio = false;
                gameAudio.UnPause();
            }
        }
    }
}
