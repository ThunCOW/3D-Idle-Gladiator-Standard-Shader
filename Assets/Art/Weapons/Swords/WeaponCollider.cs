using System.Collections;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("*********** Blood Editor Filled ***********")]
    public GameObject BloodAttach;
    
    public GameObject[] BloodFX;

    // ****************** Private Fields *************
    private BoxCollider boxCollider;
    
    private Light DirLight;
    
    private Vector3 firstPos;
    private Vector3 secondPos;

    private string parentTag;

    private Vector3 hitDir;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        parentTag = transform.root.tag;

        DirLight = GetLight.Instance.DirectLight;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform parent = other.transform.root;

        // If weapon touches its own character, do nothing
        if (parent.CompareTag(parentTag))
            return;

        Vector3 hitPoint = other.ClosestPoint(transform.position);

        Debug.Log("Character = " + parent.gameObject.name + " Part = " + other.gameObject.name);

        //StartCoroutine(StopGameUntilCanContinue());
        hitDir = transform.position - other.transform.position;
        hitDir.Normalize();
        SpawnBlood(hitPoint, hitDir, other.transform);
        /*RaycastHit hit;
        Ray ray = new Ray(transform.position, dir);
        Physics.Raycast(ray, out hit);
        SpawnBlood(hit);*/

        boxCollider.enabled = false;
    }

    private void SpawnBlood(Vector3 hitPoint, Vector3 hitNormal, Transform hitTransform)
    {
        float angle = Mathf.Atan2(hitNormal.x, hitNormal.z) * Mathf.Rad2Deg + 180;

        GameObject instance;
        if (BattleManager.Instance.BloodFXPrefab == null)
            instance = Instantiate(BloodFX[Random.Range(0, BloodFX.Length)], hitPoint, Quaternion.Euler(0, angle, 0));
        else
            instance = Instantiate(BattleManager.Instance.BloodFXPrefab, hitPoint, Quaternion.Euler(0, angle, 0));


        var settings = instance.GetComponent<BFX_BloodSettings>();
        if (settings != null)
        {   
            //settings.FreezeDecalDisappearance = InfiniteDecal;
            settings.LightIntensityMultiplier = DirLight.intensity;
        }

        var nearestBone = GetNearestObject(hitTransform.transform.root, hitPoint);
        GameObject bloodBodyAttachedDecal = null;
        if (nearestBone != null)
        {
            bloodBodyAttachedDecal = Instantiate(BloodAttach);
            Transform bloodT = bloodBodyAttachedDecal.transform;
            bloodT.position = hitPoint;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hitPoint + hitNormal, Vector3.zero);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            //Destroy(attachBloodInstance, 20);
        }

        StartCoroutine(DestroyBloodInTimeObject(25, bloodBodyAttachedDecal, instance));

        if (BattleManager.Instance.PauseGameOnAttack)
            Debug.Break();
    }

    /*private void SpawnBlood(RaycastHit hit)
    {
        // var randRotation = new Vector3(0, Random.value * 360f, 0);
        // var dir = CalculateAngle(Vector3.forward, hit.normal);
        float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

        //var effectIdx = Random.Range(0, BloodFX.Length);
        var instance = Instantiate(BloodFX[spawnIndx], hit.point, Quaternion.Euler(0, angle + 90, 0));

        var settings = instance.GetComponent<BFX_BloodSettings>();
        //settings.FreezeDecalDisappearance = InfiniteDecal;
        settings.LightIntensityMultiplier = DirLight.intensity;

        var nearestBone = GetNearestObject(hit.transform.root, hit.point);
        if (nearestBone != null)
        {
            var attachBloodInstance = Instantiate(BloodAttach);
            var bloodT = attachBloodInstance.transform;
            bloodT.position = hit.point;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hit.point + hit.normal, Vector3.zero);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            //Destroy(attachBloodInstance, 20);
        }
    }*/

    Transform GetNearestObject(Transform hit, Vector3 hitPos)
    {
        var closestPos = 100f;
        Transform closestBone = null;
        var childs = hit.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            var dist = Vector3.Distance(child.position, hitPos);
            if (dist < closestPos)
            {
                closestPos = dist;
                closestBone = child;
            }
        }

        var distRoot = Vector3.Distance(hit.position, hitPos);
        if (distRoot < closestPos)
        {
            closestPos = distRoot;
            closestBone = hit;
        }
        return closestBone;
    }

    public void Activate(bool activate)
    {
        boxCollider.enabled = activate;
    }

    public void RagdollHit()
    {
        StartCoroutine(RagdollHitDelay());
    }

    IEnumerator RagdollHitDelay()
    {
        yield return new WaitForSeconds(0.1f);

        CharacterManager targetCharaterManager = transform.root.CompareTag("Player") ? BattleManager.Characters[Gladiator.Player] : BattleManager.Characters[Gladiator.Enemy];

        targetCharaterManager.Ragdoll.RagdollDeath(hitDir);
    }

    IEnumerator DestroyBloodInTimeObject(int WaitFor, GameObject BloodBodyAttachedDecal, GameObject BloodSprayAndDecal)
    {
        yield return new WaitForSeconds(WaitFor);

        Destroy(BloodBodyAttachedDecal);
        Destroy(BloodSprayAndDecal);
    }
}
