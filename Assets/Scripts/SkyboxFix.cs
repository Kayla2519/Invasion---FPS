using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxFix : MonoBehaviour
{
    public Material skyboxMaterial;

    void Start()
    {
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment();
        }
    }
}
