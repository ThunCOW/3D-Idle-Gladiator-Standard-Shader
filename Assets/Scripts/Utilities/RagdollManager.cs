using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    private Vector3 posDef;
    private Quaternion rotDef;
    
    private Rigidbody rb;
    private Animator characterAnim;

    [Header("************* Ragdoll Editor Filled ***********")]
    public GameObject RagdollRig;
    [Space]

    public bool GetRagdollVars;
    public List<Collider> RagdollColliderList;
    public List<Rigidbody> RagdollRigidBodyList;
    [Space]
    public string BoneRootName, BoneHeadName;
    public Rigidbody RagdollRootRb;
    public Rigidbody RagdollHeadRb;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (GetRagdollVars)
        {
            GetRagdollVars = false;

            RagdollRigidBodyList = transform.GetChild(0).GetComponentsInChildren<Rigidbody>().ToList();
            RagdollColliderList = transform.GetChild(0).GetComponentsInChildren<Collider>().ToList();

            foreach (Rigidbody rb in RagdollRigidBodyList)
            {
                if (BoneRootName == rb.name)
                    RagdollRootRb = rb;
                else if (BoneHeadName == rb.name)
                    RagdollHeadRb = rb;
            }
        }
#endif
    }

    void Awake()
    {
        characterAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        posDef = RagdollRig.transform.position;
        rotDef = RagdollRig.transform.rotation;
    }

    private void Activate(bool active)
    {
        //foreach (var col in RagdollColliderList)
        //{
        //    col.isTrigger = false;
        //    col.enabled = active;
        //}
        foreach (var rb in RagdollRigidBodyList)
        {
            //rb.detectCollisions = active;
            rb.isKinematic = !active;
        }

        characterAnim.enabled = !active;
        if (rb != null)
        {
            //rb.detectCollisions = !active;
            rb.isKinematic = active;
        }
    }

    public void RagdollDeath(Vector3 forceDir)
    {
        Activate(true);
        foreach (var col in RagdollColliderList)
        {
            col.isTrigger = false;
            col.enabled = true;
        }
        RagdollRootRb.AddForce(forceDir * 25, ForceMode.Impulse);
        RagdollHeadRb.AddForce(forceDir * 25, ForceMode.Impulse);
        //RagdollRootRb.AddTorque(Vector3.left * 100000);
    }

    public void RagdollOff()
    {
        Activate(false);
        foreach (var col in RagdollColliderList)
        {
            col.isTrigger = true;
            col.enabled = true;
        }
        RagdollRig.transform.position = posDef;
        RagdollRig.transform.rotation = rotDef;

        var bloodDecalList = RagdollRig.GetComponentsInChildren<BFX_ShaderProperies>();
        for (int i = bloodDecalList.Length - 1; i >= 0; i--)
            Destroy(bloodDecalList[i].gameObject);
    }     
}
