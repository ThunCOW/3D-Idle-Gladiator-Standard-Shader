using System;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Breatplate,
    Shoulder,
    Gauntlets,
    Pants,
    Shoes,
    PrimaryWeapon,              // Left Hand is Primary
    SecondaryWeapon             // Right Hand
}

public class ItemScriptableObject : ScriptableObject, ICloneable
{
    protected string _id;
    public string ID { get { return _id; } }
    public string Name;
    public EquipmentType Type;
    public int Weight;
    public int Value;
    public int RequiredLevel;
    [Space]
    public GameObject ModelPrefab;
    public Sprite ModelSprite;

    protected GameObject modelSceneRef;
    protected CharacterManager character;
    protected MonoBehaviour activeMonoBehaviourRef;

    protected static readonly StringBuilder StringBuild = new StringBuilder();

    protected virtual void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    /// <summary>
    /// Instantiating equipments in a custom hierarcy
    /// </summary>
    /// <param name="Parent">Equipment is going to be be child of the Parent in the scene</param>
    /// <param name="CharacterManager"></param>
    /// <param name="TargetSkinnedMesh">Since its custom instantiated, Skinned meshes need to retarget to the right bones which is contained in the main body</param>
    public virtual void Equip(Transform Parent, CharacterManager CharacterManager, SkinnedMeshRenderer TargetSkinnedMesh)
    {
        activeMonoBehaviourRef = CharacterManager;
        character = CharacterManager;

        modelSceneRef = Instantiate(ModelPrefab);
        modelSceneRef = modelSceneRef.transform.GetChild(0).gameObject;

        SkinnedMeshRenderer skinnedMesh = modelSceneRef.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedMesh != null)
        {
            TransferSkinnedMeshes(modelSceneRef.GetComponentInChildren<SkinnedMeshRenderer>(), TargetSkinnedMesh, Parent);
        }
        else
        {
            Debug.LogWarning("Skinned Mesh Not Found");
            //modelSceneRef.transform.SetParent(Parent, false);
            //modelSceneRef.transform.position = modelSceneRef.transform.position;
            //modelSceneRef.transform.rotation = modelSceneRef.transform.rotation;
        }
    }
    public virtual void Unequip()
    {
        Destroy(modelSceneRef);
    }

    public static void TransferSkinnedMeshes(SkinnedMeshRenderer SkinnedMeshRenderer, SkinnedMeshRenderer TargetSkinnedMeshRenderer, Transform NewParent)
    {
        GameObject SkinnedMeshRendererParent = SkinnedMeshRenderer.transform.parent.gameObject;
        Transform newArmature = TargetSkinnedMeshRenderer.rootBone;
        Transform[] newBones = TargetSkinnedMeshRenderer.bones;

        SkinnedMeshRenderer.rootBone = newArmature;
        SkinnedMeshRenderer.bones = newBones;
        SkinnedMeshRenderer.transform.SetParent(NewParent);
        SkinnedMeshRenderer.transform.localPosition = Vector3.zero;

        if (!Application.isPlaying)
        {
            DestroyImmediate(SkinnedMeshRendererParent);
        }
        else
        {
            Destroy(SkinnedMeshRendererParent);
        }
    }

    public virtual string GetDescriptionComparison(ItemScriptableObject EquippedItem) { return ""; }

    public virtual object Clone() { return null; }
}