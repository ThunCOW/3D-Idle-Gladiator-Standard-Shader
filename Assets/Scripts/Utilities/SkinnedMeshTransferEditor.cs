#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class SkinnedMeshTransferEditor : EditorWindow
{
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public SkinnedMeshRenderer TargetSkinnedMeshRenderer;
    public Transform NewParent;

    private Transform newArmature;

    private ScriptableObject scriptableObj;
    private SerializedObject serialObj;

    private Vector2 viewScrollPosition;

    [MenuItem("TUNC/Skinned Mesh Transfer")]
    private static void OpenWindow()
    {
        EditorWindow editorWindow = (SkinnedMeshTransferEditor)GetWindow(typeof(SkinnedMeshTransferEditor), false, "Skinned Mesh Transfer");
        editorWindow.minSize = new Vector2(400, 250);
        GUI.contentColor = Color.white;
        editorWindow.Show();
    }

    private void OnEnable()
    {
        scriptableObj = this;
        serialObj = new SerializedObject(scriptableObj);
    }

    private void OnGUI()
    {
        DrawMain();
    }

    private void DrawMain()
    {
        viewScrollPosition = EditorGUILayout.BeginScrollView(viewScrollPosition, false, false);

        GUILayout.Space(20);
        this.SkinnedMeshRenderer = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Skinned Mesh Renderer", this.SkinnedMeshRenderer, typeof(SkinnedMeshRenderer), true);
        GUILayout.Space(7);
        TargetSkinnedMeshRenderer = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Target Skinned Mesh Renderer", TargetSkinnedMeshRenderer, typeof(SkinnedMeshRenderer), true);
        GUILayout.Space(7);
        NewParent = (Transform)EditorGUILayout.ObjectField("New Parent", NewParent, typeof(Transform), true);
        GUILayout.Space(15);

        if (GUILayout.Button("TRANSFER", GUILayout.MinWidth(150), GUILayout.MinHeight(30), GUILayout.ExpandWidth(true)))
        {
            ItemScriptableObject.TransferSkinnedMeshes(SkinnedMeshRenderer, TargetSkinnedMeshRenderer, NewParent);
        }

        serialObj.ApplyModifiedProperties();

        GUILayout.Space(20);
        GUILayout.EndScrollView();
    }
}
#endif