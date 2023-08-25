using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLight : MonoBehaviour
{
    public static GetLight Instance;

    public Light DirectLight;

    private void Awake()
    {
        Instance = this;
        
        DirectLight = GetComponent<Light>();
    }
}
