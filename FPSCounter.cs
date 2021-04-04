using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private void OnGUI()
    {
        int dpi = (int)Screen.dpi;

        GUIStyle style = new GUIStyle();

        style.fontSize = (int)(0.2 * dpi);

        GUI.Label(new Rect(10, 40, 100, 20), "fps: " + (int)(1f / Time.unscaledDeltaTime), style);
    }
}
