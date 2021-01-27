using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCanvasController : MonoBehaviour
{
    [Header ("Main Menu Objects:")]
    public GameObject MainMenuCanvasObject;
    public GameObject PlayMenuCanvasObject;
    public GameObject TeamSelectCanvasObject;
    public GameObject AIDifficultyCanvasObject;

    [Header("Audio:")]
    public AudioSource HoverSound;

    [Header("Team Variables:")]
    public RawImage Team1Image;
    public RawImage Team2Image;
    public Texture[] TeamImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        if (GameManager.instance.mutedAudio == false)
        {
            HoverSound.Play();
        }
    }

    public void PlayClick()
    {
        MainMenuCanvasObject.SetActive(false);
        PlayMenuCanvasObject.SetActive(true);
    }

    public void BackClick()
    {
        MainMenuCanvasObject.SetActive(true);
        PlayMenuCanvasObject.SetActive(false);
    }

    public void PvpClick()
    {
        PlayMenuCanvasObject.SetActive(false);
        TeamSelectCanvasObject.SetActive(true);
    }

    public void PlayerVsAIClick()
    {
        PlayMenuCanvasObject.SetActive(false);
        AIDifficultyCanvasObject.SetActive(true);
    }

    public void EasyAIClick()
    {
        GameManager.instance.isAIgame = true;
        GameManager.instance.AIDifficulty = "Easy";
        GameManager.instance.AIgameType = "SM";

        TeamSelectCanvasObject.SetActive(true);
        AIDifficultyCanvasObject.SetActive(false);
    }

    public void IntermediateAIClick()
    {
        GameManager.instance.isAIgame = true;
        GameManager.instance.AIDifficulty = "Intermediate";
        GameManager.instance.AIgameType = "SM";

        TeamSelectCanvasObject.SetActive(true);
        AIDifficultyCanvasObject.SetActive(false);
    }

    public void HardAIClick()
    {
        GameManager.instance.isAIgame = true;
        GameManager.instance.AIDifficulty = "Hard";
        GameManager.instance.AIgameType = "SM";

        TeamSelectCanvasObject.SetActive(true);
        AIDifficultyCanvasObject.SetActive(false);
    }

    public void EvoAIClick()
    {
        GameManager.instance.isAIgame = true;
        GameManager.instance.AIDifficulty = "";
        GameManager.instance.AIgameType = "Evo";

        TeamSelectCanvasObject.SetActive(true);
        AIDifficultyCanvasObject.SetActive(false);
    }

    public void CancelClick()
    {
        PlayMenuCanvasObject.SetActive(true);
        TeamSelectCanvasObject.SetActive(false);

        if (GameManager.instance.isAIgame == true)
        {
            GameManager.instance.isAIgame = false;
            GameManager.instance.AIDifficulty = "";
        }
    }

    public void StartGameClick()
    {
        SceneManager.LoadScene(1);
        //GameManager.instance.gameAudio.volume = 0.2f;
    }

    public void SwapTeamsClick()
    {
        if (Team1Image.texture == TeamImages[0])
        {
            Team1Image.texture = TeamImages[1];
            Team2Image.texture = TeamImages[0];

            GameManager.instance.p1Team = "USA";
            GameManager.instance.p2Team = "USSR";
        }
        else
        {
            Team1Image.texture = TeamImages[0];
            Team2Image.texture = TeamImages[1];

            GameManager.instance.p1Team = "USSR";
            GameManager.instance.p2Team = "USA";
        }
    }
}
