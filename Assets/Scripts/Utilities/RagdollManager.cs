using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public bool RagdollActive;

    private Rigidbody rb;
    private Animator characterAnim;

    [Header("________Ragdoll Var________")]
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
    }

    public void Activate(bool active)
    {
        if (RagdollActive)
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
    }

    public void RagdollDeath(Vector3 forceDir)
    {
        if (RagdollActive)
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
    }

    private void OnEnable()
    {
        Activate(false);
        //RagdollRig.SetActive(false);
        //RagdollDeath(Vector3.back);

        if (!RagdollActive)
        {
            foreach (Rigidbody rig in RagdollRigidBodyList)
            {
                //Destroy(rig.GetComponent<ConfigurableJoint>());
                //Destroy(rig.GetComponent<CapsuleCollider>());
                //Destroy(rig);
            }
        }
    }
}
