using UnityEngine;

public class DuckInteractor : MonoBehaviour
{
    [Header("Gizmos Switch")]
    [SerializeField] bool showInteractableRangeGizmo = true;

    [SerializeField] float checkRadius = 5.0f;
    [SerializeField] LayerMask isInteractable;

    public Ui_interaction ui_Interaction;

    IInteractable interactable;

    Collider[] noAllcolliders = new Collider[10];

    //state caching
    bool wasVisibleLastFrame = false;

    private void Update()
    {

        //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, isInteractable);
        //GC
        int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, checkRadius, noAllcolliders, isInteractable);
        float closestDistance = Mathf.Infinity;
        interactable = null;

        for (int i = 0; i < colliderCount; i++)
        {
            IInteractable someInteractable = noAllcolliders[i].GetComponent<IInteractable>();
            if (someInteractable != null)
            {
                float distanceToDuck = Vector3.Distance(transform.position, noAllcolliders[i].transform.position);
                if (distanceToDuck < closestDistance)
                {
                    closestDistance = distanceToDuck;
                    interactable = someInteractable;
                }
            }
        }

        //show ui state
        bool hasTarget = (interactable != null);

        if (hasTarget != wasVisibleLastFrame)
        {
            if (hasTarget)
            {
                ui_Interaction.Show(interactable);
            }
            else
            {
                ui_Interaction.Hide();
            }
            wasVisibleLastFrame = hasTarget;
        }


        //Debug.Log(interactable.GetInteractPrompt());
        if (hasTarget && Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    //use for button click
    public void TryInteract()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showInteractableRangeGizmo) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
