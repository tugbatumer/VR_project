// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
// using System.Collections;
//
// public class PlayerZoneController : MonoBehaviour
// {
//     private enum Zone { Land, Water }
//     private Zone currentZone = Zone.Land;
//
//     [Header("Transition Settings")]
//     [SerializeField] private float transitionDuration = 1.0f;
//     [SerializeField] private float waterEntrySlowFactor = 0.3f;
//
//     [Header("References")]
//     [SerializeField] private Transform waterSurface; // Optional reference to water surface
//
//     private VRSwimmingController swimmingController;
//     private DynamicMoveProvider moveProvider;
//     private Rigidbody playerRb;
//     private float transitionProgress;
//     private bool isTransitioning;
//     private Vector3 entryVelocity;
//     private float originalSwimStrength;
//     private float originalSwimDrag;
//
//     private void Awake()
//     {
//         swimmingController = GetComponentInChildren<VRSwimmingController>();
//         moveProvider = GetComponentInChildren<DynamicMoveProvider>();
//         playerRb = GetComponentInChildren<Rigidbody>();
//
//         // Store original values
//         if (swimmingController != null)
//         {
//             originalSwimStrength = swimmingController.propulsionStrength;
//             originalSwimDrag = swimmingController.waterDrag;
//         }
//
//         InitializeState();
//     }
//
//     private void InitializeState()
//     {
//         swimmingController.enabled = false;
//         moveProvider.enabled = true;
//         playerRb.useGravity = true;
//         playerRb.linearDamping = 0f;
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (isTransitioning) return;
//
//         if (other.CompareTag("Water") && currentZone != Zone.Water)
//         {
//             StartCoroutine(TransitionToWater());
//         }
//         else if (other.CompareTag("Land") && currentZone != Zone.Land)
//         {
//             StartCoroutine(TransitionToLand());
//         }
//     }
//
//     private IEnumerator TransitionToWater()
//     {
//         isTransitioning = true;
//         currentZone = Zone.Water;
//         transitionProgress = 0f;
//
//         // Immediately disable movement and store entry velocity
//         moveProvider.enabled = false;
//         entryVelocity = playerRb.linearVelocity;
//
//         // Enable swimming but with reduced forces during transition
//         swimmingController.enabled = true;
//         swimmingController.propulsionStrength = originalSwimStrength * waterEntrySlowFactor;
//
//         // Position player at water surface if reference exists
//         if (waterSurface != null)
//         {
//             Vector3 newPosition = transform.position;
//             newPosition.y = waterSurface.position.y;
//             transform.position = newPosition;
//         }
//
//         while (transitionProgress < 1f)
//         {
//             transitionProgress += Time.deltaTime / transitionDuration;
//             float lerpValue = transitionProgress;
//
//             // Gradually change physics properties
//             playerRb.useGravity = false;
//             playerRb.linearDamping = Mathf.Lerp(0f, originalSwimDrag, lerpValue);
//             
//             // Smoothly transition velocity
//             playerRb.linearVelocity = Vector3.Lerp(entryVelocity, Vector3.zero, lerpValue);
//             
//             yield return null;
//         }
//
//         // Restore full swimming strength
//         swimmingController.propulsionStrength = originalSwimStrength;
//         isTransitioning = false;
//     }
//
//     private IEnumerator TransitionToLand()
//     {
//         isTransitioning = true;
//         currentZone = Zone.Land;
//         transitionProgress = 0f;
//
//         // Store exit velocity
//         Vector3 exitVelocity = playerRb.linearVelocity;
//
//         // Disable swimming immediately
//         swimmingController.enabled = false;
//
//         while (transitionProgress < 1f)
//         {
//             transitionProgress += Time.deltaTime / transitionDuration;
//             float lerpValue = transitionProgress;
//             
//             // Gradually change physics properties
//             playerRb.useGravity = true;
//             playerRb.linearDamping = Mathf.Lerp(originalSwimDrag, 0f, lerpValue);
//             
//             // Smoothly transition velocity
//             playerRb.linearVelocity = Vector3.Lerp(exitVelocity, Vector3.zero, lerpValue);
//             
//             yield return null;
//         }
//
//         // Enable movement
//         moveProvider.enabled = true;
//         isTransitioning = false;
//     }
//
//     public bool IsInWater()
//     {
//         return currentZone == Zone.Water && !isTransitioning;
//     }
//
//     public bool IsTransitioning()
//     {
//         return isTransitioning;
//     }
// }


using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerZoneController : MonoBehaviour
{
    private enum Zone { Land, Buffer, Water }

    private Zone currentZone = Zone.Land;

    private VRSwimmingController swimmingController;
    private DynamicMoveProvider moveProvider;
    private Rigidbody playerRb;

    private void Awake()
    {
        swimmingController = GetComponentInChildren<VRSwimmingController>();
        moveProvider = GetComponentInChildren<DynamicMoveProvider>();
        playerRb = GetComponentInChildren<Rigidbody>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered zone with tag: " + other.tag);
        if (other.CompareTag("Water"))
        {
            currentZone = Zone.Water;
            ApplyMode();
        }
        else if (other.CompareTag("Buffer"))
        {
            currentZone = Zone.Buffer;
            ApplyMode();
        }
        else if (other.CompareTag("Land"))
        {
            currentZone = Zone.Land;
            ApplyMode();
        }
    }

    private void ApplyMode()
    {
        switch (currentZone)
        {
            case Zone.Land:
                swimmingController.enabled = false;
                moveProvider.enabled = true;
                playerRb.linearVelocity = Vector3.zero;
                // playerRb.useGravity = true;
                break;
            
            case Zone.Buffer:
                swimmingController.enabled = true;
                moveProvider.enabled = true; // Allow both
                // playerRb.useGravity = true;
                break;
            
            case Zone.Water:
                swimmingController.enabled = true;
                moveProvider.enabled = false;
                playerRb.useGravity = false;
                break;
        }
    }
}
