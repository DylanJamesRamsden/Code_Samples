using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValuePopUpController : MonoBehaviour
{
    TextMeshPro textMesh;

    PlayManager pManager;

    Vector3 startingPosition;

    public float timeAlive = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        //StartSetUp(200, "resource");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DestroyText();
    }

    void RotateToCamera()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void StartSetUp(int valueToShow, string type)
    {
        if (type == "damage")
        {
            textMesh.faceColor = Color.red;
            textMesh.SetText("-" + valueToShow.ToString() + "HP");
        }
        else if (type == "resource")
        {
            textMesh.faceColor = Color.green;
            textMesh.SetText("+" + valueToShow.ToString() + "$");
        }
    }

    void DestroyText()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > 7)
        {
            Destroy(transform.gameObject);
        }
    }
}
