using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Controller : MonoBehaviour
{

    [Header("Rock UI")]
    public GameObject Icon1;
    public GameObject Icon2;
    MeshRenderer iconMeshRenderer1;
    MeshRenderer iconMeshRenderer2;

    [Header("Materials")]
    public Material InteractableIconMaterial;
    public Material PickUpIconMaterial;

    [Header ("Player Variables")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        iconMeshRenderer1 = Icon1.GetComponent<MeshRenderer>();
        iconMeshRenderer2 = Icon2.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RockSymbolController();
    }

    void RockSymbolController() //Rotates the bully's detection symbol to the camera
    {
        Icon1.transform.LookAt(Camera.main.transform.position);
        //Icon.transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iconMeshRenderer1.material = PickUpIconMaterial;
            iconMeshRenderer2.material = PickUpIconMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iconMeshRenderer1.material = InteractableIconMaterial;
            iconMeshRenderer2.material = InteractableIconMaterial;
        }
    }
}
