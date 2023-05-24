using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private List<Transform> lightParentList;

    public List<Transform> LightParentList => lightParentList;

    private void Start()
    {
        
    }

    private void Test()
    {

    }

    public float GetLightIntensity(Transform lightParent)
    {
        return lightParent.GetComponentInChildren<Light>().intensity;
    }

    public float GetLightRange(Transform lightParent)
    {
        return lightParent.GetComponentInChildren<Light>().range;
    }

    public Vector3 GetLightColorHSV(Transform lightParent)
    {
        Color.RGBToHSV(lightParent.GetComponentInChildren<Light>().color, out float h, out float s, out float v);
        return new Vector3(h, s, v);
    }

    public void SetLightRange(Transform lightParent, float range)
    {
        foreach (var lightBulb in lightParent.GetComponentsInChildren<Light>())
        {
            lightBulb.intensity = range;
        }    
    }

    public void SetLightIntensity(int parentIndex, float intensity)
    {
        SetLightIntensity(lightParentList[parentIndex], intensity);
    }
    
    public void SetLightIntensity(Transform lightParent, float intensity)
    {
        foreach (var lightBulb in lightParent.GetComponentsInChildren<Light>())
        {
            lightBulb.intensity = intensity;
        }
    }

    public void SetLightColor(int parentIndex, Color color)
    {
        SetLightColor(lightParentList[parentIndex], color);
    }

    public void SetLightColor(Transform lightParent, Color color)
    {
        foreach (var lightBulb in lightParent.GetComponentsInChildren<Light>())
        {
            lightBulb.color = color;
        }
    }
}
