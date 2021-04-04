using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Выравнивает камеру по ширине
 width = const
 */

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class CameraFix : MonoBehaviour
{
    public bool orth = false;

    public float horizontalFOV = 120f;
    private float aspectRatio = 1f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        if (!orth)
        {
            cam.fieldOfView = calcVertivalFOV(horizontalFOV, cam.aspect);
        }
        else
        {
            cam.orthographicSize = calcVertivalFOV(horizontalFOV, cam.aspect);
        }

        calcVertivalFOV(horizontalFOV, aspectRatio);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        cam = GetComponent<Camera>();
#endif
        if (!orth)
        {
            cam.fieldOfView = calcVertivalFOV(horizontalFOV, cam.aspect);
        }
        else 
        {
            cam.orthographicSize = calcVertivalFOV(horizontalFOV, cam.aspect);
        }
        
        calcVertivalFOV(horizontalFOV, aspectRatio);
    }

    private float calcVertivalFOV(float hFOVInDeg, float aspectRatio)
    {
        float hFOVInRads = hFOVInDeg * Mathf.Deg2Rad;
        float vFOVInRads = 2 * Mathf.Atan(Mathf.Tan(hFOVInRads / 2) / aspectRatio);
        float vFOV = vFOVInRads * Mathf.Rad2Deg;
        return vFOV;
    }
}
