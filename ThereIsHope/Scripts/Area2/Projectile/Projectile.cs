using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Projectile Variables")]
    public float speed;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projectile.transform.Rotate(Vector3.left * 100f * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
