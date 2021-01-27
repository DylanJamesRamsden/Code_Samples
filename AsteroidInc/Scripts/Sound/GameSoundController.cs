using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundController : MonoBehaviour
{

    [Header("UI Audio:")]
    public AudioSource hoverSound;
    public AudioSource fightSound;
    public AudioSource explosionSound;

    bool gameAudioSet = false;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameAudioSet == false)
        {
            if (GameManager.instance.gameAudio.volume != 0.1f)
            {
                t += 0.1f * Time.deltaTime;
                GameManager.instance.gameAudio.volume = Mathf.Lerp(0.5f, 0.1f, t);
            }
            else gameAudioSet = true;
        }
    }

    public void HoverPlay()
    {
        if (GameManager.instance.mutedAudio == false)
        {
            hoverSound.Play();
        }
    }

    public void FightPlay()
    {
        if (GameManager.instance.mutedAudio == false)
        {
            fightSound.Play();
        }
    }

    public void ExplosionPlay()
    {
        if (GameManager.instance.mutedAudio == false)
        {
            explosionSound.Play();
        }
    }
}
