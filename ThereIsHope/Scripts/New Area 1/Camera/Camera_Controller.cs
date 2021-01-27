using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Camera_Controller : MonoBehaviour
{
    [Header("Player Variables")]
    public GameObject player;

    public Player_Controller pController;

    [Header ("Post Processing Variables")]
    public PostProcessingProfile ppProfile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z); //Makes camera follow the player

        VignetteController();
    }

    void VignetteController()
    {
        if (pController.isSneaking == true)
        {
            VignetteModel.Settings vigSettings = ppProfile.vignette.settings;

            if (vigSettings.intensity < 0.7)
                vigSettings.intensity += 0.001f;

            ppProfile.vignette.settings = vigSettings;
        }
        else
        {
            VignetteModel.Settings vigSettings = ppProfile.vignette.settings;

            if (vigSettings.intensity > 0.59)
                vigSettings.intensity -= 0.001f;

            ppProfile.vignette.settings = vigSettings;
        }
    } //Controls the vignette when the player is crouching
}
