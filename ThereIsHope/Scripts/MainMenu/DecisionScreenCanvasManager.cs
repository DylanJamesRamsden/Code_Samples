using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionScreenCanvasManager : MonoBehaviour
{

    [Header("Condition Buttons")]
    public Text nextLevel1;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.isLevelDone == true && GameManager.instance.level < 4)
        {
            nextLevel1.enabled = true;
        }
        else
        {
            nextLevel1.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
