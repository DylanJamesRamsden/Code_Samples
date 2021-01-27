using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2_CameraController : MonoBehaviour
{

    public GameObject player;
    public Area2_PlayerController pController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
