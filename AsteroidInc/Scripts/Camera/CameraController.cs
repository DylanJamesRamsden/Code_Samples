using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header ("Camera Movement Values:")]
    [Range (0f, 10f)]
    public float rotationSpeed;
    [Range(0f, 10f)]
    public float smoothingValue;
    public GameObject objectToRotateAround;
    Vector3 _cameraOffSet;
    //bool ZoomedIn = false;
    //public float CameraMoveSpeed;
    //public float DragSpeed;

    //[Header("Zoom Values")]
    //public float minZoom;
    //public float maxZoom;
    //public float ZoomSensitivity;

    //[Header ("Planet Clicking Values:")]
    //float time = 0;
    //int clickCounter = 0;
    //bool initiateClickTimer = false;    
    //GameObject planetPicked = null;

    //[Header("Camera Positions:")]
    //public GameObject p1TurnPosition;
    //public GameObject p2TurnPosition;

    //float CameraY = 0;
    //public bool initiateCameraChange = false;

    //Camera GameCamera;
    //Vector3 originalPosition;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        _cameraOffSet = Camera.main.transform.position - objectToRotateAround.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //CameraZoom();
        //unZoomedRotate();
        //PlanetClick();
        //EndTurnCamera();

        //CameraZoom();
    }

    private void FixedUpdate()
    {
        CameraMovement();

        //CameraZoom();
        //unZoomedRotate();
        //EndTurnCamera();
    }

    void CameraMovement()
    {
        if (Input.GetMouseButton(2))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.right);
            camTurnAngle = camTurnAngle * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            _cameraOffSet = camTurnAngle * _cameraOffSet;
        }
        else
        {
            if (pManager.gameOver == true)
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(10f * Time.deltaTime, Vector3.up);

                _cameraOffSet = camTurnAngle * _cameraOffSet;
            }
        }

        Vector3 newPos = objectToRotateAround.transform.position + _cameraOffSet;

        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, newPos, smoothingValue);

        Camera.main.transform.LookAt(objectToRotateAround.transform.position);
    }
}
