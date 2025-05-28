using UnityEngine;

public class EnemyYellowGay3 : MonoBehaviour, IAttackable
{
    public GameObject blastMultiColor;
    public Transform blastPointer;
    public void TakeDamage()
    {
        Instantiate(blastMultiColor, blastPointer.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
