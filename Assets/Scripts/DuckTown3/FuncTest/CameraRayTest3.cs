using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraRayTest3 : MonoBehaviour
{
    [SerializeField]private GameObject castShadow;
    private GameObject ShadowInstance;

    [SerializeField]private GameObject BloodMoonPrefab;
    private Collider[] colliders = new Collider[10];
    private Vector3? groudHitpointer;

    private bool isFallingDown = false;
    [SerializeField] float delayTime = 1.0f;

    [SerializeField]bool useGizmo = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        { 
            ShadowInstance = Instantiate(castShadow);
        }
        if (ShadowInstance != null)
        {
            
            ChangeShadowPosWithCameraMouse();
            ShadowInstance.transform.position = groudHitpointer.Value;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BloodMoonPrefab, groudHitpointer.Value, Quaternion.identity);

                StartCoroutine(DelayDamageForBooldMoon(groudHitpointer.Value, delayTime));

                Destroy(ShadowInstance);
                ShadowInstance = null;
                groudHitpointer = null;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //避免悬空引用 
            //话说这个可以当陷阱用（笑）
            ShadowInstance = null;
        }

    }

    private void ChangeShadowPosWithCameraMouse()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Ground")))
        { 
            groudHitpointer = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        if (!useGizmo)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * 100f);

        if (groudHitpointer.HasValue)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groudHitpointer.Value, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groudHitpointer.Value, 3.0f);
        }

        
    }

    //更推荐传参
    private IEnumerator DelayDamageForBooldMoon(Vector3 groundPinterCache, float delayTime)
    { 
        
        yield return new WaitForSeconds(delayTime);

        int hitCount = Physics.OverlapSphereNonAlloc(groundPinterCache, 3.0f, colliders, LayerMask.GetMask("Enemy"));

        //Debug.Log("有没有collider:" + hitCount);
        for (int i = 0; i < hitCount; i++)
        {
            IAttackable attackable = colliders[i].GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.TakeDamage();
            }
        }
        
    }
}
