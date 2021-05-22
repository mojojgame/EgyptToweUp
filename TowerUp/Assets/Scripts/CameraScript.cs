using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript script;
    public Camera cam;
    Transform myTransform;
    public float cameraSpeed;
    private void Awake()
    {
        script = this;
        myTransform = transform;
    }
    void Update()
    {
        if (PlayerScript.script.canCameraMove)
        {
            myTransform.Translate(Vector2.up * cameraSpeed * Time.deltaTime);
        }
    }
}
