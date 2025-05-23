using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [Header("Bow Setup")]
    public Transform stringRestPosition;
    public Transform stringPullLimit;
    public Transform stringBone;
    public Transform topLimit;
    public Transform bottomLimit;
    public Transform arrowNockPoint;
    public GameObject arrowPrefab;

    [Header("Hand Transforms")]
    public Transform leftHand;
    public Transform rightHand;

    [Header("Input Actions")]
    public InputActionReference leftTriggerAction;
    public InputActionReference rightTriggerAction;

    [Header("Settings")]
    public float attachDistance = 0.15f;
    public float shootForceMultiplier = 50f;

    private Transform bowHand;
    private Transform pullingHand;
    private Transform otherHand;
    private GameObject currentArrow;
    private float maxPullDistance;

    void Start()
    {
        maxPullDistance = Vector3.Distance(stringRestPosition.position, stringPullLimit.position);
    }

    void Update()
    {
        if (!bowHand) return;
        

        // Attach string to the other hand when close
        if (!pullingHand && Vector3.Distance(otherHand.position, stringRestPosition.position) < attachDistance)
        {
            pullingHand = otherHand;
            SpawnArrow();
        }

        // Simulate string pull
        if (pullingHand && currentArrow)
        {
            Vector3 pullDir = (stringPullLimit.position - stringRestPosition.position).normalized;
            float pullAmount = Vector3.Dot(pullingHand.position - stringRestPosition.position, pullDir);
            pullAmount = Mathf.Clamp(pullAmount, 0, maxPullDistance);
            

            stringBone.position = stringRestPosition.position + pullDir * pullAmount;
            topLimit.localRotation = Quaternion.Euler(pullAmount * -15f, 0f, 0f);
            bottomLimit.localRotation = Quaternion.Euler(pullAmount * 15f, 0f, 0f);
            currentArrow.transform.position = arrowNockPoint.position + pullDir * pullAmount;
            currentArrow.transform.rotation = arrowNockPoint.rotation;

            if (IsTriggerHeld(pullingHand))
            {
                FireArrow(pullAmount);
            }
        }
    }

    public void OnBowGrabbed(SelectEnterEventArgs args)
    {
        bowHand = args.interactorObject.transform;
        pullingHand = null;
        currentArrow = null;

        // Use hierarchy check instead of direct equality
        if (bowHand.IsChildOf(leftHand))
            otherHand = rightHand;
        else
            otherHand = leftHand;
        
    }


    public void OnBowReleased()
    {
        if (currentArrow)
        {
            Destroy(currentArrow);
            currentArrow = null;
        }
        bowHand = null;
        pullingHand = null;
        otherHand = null;
        stringBone.position = stringRestPosition.position;
    }

    private void SpawnArrow()
    {
        currentArrow = Instantiate(arrowPrefab);
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        currentArrow.transform.position = arrowNockPoint.position;
        currentArrow.transform.rotation = arrowNockPoint.rotation;
        
        currentArrow.transform.Rotate(0, 180f, 0);
    }

    private void FireArrow(float pullAmount)
    {
        if (!currentArrow) return;

        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(arrowNockPoint.forward * pullAmount * shootForceMultiplier, ForceMode.Impulse);
        Debug.Log(arrowNockPoint.forward * pullAmount * shootForceMultiplier);
        Debug.DrawRay(arrowNockPoint.position, arrowNockPoint.forward * 2f, Color.red, 2f);

        currentArrow = null;
        pullingHand = null;
        stringBone.position = stringRestPosition.position;
    }

    private bool IsTriggerHeld(Transform hand)
    {
        if (hand == leftHand)
            return leftTriggerAction.action?.IsPressed() ?? false;
        if (hand == rightHand)
            return rightTriggerAction.action?.IsPressed() ?? false;
        return false;
    }
    
}
