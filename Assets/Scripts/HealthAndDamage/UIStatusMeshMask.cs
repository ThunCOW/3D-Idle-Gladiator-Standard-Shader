using UnityEngine;
using UnityEngine.UI;

public class UIStatusMeshMask : MaskableGraphic
{
    public float Width;
    public float Height;

    public float WidthScale;
    public float HeightScale;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Vector3 vec_00 = new Vector3(0, 0);
        Vector3 vec_01 = new Vector3(0, Height * HeightScale);
        Vector3 vec_10 = new Vector3(Width * WidthScale, 0);
        Vector3 vec_11 = new Vector3(Width * WidthScale, Height * HeightScale);

        vh.AddUIVertexQuad(new UIVertex[]
        {
            new UIVertex { position = vec_00, color = Color.green },
            new UIVertex { position = vec_01, color = Color.green },
            new UIVertex { position = vec_11, color = Color.green },
            new UIVertex { position = vec_10, color = Color.green },
        });
    }

    public void UpdateMask(float WidthScale, float HeightScale)
    {
        this.WidthScale = WidthScale;
        this.HeightScale = HeightScale;

        SetVerticesDirty();
    }
}
