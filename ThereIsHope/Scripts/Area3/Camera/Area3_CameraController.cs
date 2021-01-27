using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area3_CameraController : MonoBehaviour
{
    [Header ("Worldspace Variables")]
    public GameObject player;
    public Area3_PlayerController pController;
    public Area3_Manager a3Manager;

    GameObject lastHitWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pController.pDiary.isOpen == false && a3Manager.inDialogueInteraction == false && a3Manager.inComputerInteraction == false)
        {
            transform.position = new Vector3(player.transform.position.x, -3f, player.transform.position.z + 4);
        }

        //hideWalls();
    }

    void hideWalls()
    {
        lastHit();

        RaycastHit hit;
        Ray ray = new Ray(player.transform.position, Camera.main.transform.position);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "House")
            {
                lastHitWall = hit.transform.gameObject;

                MeshRenderer hitMesh = hit.transform.gameObject.GetComponent<MeshRenderer>();
                hitMesh.enabled = false;
            }
        }
    }

    void lastHit()
    {
        if (lastHitWall != null)
        {
            MeshRenderer lastHitMesh = lastHitWall.transform.gameObject.GetComponent<MeshRenderer>();
            lastHitMesh.enabled = true;
        }
    }
}
