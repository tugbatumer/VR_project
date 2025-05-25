using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

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
    
    [Header("Input Actions")]
    public InputActionReference leftTriggerAction;
    public InputActionReference rightTriggerAction;

    [Header("Settings")]
    public float attachDistance = 0.15f;
    public float shootForceMultiplier = 50f;
    public Transform pullingHand;
    public Transform otherHand;
    public Transform leftHand;
    public Transform rightHand;

    public Transform bowHand;

    private GameObject currentArrow;
    private float maxPullDistance;
    private float pullAmount = 0;
    
    public XRBaseInteractor handInteractor;
    public XRGrabInteractable grabInteractable;

    public GameObject aimCanvas;
    public float maxRayDistance = 100f;
    
    [SerializeField] public HapticImpulsePlayer rightHapticImpulse;
    [SerializeField] private float hapticIntensity = 0.5f;
    [SerializeField] private float hapticDuration = 0.2f;
    
    

    void Start()
    {
        maxPullDistance = Vector3.Distance(stringRestPosition.position, stringPullLimit.position);
    }

    void Update()
    {
        if (!bowHand) return;
        int arrowCount = InventoryManager.Instance.itemCounts[InventoryManager.itemType.Arrow];

        // Attach string to the other hand when close
        if (arrowCount > 0 && !pullingHand && Vector3.Distance(otherHand.position, stringRestPosition.position) < attachDistance)
        {
            pullingHand = otherHand;
            SpawnArrow();
        }

        // Simulate string pull
        if (pullingHand && currentArrow)
        {
            
            Vector3 pullDir = (stringPullLimit.position - stringRestPosition.position).normalized;
            pullAmount = Vector3.Dot(pullingHand.position - stringRestPosition.position, pullDir);
            pullAmount = Mathf.Clamp(pullAmount, 0, maxPullDistance);

            if (pullAmount > 0)
            {
                rightHapticImpulse.SendHapticImpulse(hapticIntensity, hapticDuration);
            }

            Vector3 rayOrigin = currentArrow.transform.Find("tipPoint").position;
            if (Physics.Raycast(rayOrigin, -1.0f * pullDir, out RaycastHit hit, maxRayDistance))
            {
                aimCanvas.SetActive(true);
                aimCanvas.transform.position = hit.point + 0.15f * pullDir;
                aimCanvas.transform.rotation = Quaternion.LookRotation(hit.normal);
            }

            stringBone.position = stringRestPosition.position + pullDir * pullAmount;
            topLimit.localRotation = Quaternion.Euler(pullAmount * -15f, 0f, 0f);
            bottomLimit.localRotation = Quaternion.Euler(pullAmount * 15f, 0f, 0f);
            currentArrow.transform.position = arrowNockPoint.position + pullDir * pullAmount;
            currentArrow.transform.rotation = arrowNockPoint.rotation;
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
            InventoryManager.Instance.itemCounts[InventoryManager.itemType.Arrow]++;
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
        InventoryManager.Instance.itemCounts[InventoryManager.itemType.Arrow]--;
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        currentArrow.transform.position = arrowNockPoint.position;
        currentArrow.transform.rotation = arrowNockPoint.rotation;
        
        currentArrow.transform.Rotate(0, 180f, 0);
    }

    public void FireArrow()
    {
        if (!currentArrow || pullAmount == 0) return;
        
        AudioManager.Instance.arrowShootingAudio.PlayOneShot(AudioManager.Instance.arrowShootingAudio.clip, MenuManager.Instance.masterVolumeScaler);
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(arrowNockPoint.forward * pullAmount * shootForceMultiplier, ForceMode.Impulse);

        currentArrow = null;
        pullingHand = null;
        stringBone.position = stringRestPosition.position;
        
        aimCanvas.SetActive(false);
    }

    private bool IsTriggerHeld(Transform hand)
    {
        if (hand == leftHand)
            return leftTriggerAction.action?.IsPressed() ?? false;
        if (hand == rightHand)
            return rightTriggerAction.action?.IsPressed() ?? false;
        return false;
    }
    
    public void ForceGrab()
    {
        if (grabInteractable && handInteractor)
        {
            handInteractor.interactionManager.SelectEnter((IXRSelectInteractor)handInteractor, grabInteractable);
        }
    }
}
