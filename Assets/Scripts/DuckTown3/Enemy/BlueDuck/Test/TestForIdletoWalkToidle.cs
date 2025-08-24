using UnityEngine;

//just a test from walk to idle to walk to idle animation
public class TestForIdletoWalkToidle : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private bool isWalking = false;

    private void Start()
    {
        animator.Play("BlueDuckIdle");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        { 
            isWalking = !isWalking;
            if (isWalking)
            {
                animator.Play("BlueDuckWalk");
            }
            else
            {
                animator.Play("BlueDuckIdle");
            }
        }
    }
}
