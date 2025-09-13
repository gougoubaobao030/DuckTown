using DuckTown3;
using System;
using Unity.VisualScripting;
using UnityEngine;
using DuckTown3.ObjectPool;


public class EnemyYellowGay3 : MonoBehaviour, IAttackable
{
    public static event Action<Vector3, Quaternion, float> OnMinionDied;
    private Vector3 spawnPos = Vector3.zero;
    private Quaternion spawnRpt = Quaternion.identity;

    public GameObject blastMultiColor;
    public Transform blastPointer;

    private bool isSettedPosRotInfo = false;

    private void Awake()
    {
        //spawnPos = transform.position;
        //spawnRpt = transform.rotation;
        //Debug.Log("Awake" + transform.position);
    }

    private void Start()
    {
        //Debug.Log("Start" + transform.position);
    }

    private void OnEnable()
    {
        //transform.position = spawnPos;
        //transform.rotation = spawnRpt;
        //Debug.Log("Enable" + transform.position);
        if (isSettedPosRotInfo) SetPosAndRot();
    }

    private void OnDisable()
    {
        //Debug.Log("Disable" + transform.position);
    }

    public void SetPosAndRotInfo(Vector3 pos, Quaternion rot)
    {
        spawnPos = pos;
        spawnRpt = rot;

        isSettedPosRotInfo = true;
    }

    public void SetPosAndRot()
    {
        transform.position = spawnPos;
        transform.rotation = spawnRpt;
    }

    public void TakeDamage(float amout)
    {
        //Instantiate(blastMultiColor, blastPointer.position, Quaternion.identity);
        var blast = ObjectPoolManager.Instance.Get(blastMultiColor);
        blast.transform.position = blastPointer.position;
        //Destroy(gameObject);
        OnMinionDied?.Invoke(spawnPos, spawnRpt, 3.0f);
        gameObject.SetActive(false);

        //Debug.Log("又遇到问题咯");

    }

}
