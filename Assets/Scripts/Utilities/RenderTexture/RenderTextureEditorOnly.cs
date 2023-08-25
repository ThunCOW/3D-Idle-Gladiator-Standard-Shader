#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RenderTextureEditorOnly : MonoBehaviour
{
    public RenderTexture RenderTexturePrefab;

    private Camera uiCamera;

    void Update()
    {
        if (uiCamera == null)
            uiCamera = GetComponent<Camera>();

        if (RenderTexturePrefab == null)
        {
            Debug.LogError("Set Render Texture Prefab!");
            return;
        }

        if (uiCamera.targetTexture != RenderTexturePrefab)
            uiCamera.targetTexture = RenderTexturePrefab;
    }
}
#endif