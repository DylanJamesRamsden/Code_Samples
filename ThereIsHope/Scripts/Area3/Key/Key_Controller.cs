using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Controller : MonoBehaviour
{
    [Header ("Key Variables")]
    public string keyTag;

    GameObject player;
    Area3_PlayerController pController;

    [Header("Key Sound")]
    public AudioSource keyPickupSound;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<Area3_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            pController.showInteractabledText = true;

            if (Input.GetKey(KeyCode.E))
            {
                pController.keyTag = keyTag;
                pController.showInteractabledText = false;

                keyPickupSound.Play();

                Destroy(transform.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.showInteractabledText = false;
        }
    }
}
