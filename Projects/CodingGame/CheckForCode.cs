using Coder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCode : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CanvasManager.ShowCanvas.Invoke("CodeText");
            PlayerScripts.DisableMovement.Invoke();
        }
    }
}
