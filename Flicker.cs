using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlickerType 
{
    Sin,
    Indecator
}

public class Flicker : MonoBehaviour
{
    Light light;

    public FlickerType type;

    public float speed = 1f;

    [Range(0, 0.5f)]
    public float minIntensity = 0;

    [Range(0.5f, 1)]
    public float maxIntensity = 1;

    public Gradient color;

    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        switch (type) 
        {
            case FlickerType.Sin:
                light.intensity = (Mathf.Sin(t * speed) * 0.5f + 0.5f) * (maxIntensity - minIntensity) + minIntensity;
                light.color = color.Evaluate(Mathf.Sin(t * speed));
                break;

            case FlickerType.Indecator:
                light.intensity = ((Mathf.Sign(Mathf.Sin(t * speed))) * 0.5f + 0.5f) * (maxIntensity - minIntensity) + minIntensity;
                light.color = color.Evaluate(Mathf.Sign(Mathf.Sin(t * speed)));
                break;
        }
    }

}
