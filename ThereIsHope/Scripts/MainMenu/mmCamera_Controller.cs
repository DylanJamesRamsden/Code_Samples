using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class mmCamera_Controller : MonoBehaviour
{

    [Header ("PostProcessing Variables")]
    public PostProcessingProfile ppProfile;

    [Header ("Menu Debrief Variables")]
    public Canvas MainMenuCanvas;
    public Text WarningText1;
    public Text WarningText2;

    bool transission2menu = false;

    // Start is called before the first frame update
    void Start()
    {
        startVignette();

        MainMenuCanvas.enabled = false;

        WarningText2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transission2menu == true)
        {
            finishedIntro();
        }
    }

    void startVignette()
    {
        VignetteModel.Settings vigSettings = ppProfile.vignette.settings;

        vigSettings.intensity = 0.75f;

        ppProfile.vignette.settings = vigSettings;
    }

    public void ChangeTextTo2()
    {
        WarningText2.enabled = true;
        WarningText1.enabled = false;
    }

    public void Fade2menu()
    {
        transission2menu = true;
    }

    void finishedIntro()
    {
        WarningText2.enabled = false;

        VignetteModel.Settings vigSettings = ppProfile.vignette.settings;

        if (vigSettings.intensity > 0.45f)
        {
            vigSettings.intensity -= 0.001f;
        }
        else
        {
            transission2menu = false;
        }


        MainMenuCanvas.enabled = true;

        ppProfile.vignette.settings = vigSettings;
    }
}
